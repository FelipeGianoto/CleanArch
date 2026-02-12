# Camada de Infra (Acesso a Dados)

A camada de Infra implementa os detalhes técnicos necessários para persistência e recuperação de dados. No contexto da Clean Architecture, 
ela fica na borda do sistema e traduz as abstrações do domínio para implementações concretas (banco de dados, filas, storage, etc.).

Por que existe
- Separar responsabilidades: manter o núcleo do negócio (camada `Domain`) isolado de detalhes de infraestrutura.
- Fornecer implementações concretas para as interfaces definidas no domínio (ex.: `ICategoryRepository`).

Responsabilidades principais
- Implementar repositórios que seguem as interfaces do domínio.
- Configurar e expor o `DbContext` (Entity Framework Core) ou outro cliente de persistência.
- Mapear entidades de domínio para a estrutura de persistência (configurações EF Core / fluent API).
- Criar e aplicar migrações, configurar connection string e opções do provedor.
- Tratar aspectos de performance e comportamento específico do banco (índices, collations, queries otimizadas).

Estrutura comum nesta pasta
- `Contexts/` - classes de `DbContext` (ex.: `AppDbContext`).
- `Repositories/` - implementações concretas de repositórios do domínio.
- `Configurations/` - classes com configuração de entidades (ex.: `CategoryConfiguration`) usando Fluent API.
- `Migrations/` - pasta gerada pelo EF Core com versionamento do esquema.

Dicas e boas práticas (EF Core e SQL Server)
- Registrar `DbContext` com scope `Scoped` no contêiner de DI.
- Manter as configurações de entidade na pasta `Configurations` e usar `HasIndex(...).IsUnique()` quando precisar garantir unicidade no banco (ex.: nome de categoria).
- Tratar collations quando a unicidade deve ser case-insensitive ou case-sensitive explicitamente (usar `UseCollation` ou definir collation na coluna).
- Use `async`/`await` nas operações de I/O no repositório (`ToListAsync`, `FirstOrDefaultAsync`, etc.).
- Evite expor `DbContext` diretamente para a camada de apresentação; use repositórios/abstrações.

Migrações e deploy

As migrações do EF Core servem para versionar o esquema do banco de dados de forma controlada. Elas traduzem mudanças no modelo (entidades/configurações) 
em instruções DDL que podem ser aplicadas ao banco.

Fluxo comum
- Fazer alterações nas entidades ou nas classes de configuração (por exemplo adicionar `HasIndex(...).IsUnique()`).
- Criar uma nova migração que descreve a mudança.
- Revisar a migração gerada (às vezes é necessário ajustar o SQL gerado).
- Aplicar a migração ao banco de dados de desenvolvimento/teste.
- Em ambientes de produção, gerar script SQL e revisar antes de aplicar.

Comandos úteis
- Criar migração:
  - `dotnet ef migrations add <Name> --project src/CleanArch.Infra.SqlServer --startup-project src/CleanArch.WebApi`
  - Se preferir executar a partir do diretório do projeto de infra:
    - `cd src/CleanArch.Infra.SqlServer`
    - `dotnet ef migrations add <Name> --startup-project ../CleanArch.WebApi`
- Aplicar migrações:
  - `dotnet ef database update --project src/CleanArch.Infra.SqlServer --startup-project src/CleanArch.WebApi`
- Gerar script SQL para revisão/manual deploy:
  - `dotnet ef migrations script --project src/CleanArch.Infra.SqlServer --startup-project src/CleanArch.WebApi --output migrations.sql`

Pontos importantes e boas práticas
- Antes de criar um índice único em uma tabela existente, verifique e trate duplicatas — caso contrário a migração falhará ao aplicar.
- Sempre revise as migrações geradas automaticamente; o EF Core nem sempre produz exatamente o que você espera para operações complexas.
- Use `--project` e `--startup-project` corretamente: o projeto que contém o DbContext é o `--project`; o `--startup-project` deve ser o projeto executável 
que contém a configuração de runtime (ex.: WebApi).
- Se o `DbContext` não puder ser criado em tempo de design, implemente `IDesignTimeDbContextFactory<TContext>` para fornecer a configuração necessária ao `dotnet ef`.
- Em CI/CD é recomendável gerar um script SQL (`dotnet ef migrations script`) e aplicar esse script via processo de deploy, permitindo revisão e controle transacional.
- Para bancos em produção considere criar estratégias de deploy seguras:
  - Rodar migrations durante janelas de manutenção controladas.
  - Testar migrações em ambiente de staging com dados representativos.
- Para reverter uma migração localmente use:
  - `dotnet ef database update <PreviousMigrationName> --project ... --startup-project ...`
  - Em produção prefira scripts de rollback manuais ou backups/restore ao invés de downgrades automáticos.

Detectando problemas comuns
- "No project was found": execute o comando a partir do diretório correto ou informe `--project` / `--startup-project` com os caminhos corretos para os .csproj.
- Erros ao aplicar migração por causa de dados existentes (ex.: ao criar índice único): crie uma migração manual que limpe/normalize dados antes de criar a constraint, 
ou trate os dados previamente.

Observações finais
- Manter migrações no mesmo projeto que o `DbContext` é uma convenção simples; em soluções maiores pode-se centralizar migrações em um projeto dedicado de infra/migrations.
- Versione as pastas de migração no controle de versão e documente qualquer alteração manual feita nos scripts gerados.

Transações e Unidade de Trabalho
- Quando uma operação de aplicação precisa agrupar várias alterações, utilize uma abstração de UnitOfWork ou coordene isso na camada de aplicação. A infra deve 
expor meios para iniciar/commitar/rollback de transações quando necessário.

Testes
- Implementações de repositório podem ser testadas com um banco em memória (EF InMemory) ou com testes de integração usando um banco real/containers.
- Para testes unitários da camada de aplicação e domain, substitua repositórios por mocks/stubs — evite executar migrações nos testes unitários.

Boas práticas de manutenção
- Isolar SQL específico e otimizações dentro da infra para facilitar futuras trocas de provedor.
- Documentar alterações de esquema e pré-requisitos antes de aplicar migrações em produção.
- Tratar logs e métricas na infraestrutura, mas manter a configuração e uso via abstrações quando possível.

Observações sobre design
- A infra traduz conceitos do domínio para o modelo relacional ou objeto-relacional, mas não contém regras de negócio. Qualquer lógica complexa de domínio deve 
permanecer nas entidades ou serviços do domínio.

Padrão Repository
- O padrão `Repository` é usado para encapsular a lógica de acesso a dados e expor operações de persistência por meio de interfaces definidas na 
camada de `Domain` (ex.: `ICategoryRepository`).
- Benefícios:
  - Isola a camada de aplicação e domínio dos detalhes do EF Core ou SQL.
  - Facilita testes (substituir repositórios por mocks ou fakes).
  - Centraliza queries e operações de persistência que pertencem ao agregado.
- Diretrizes de uso neste projeto:
  - Defina métodos específicos necessários pelo domínio (por exemplo: `ListAsync`, `GetByWhereAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync`).
  - Retorne coleções ou entidades prontas, evitando expor `IQueryable<T>` diretamente para a camada de aplicação (evita vazamento de detalhes de implementação e 
  queries acopladas ao EF).
  - Implemente operações assíncronas (`ToListAsync`, `FirstOrDefaultAsync`) e propague `CancellationToken`.
  - Não coloque lógica de negócio no repositório; ele deve apenas recuperar e persistir o estado. Validações de negócio permanecem no domínio/serviços de domínio.
  - Considere combinar o `Repository` com uma abstração de `UnitOfWork` quando operações precisarem de transação explícita envolvendo múltiplos repositórios.

Resumo
- A camada Infra garante que o domínio e a aplicação tenham implementações concretas para persistência, mantendo o núcleo do sistema testável e independente de 
tecnologias específicas.

Resumo
- A camada Infra garante que o domínio e a aplicação tenham implementações concretas para persistência, mantendo o núcleo do sistema testável e independente 
de tecnologias específicas.

