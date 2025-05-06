
# 💰 CDB - Simulador de Rendimento de Certificado de Depósito Bancário

[![.NET 8](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com)
[![Angular](https://img.shields.io/badge/Angular-19-red)](https://angular.io)
[![Docker](https://img.shields.io/badge/Docker-Enabled-2496ED)](https://www.docker.com/)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

---

## 🧾 Sobre o Projeto

Este projeto é um simulador de investimentos em **CDB (Certificado de Depósito Bancário)**, permitindo calcular o rendimento bruto e líquido com base em um valor inicial e um prazo em meses.

### 📊 O que é um CDB?

- Um **CDB** é um título de renda fixa emitido por bancos.
- O investidor empresta dinheiro ao banco e recebe juros em troca.
- O rendimento depende do **CDI** (Certificado de Depósito Interbancário) e do tempo investido.
- Tributações são aplicadas conforme a tabela regressiva do Imposto de Renda.

---

## 📂 Estrutura da Solução

```
/CDB
 ├─ cdb.client/         # Frontend Angular 19 (Calculadora CDB)
 ├─ CDB.Server/         # Backend em .NET 8 (Web API)
 ├─ CDB.Tests/          # Testes Unitários do Backend 
 └─ docker-compose.yml  # Orquestração Docker (Backend + Frontend)
```

- **CDB.Server/**: Exposição de endpoint para cálculo com regras de negócios aplicadas.
- **CDB.Tests/**: Testes automatizados com FluentAssertions e FluentValidation.
- **cdb.client/**: Interface Web para entrada de dados e visualização dos resultados.

---

## 🚀 Tecnologias Usadas

- [ASP.NET Core 8.0](https://dotnet.microsoft.com) — API REST com CORS e validação.
- [Angular 19](https://angular.io) — Frontend moderno e reativo.
- [FluentValidation](https://fluentvalidation.net) — Validação clara e declarativa.
- [Docker & Docker Compose](https://www.docker.com/) — Empacotamento e orquestração.
- [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/vs/) — IDE de Desenvolvimento.

---

## ⚙️ Executando a Solução

### 📋 Pré-requisitos

1. Visual Studio 2022 com workload de ASP.NET e desenvolvimento web com suporte a container.

2. .NET 8 SDK instalado.

3. Node.js 20+ e Angular CLI

4. Docker e Docker Desktop (Windows).

### 🔽 Clonar o repositório

```bash
	git clone https://github.com/rodneyvictorsoares/CDB.git
	cd CDB
   ```
### 🚀 Executar com Visual Studio 2022

1. Abra a solução CDB.sln no Visual Studio 2022.

2. No Solution Explorer, clique com o botão direito na Solução e escolha 
	Configurar Projetos de Inicialização (Set Startup Projects....).

3. Selecione Vários Projetos de Inicialização (Multiple startup projects).

4. Defina CDB.Server como Iniciar (Start) (Project).

5. Defina cdb.client (o projeto do Angular) como Inciar (Start). 

6. Pressione F5 para rodar em modo Debug. O Visual Studio:

- Publica e sobe o CDB.Server em https://localhost:5133 (HTTPS) e http://localhost:5132 (HTTP).

- Inicia o Angular em http://localhost:4200 e proxya /api para o back-end.

7. **Importante**: Aguarde até que o navegador carregue a pagina do Angular (http://localhost:4200) 
	e a API (http://localhost:5132/swagger/index.html).
	
8. Após as páginas carregadas teste normalmente a aplicação.

### 🐳 Executar com Docker Compose

1. Feche o Visual Studio 2022 (Caso esteja aberto), caso não deseje fechar, tome o cuidado de parar a execução do 
	conteiner CDB.Server que pode ter sido iniciado nos testes do tópico anterior.
	
2. Abra um terminal na raiz da solução (`CDB/`), execute:
   ```bash
   docker compose down
   docker compose up --build
   ```
3. Após o processo de implantação ser finalizado, Acesse:
   - Frontend: [http://localhost:4200](http://localhost:4200)
   - API: [http://localhost:5132/swagger/index.html](http://localhost:5132/swagger/index.html)

---

## 🛠️ Abrir no Visual Studio Code

1. Abra o terminal na pasta raiz do projeto:
   ```bash
   code .
   ```
2. Use a extensão **Docker** para monitorar os containers, se desejar.
3. Utilize o terminal integrado para subir **docker-compose** ou rodar os projetos individualmente.

---

## 🧪 Testando o Cálculo do CDB

- **API**: use o Swagger integrado em /swagger ou curl:
   ```bash
   curl -X POST http://localhost:5132/api/CDB/calcular \
     -H "Content-Type: application/json" \
     -d '{"valorInicial":1000,"prazoEmMeses":6}'
   ```

- **Front**: insira um valor e prazo na UI e verifique o resultado bruto e líquido.

---

## 📝 Licença

Este projeto está licenciado sob a Licença MIT.
