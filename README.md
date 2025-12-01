# 🏦 BankSim - Projeto Final Solutis-Dev-Trail

> **AVISO:** Este projeto foi desenvolvido utilizando **.NET 9**.

## 1. 🚀 Sobre o Projeto

Este é o projeto final do processo **Solutis-Dev-Trail**. O objetivo principal foi desenvolver uma aplicação de simulação bancária trabalhada durante as aulas

* A construção e acompanhamento do projeto durante as aulas foi feita no repositório : https://github.com/Jean-Carlo89/c-learning
   Nele estão todo os mini-projetos, referencia de commits do projeto final, testes e tudo que foi aprendido durante o curso.

## 2. 🔗 Acesso ao Swagger (GCP)

Você pode interagir com a aplicação que está rodando na Google Cloud Platform através do Swagger. O link será disponibilizado aqui:

[**LINK DO SWAGGER**]
(http://34.95.165.194:8080/swagger/index.html)

## 3. 🛠️ Como Rodar o Projeto Localmente

Existem duas formas principais de colocar o projeto em execução na sua máquina: utilizando `docker-compose` ou de forma manual.

---

### Método 1: Orquestração com Docker Compose 

Esta abordagem é a mais simples, pois irá subir tanto a aplicação quanto o SQL SERVER

1.  **Clone o Repositório:**
    ```bash
    git clone https://github.com/Jean-Carlo89/solutis-dev-trail-projeto-final.git
    ```

2.  **Acesse a Pasta do Projeto:**
    ```bash
    cd [pasta-do-projeto]
    ```

3.  **Execute o Docker Compose:**
    ```bash
    docker-compose up
    ```

#### ⚙️ Observações sobre o Docker Compose:

* **Containers:** A orquestração no `docker-compose.yml` irá construir e rodar dois containers: um para o **BankSystem** e um container com o **SQL Server**.
* **Aplicação:** A aplicação estará disponível na porta **8080** do seu `localhost`.
* **Swagger Local:** Após a aplicação subir, você pode acessar a documentação e testar as rotas por meio do Swagger:
    * 👉 **[http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)**
* **Banco de Dados:** O container do SQL Server executa automaticamente uma **migration** que cria todas as tabelas necessárias no banco `BankDb`.
* **Recomendação:** Se você já teve outros containers de SQL Server rodando localmente, **é altamente recomendável deletar todos os volumes não utilizados** antes de rodar o comando `docker-compose up`, pois isso pode causar conflitos na inicialização do banco e nas criações das tabelas o que pode gerar erros.

---

### Método 2: Execução Manual

Para rodar a aplicação diretamente, você precisará ter o **.NET 9** SDK instalado e um SQL Server configurado.

1.  **Clone o Repositório:**
    ```bash
    git clone https://github.com/Jean-Carlo89/solutis-dev-trail-projeto-final.git
    ```

2.  **Acesse a Pasta do Projeto:**
    ```bash
    cd [pasta-do-projeto]
    ```

3.  **Execute a Aplicação:**
    ```bash
    dotnet run
    ```

    * **Swagger Local:** Após a aplicação subir, você pode acessar a documentação e testar as rotas por meio do Swagger:
    * 👉 **[http://localhost:5102/swagger/index.html](http://localhost:5102/swagger/index.html)**

#### 🗄️ Configuração do Banco de Dados (SQL Server)

Para a execução manual, é necessário que o **SQL Server** esteja acessível e configurado.

* **Configuração Padrão Esperada:** O projeto espera que você tenha um SQL Server rodando na porta padrão **1433** do seu `localhost`.
* **Configurações de Conexão:** A string de conexão é definida no arquivo `appsettings.Development.json`.

**Conteúdo Padrão de `appsettings.Development.json`:**

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "BankDatabase": "Server=tcp:localhost,1433; Database=BankDb; User=sa; Password=minhaSenhaForte*; TrustServerCertificate=True;"
  },
  "AllowedHosts": "*"
}

```

* **Se Você já Tiver um SQL Server Local:** Se a sua instância local do SQL Server tiver uma senha de usuário `sa` diferente da senha inicial que deixei, você **deverá alterar a senha** na `ConnectionStrings` dentro do arquivo `appsettings.Development.json`.


* **É possível optar por usar o docker compose aoenas para o SQL Server caso não tenha instalado localmente:** Você pode subir só o container do banco com `docker-compose up sqlserver`. **Neste caso**, a porta mapeada no seu `localhost` é **1432** (enquanto a porta interna do container é 1433, conforme o `docker-compose.yml`). Você **deverá alterar a porta** na sua `ConnectionStrings` de `1433` para **`1432`**:

    ```json
    "BankDatabase": "Server=tcp:localhost,1432; Database=BankDb; User=sa; Password=minhaSenhaForte*; TrustServerCertificate=True;"
    ```
