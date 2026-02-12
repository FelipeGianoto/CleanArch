# Camada Application

Resumo
- A camada `Application` contém a lógica de orquestração da aplicação: casos de uso (use cases), comandos, queries e handlers. 
Ela não conhece detalhes de infraestrutura (DB, web, etc.) e depende apenas de abstrações (interfaces) definidas na camada `Domain` 
ou em contratos internos.

Princípios (Clean Architecture)
- Dependências apontam para dentro: a camada `Application` depende de `Domain` (entidades e interfaces), nunca de infra.
- Cada caso de uso é representado por um comando ou query e um handler responsável por executar a operação.
- A camada expõe apenas entradas e saídas (DTOs/Outputs) — não expõe entidades de domínio diretamente para a camada de apresentação.

Detalhamento sobre Clean Architecture aplicado à camada Application

- Regra de dependência (Dependency Rule): código em `Application` só pode depender de camadas mais internas (normalmente `Domain`). 
Não deve haver referência a implementações de infra (DB, filas, frameworks). Em tempo de execução a infraestrutura proverá as implementações através de interfaces.

- Responsabilidade da camada Application:
  - Orquestrar casos de uso (coordenar repositórios, serviços de domínio e outras abstrações).
  - Validar regras de aplicação (pré-condições que não sejam regra de negócio pura).
  - Montar e retornar objetos de saída (Outputs / DTOs) usados pela camada de apresentação.
  - Iniciar/encerrar transações quando a política for de aplicação (pode também ser delegada à infra via UnitOfWork abstraído).

- O que pertence aqui (exemplos):
  - Commands/Queries e seus Handlers.
  - DTOs de saída (Outputs) e estruturas resultado dos use cases.
  - Orquestrações entre múltiplos repositórios/serviços.
  - Facades que agrupam vários use cases para simplificar a camada de apresentação.

- O que NÃO pertence aqui:
  - Detalhes de acesso a dados (implementações de repositório).
  - Código específico de framework (controllers, middleware, EF migrations, etc.).
  - Infraestrutura de logging/telemetria específica — apenas use abstrações ou deixe para a camada superior.

- Validação e erros
  - Validação de contratos HTTP (formatos, required fields) deve ficar na camada de apresentação (WebApi). Validações de negócio que envolvem 
  regras do domínio podem ser executadas aqui chamando serviços/domain entities.
  - Lançar exceções específicas (ex.: `KeyNotFoundException`) é aceitável; a camada de apresentação (middleware global) deve mapear para códigos HTTP apropriados.

- Transações
  - A camada Application pode decidir a unidade de trabalho (quando uma operação precisa agrupar várias alterações). Idealmente use uma abstração (ex.: `IUnitOfWork`)
  que a infraestrutura implementa.

- Mapeamento entre Presentation <-> Application
  - Use DTOs/Request objects na WebApi e converta para Commands/Queries antes de chamar a Application. O mapeamento pode ser manual, por métodos de extensão 
  ou com ferramentas como AutoMapper.

- Testabilidade
  - Handlers e facades são fáceis de testar isoladamente porque dependem apenas de interfaces. Escreva testes unitários cobrindo fluxos felizes e exceções de negócio.

- Boas práticas rápidas
  - Mantenha handlers pequenos e com responsabilidade única.
  - Não repita lógica de domínio — reutilize services/entidades do Domain.
  - Use nomes claros para Commands/Queries (`GetCategoryByIdQuery`, `CreateCategoryCommand`, etc.).
  - Se o controller precisa de 3+ dependências relacionadas, considere criar um Facade para reduzir acoplamento do controller.

O que foi implementado nesta camada
- Abstrações de handler
  - `ICommandHandler<TCommand, TOutput>` / `ICommandHandler<TCommand>`
  - `IQueryHandler<TQuery, TOutput>`

- Casos de uso de `Category`
  - Commands
    - `CreateCategoryCommand` e handler correspondente (criação de categoria)
    - `UpdateCategoryCommand` e `UpdateCategoryHandler` (atualização)
  - Queries
    - `ListCategoryQuery` e `ListCategoryHandler` (listagem)
    - `GetCategoryByIdQuery` e `GetCategoryByIdHandler` (buscar por id)
    - `GetCategoryByIdWithProductsQuery` (buscar categoria incluindo produtos) — handler dedicado para operações com includes/paginação
  - Outputs / DTOs
    - `ListCategoryOutput`, `CategoryOutput`, `GetCategoryByIdOutput`, `CreateCategoryOutput` etc.

- Facade
  - `ICategoryFacade` e `CategoryFacade` para agrupar e orquestrar os handlers relacionados a `Category`. O facade reduz 
    o número de dependências injetadas em controllers e centraliza composições simples de use cases.

Seção de referência
- Veja a pasta `UseCases/Category` para os comandos, queries, handlers, outputs e facade implementados.
