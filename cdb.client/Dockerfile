FROM mcr.microsoft.com/dotnet/sdk:8.0 AS with-node
RUN apt-get update
RUN apt-get install curl
RUN curl -sL https://deb.nodesource.com/setup_20.x | bash
RUN apt-get -y install nodejs
RUN npm install -g @angular/cli

FROM with-node AS build
WORKDIR /src
COPY ["cdb.client/cdb.client.esproj", "cdb.client/"]
COPY . .
WORKDIR "/src/cdb.client"
RUN npm install

EXPOSE 4200
CMD ["sh", "-c", "ng serve --host 0.0.0.0 --disable-host-check 2>/dev/null"]
