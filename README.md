# PERSONAL FINANCE SYSTEM - PFS

Sistema para gerenciar e integrar todas as finanças pessoal permitindo o usuário ter o controle de seus gastos, analisando por categorias e possibilitando o estabelecimento de metas financeiras. Permite que o usuário crie simulações para sua atual situação.

## Produto 

Regras de negócio do produto incluem integração de contas bancárias, visualização de balanço financeiro, gerenciamento de receitas e gastos, e funcionalidades como login e monitoramento em tempo real via aplicativo.


### Requisitos Funcionais

| #  | Case        | Description                                                       | Priority | Note |
|----|-------------|-------------------------------------------------------------------|--|--|
| 1  | Integração  | O usuário poderá integrar contas de diferentes bancos             | | |
| 2  | | O usuário pode pertencer a mais de um painel | | |
| 3  | | O usuário poderá visualizar o balanço geral de todas as informações financeiras | | |
| 4  | | O usuário poderá visualizar os últimos gastos na tela de dashboard | | |
| 5  | | O usuário poderá verificar todos os gastos em um tela  exclusiva | | |
| 6  | | O usuário pode fazer um CRUD de receitas e gastos | | |
| 7  | | O usuário poderá informar receitas/gastos fixos ou variáveis | | |
| 8  | | O usuário poderá visualizar os dados das contas cadastradas em uma tela exclusiva | | |
| 9  | | O usuário poderá simular meses a frente utilizando os gastos e receitas fixas | | |
| 10 | | O usuário poderá ter a integração com atualizações em tempo real através do aplicativo que monitorará as atividades via SMS | | |
| 11 | | Aplicativo mobile de background que monitora | | |
| 12 | | O usuário poderá visualizar os gastos por categorias | | |


## Técnico

### Banco de dados

O banco utilizado é o **PostgreSQL** 

#### Descrição modelagem

- Management Control
  - Users
    - Usuario cadastros contendo informações como senha, access_token para a plataforma seu status entre outros.
  - Role
    - Identificar qual o nivel de permissão do usuário
  - Painel User
    - Como os usuários poderão ter mais de um painel (caso esteja permitido dentro do role) será uma tabela de vinculo entre os mesmos.
  - Painel
    - Será o que conterá as principais informações do painel, unificando as informações referente aquele painel
  - Accounts 
    - Todas as contas vinculadas ao painel
  - Status
    - Status possiveis dos registros em geral
  - Bank
    - Informações de quais bancos estão disponiveis para a integração

- DB Painel
  - Transaction
    - Todas as transações realizadas e importadas para o painel
  - Categorias
    - Categorias para identificar as transações
  - Importance
    - Apenas para definir um grau de importancia do gasto
  - Credit card
    - Cadastro de todos os cartões de creditos
  - AccountCreditCard
    - Vinculação de contas com o cartão de crédito


#### Modelagem 

![img.png](img.png)

#### DUMPS DO BANCO

- [dump-PFS-CORE-202505022031](../../Users/lucas/Downloads/dump-PFS-CORE-202505022031)
- [dump-PFS-MG-202505022032](../../Users/lucas/Downloads/dump-PFS-MG-202505022032)




















