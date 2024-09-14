## Domínio

O domínio escolhido para a primeira etapa do trabalho de BAN II foi o de gerenciamento de linhas de ônibus. 

Neste sistema é possível registrar e consultar todas as viagens realizadas em uma empresa de ônibus municipal, adentrando detalhes como o ônibus, motorista, linha e estações de cada uma das viagens.

Cada uma das propriedades de uma viagem (Motorista, Ônibus, Linha (com suas paradas e estações)) são possíveis de serem criadas, editadas, excluídas e lidas, formando o CRUD completo, através dos menus disponíveis.

O sistema foi desenvolvido em formato de console, através das tecnologias do .Net 8 e Entity Framework Core, enquanto o banco PostgreSQL utilizado se encontra hospedado no Neon DB.

[Neon](https://console.neon.tech/)

![image](https://github.com/user-attachments/assets/c4de8521-40cd-47c2-90f8-7f209c366ca6)

A connection string para o banco se encontra no projeto e possui limite de tempo de computação e storage limit, portanto, podem ocorrer erros ao tentar utilizá-lo após muito tempo. Além disso, algumas redes (como a da UDESC) barram requisições ao Neon, portanto, você pode enfrentar problemas quanto à conectividade dependendo da sua rede.

## Esquema Conceitual

![image](https://github.com/user-attachments/assets/07e59bc3-7881-4cf4-8d08-c6adedd80f61)

## Esquema lógico como dicionário de dados

![image](https://github.com/user-attachments/assets/62225319-8775-4d5e-930e-da35d9e6299d)
![image](https://github.com/user-attachments/assets/e481186b-4918-4254-a177-3c1dc0839e31)
![image](https://github.com/user-attachments/assets/425b3713-58cd-4f58-9134-d10292baaee5)
![image](https://github.com/user-attachments/assets/be7805d1-a5e6-4456-8fd3-2ef1c5d496f2)
![image](https://github.com/user-attachments/assets/47ed0f3d-ea5a-4266-8426-a0c66f61b7b1)
![image](https://github.com/user-attachments/assets/051c4d71-211d-4aa8-beaa-e1164d35c990)

## Como utilizar

O projeto está disponível para o público no repositório do GitHub abaixo

[Repositório](https://github.com/phdguigui/BusManager)

Primeiramente você deve realizar o clone ou então baixar o projeto como zip na sua máquina, como no exemplo abaixo

![image](https://github.com/user-attachments/assets/ca21991b-88d2-4643-8381-e7ae4fba3cea)

Feito isso, você deverá acessar o diretório abaixo e procurar pelo executável do aplicativo (BusManager.exe)

```bash
.\BusManager\bin\Release\net8.0\win-x86
```

![image](https://github.com/user-attachments/assets/3f69ca4a-8d04-404b-a255-80e4d51ae43f)

Executando o .exe você já estará com tudo pronto e com o aplicativo em execução. A conexão ao banco de dados do Neon já está configurada e com a base populada e você não precisa instalar nada previamente, dado que o projeto foi publicado como self-contained, com todas as dlls necessárias já na pasta.

> [!CAUTION]
> Aplicativo disponível apenas para sistemas Windows x86

> [!WARNING]
> Base de dados populada por inteligência artificial, certos registros podem não estar seguindo a regra de negócio ou não seguirem 100% a realidade.

## Dump

Caso deseje replicar o banco de dados, no link abaixo, apontando para o mesmo repositório, terá o arquivo .sql para o dump da base de dados.

[Dump](https://github.com/phdguigui/BusManager/blob/main/Dump/dump-busmanager-202409141735.sql)

Entretanto, dentro do projeto também há a pasta Migrations, geradas a partir do Entity Framework Core, basta executá-los também.

[Migrations](https://github.com/phdguigui/BusManager/tree/main/BusManager/Migrations)

---

> [!NOTE]
> Trabalho realizado para a matéria de Banco de Dados II da professora Rebeca Schroeder da Universidade do Estado de Santa Catarina - Centro de Ciências Tecnológicas

### Developed by [Guilherme Siedschlag](https://github.com/phdguigui)
