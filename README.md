# Smart-Watts

## Integrantes

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


 ### TESTES SWAGGER

 - **POST-ML**

   ![image](https://github.com/user-attachments/assets/b92bd530-8af1-4cad-a6bb-f7490b7d012a)

   - **POST**
   - ![image](https://github.com/user-attachments/assets/a0343d6c-7e84-4b18-bccd-89f697796280)
   - ![image](https://github.com/user-attachments/assets/bc3e4d8e-8736-4268-8fad-d85695e2638b)
   - ![image](https://github.com/user-attachments/assets/f50ad452-5c05-4e72-ae14-8f8994614f58)
  


- **GET**
- ![image](https://github.com/user-attachments/assets/f3af6a1d-db49-4694-8d2b-27e079077083)
- ![image](https://github.com/user-attachments/assets/2de4f81f-d2de-4606-9988-061d4a264322)
- ![image](https://github.com/user-attachments/assets/fb90d756-7eae-4afd-a1b3-477767bcc316)




- **GET ID**
- ![image](https://github.com/user-attachments/assets/3910910f-11a0-4f16-a85c-ad8d97b226fa)
- ![image](https://github.com/user-attachments/assets/2adc007f-c888-4e31-801a-2104163c1e3c)
- ![image](https://github.com/user-attachments/assets/1c9e8c02-ad64-4280-90c7-28f7a9919551)



- **PUT**
- ![image](https://github.com/user-attachments/assets/493f121c-1c66-4b24-b31b-531cec09b120)
- ![image](https://github.com/user-attachments/assets/f6534f33-92d0-4758-9776-2ccd6f319d72)
- ![image](https://github.com/user-attachments/assets/e0eb94f4-9280-4856-a03a-9d308571088c)



- **DELETE/SELECT**
- ![image](https://github.com/user-attachments/assets/10a7fcf2-752a-4c6d-90da-1e5dae744cae)
  ![image](https://github.com/user-attachments/assets/81ea4ba1-66dd-41f8-8d10-6dfd73a9ecf5)
  ![image](https://github.com/user-attachments/assets/faacc310-8da7-4306-9a28-e26dc9a7d2e5)












