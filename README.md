# Teste Pratico empresa programação.

Apresentação da aplicação:
* A aplicação foi desenvolvida utilizando o Framework .Net Core 2.1;
* O controle de segurança utilizado é o Identity com controle de roles para "Usuario" e "Jogo";
* A aplicação utiliza Web API do .NET Core junto da Auto Validação de Token Antifalsificação;
* O banco de dados utilizado é o SQL Server com Entity Framework Core database first;
* Implementado Docker files dos projetos e o Docker Compose subindo a aplicação e um servidor Sql Server local;
* A camada de teste contém testes unitários e integrados;
* Implementado script de deployment local da aplicação;
* A arquitetura utilizada foi a modelagem de software DDD;
* Utilizado o Framework web Razor, junto do Bootstrap 4 e jQuery.



Configuração do Docker Linux Containers:
* Alterar em "~\Fonte\TesteInvillia\TesteInvillia\appsettings*.json" a propriedade "ConnectionStrings.DefaultConnection" com a "ConnectionStrings.DockerConnection".
* Executar o comando "docker-compose up -d" na raiz da solução.


Configuração do banco de dados Local - SQL Server:
* Criar um banco de dados com o nome "TesteInvillia".
* Executar o script "~\Banco de Dados\script_create.sql".
* Executar o script "~\Banco de Dados\script_data.sql".
* Alterar em "~\Fonte\TesteInvillia\TesteInvillia\appsettings*.json" a propriedade "ConnectionStrings.DefaultConnection" com a string de conexão correta.


Configuração do banco de dados Docker - SQL Server:
* Após a criação do Docker, acessar o servidor com as seguintes credencias: 
        Nome servidor: localhost,1433
        Logon: sa
        Senha: PasswordInvillia001
* Utilizar o banco de dados do sistema "master".
* Executar o script "~\Banco de Dados\script_create.sql".
* Executar o script "~\Banco de Dados\script_data.sql".
* Alterar em "~\Fonte\TesteInvillia\TesteInvillia\appsettings*.json" a propriedade "ConnectionStrings.DefaultConnection" com a "ConnectionStrings.DockerConnection".


Configuração dos Testes unitário e integrado:
* Alterar em "~\Fonte\TesteInvillia\TesteInvillia.TestesIntegracao\Config\ConexaoStringTestes.cs" a propriedade "CONEXAO_STRING_TESTE" com a string de conexão correta.
* Alterar em "~\Fonte\TesteInvillia\TesteInvillia.TestesUnitario\Config\ConexaoStringTestes.cs" a propriedade "CONEXAO_STRING_TESTE" com a string de conexão correta.
* Obs.: Ao executar os testes serão perdidas as permissões do usuário principal, recomendo executar os scripts de banco de dados novamente.


Script de Deployment:
* O escript de deployment enconta-se em "~\Fonte\TesteInvillia\TesteInvillia\deployment.ps1" e pode ser executado o PowerShell.
