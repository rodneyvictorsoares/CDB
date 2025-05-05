# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS with-node
RUN apt-get update
RUN apt-get install curl
RUN curl -sL https://deb.nodesource.com/setup_20.x | bash
RUN apt-get -y install nodejs
RUN npm install -g @angular/cli

FROM with-node AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CDB.Server/CDB.Server.csproj", "CDB.Server/"]
COPY ["cdb.client/cdb.client.esproj", "cdb.client/"]
RUN dotnet restore "./CDB.Server/CDB.Server.csproj"
COPY . .
WORKDIR "/src/CDB.Server"
RUN dotnet build "./CDB.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CDB.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
# 4.1 Volta a ter privilégios de root para instalar openssl e gerar o cert
USER root
RUN apt-get update 
RUN apt-get install -y openssl 
RUN mkdir /https 

 # gera um certificado X.509 válido por 1 ano para 'localhost'
RUN openssl req -x509 -nodes -days 365 -subj "/CN=localhost"  -newkey rsa:2048 -keyout /https/localhost.key -out /https/localhost.crt 
 # empacota num .pfx protegido por senha
RUN openssl pkcs12 -export -out /https/aspnetapp.pfx -inkey /https/localhost.key -in /https/localhost.crt -passout pass:Crypt3dP@ss 
RUN chmod -R a+r /https 
RUN chown -R app:app /https 
 # limpa cache apt para manter a imagem enxuta
RUN rm -rf /var/lib/apt/lists/*

# 4.2 Define as variáveis de ambiente para Kestrel servir HTTP e HTTPS
ENV ASPNETCORE_URLS="http://+:8080;https://+:8081"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=Crypt3dP@ss

# 4.3 Volta ao usuário sem privilégios
USER app
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CDB.Server.dll"]
