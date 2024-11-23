# Smart-Watts

##Integrantes

O Aluno Felipe Batista é da turma 2TDSPY Enquanto os demais são do 2TDSPV.

RM551191 - Diego Mascarenhas Santos

RM98482 - Sarah Oliveira Souza Rosa

RM97798 - Ester Dutra da Silva

RM550981- Henrique Gerson Costa Correia

RM99985 - Felipe Batista Gregório (2TDSPY)

## Arquitetura

No desenvolvimento da aplicação SmartWatts, optamos por utilizar a arquitetura monolítica, onde todas as funcionalidades e interfaces do sistema são integradas em uma única aplicação. Esta abordagem tem se mostrado vantajosa em várias frentes, especialmente no que se refere à simplicidade no gerenciamento e na agilidade de desenvolvimento.

## Padrão Utilizado
Repository
No desenvolvimento da aplicação SmartWatts, adotamos o padrão Repository para gerenciar a comunicação entre a camada de persistência de dados e a lógica de negócios da aplicação. Este padrão tem como objetivo abstrair o acesso aos dados, proporcionando uma maneira mais organizada e flexível de manipular as entidades do sistema, além de permitir maior desacoplamento e facilitando testes e manutenção.


## Projeto Smart-Watts
O SmartWatts é um sistema projetado para gerenciar dispositivos e realizar previsões de consumo de energia com base nos dados coletados de dispositivos conectados. O foco principal é proporcionar uma maneira eficiente de monitorar o consumo de energia e otimizar os custos associados.

No projeto, a aplicação centraliza a gestão de informações como usuários, dispositivos e dados de consumo de energia. Os dados são armazenados de forma organizada em um banco de dados relacional, garantindo integridade e consistência, o que é essencial para as funcionalidades do sistema. A aplicação realiza operações como leitura, escrita e atualização de dados de forma simples e eficiente.

Um dos principais recursos do SmartWatts é a capacidade de prever o consumo de energia, utilizando algoritmos de Machine Learning. A partir dos dados históricos de consumo e custos, o sistema é capaz de fornecer previsões sobre o consumo futuro, auxiliando na gestão eficiente da energia.

## Instruções para Rodar a API:
1. **Clone o repositório do github**:
2. **Banco de Dados**: Configure o banco de dados que será utilizado pela API e atualize a string de conexão no arquivo `appsettings.json`.
3. **Criar migrations**: Rode no cmd "dotnet ef migrations add InitialCreate".
4. **Banco de Dados**: Aplique as alterações no banco "dotnet ef database update".
5. **Projeto**: Rode o projeto.
6. **SWAGGER**: Voce ira acessar o Swagger em um caminho semelhante a esse https://localhost:7299/Swagger/index.html

7. ### TESTES SWAGGER
