# Camada IoC (Inversão de Controle / Dependency Injection)

Resumo
- A camada `Infra.IoC` é responsável por registrar dependências da aplicação no contêiner de DI (Dependency Injection). Ela centraliza a configuração de serviços 
e integrações entre camadas (Application, Domain e Infraestrutura), sem implementar lógica de negócio.

Propósito
- Fornecer métodos e extensões para registrar serviços, handlers, facades, repositórios e clientes externos.
- Manter o ponto único de configuração de DI para facilitar manutenção, testes e inicialização da aplicação.

O que deve conter
- Extensões `IServiceCollection` (por exemplo `AddPresentation`, `AddInfrastructure`, `AddApplication`): métodos que registram conjuntos de serviços por responsabilidade.
- Registro de implementações de repositórios e unidades de trabalho (ex.: `ICategoryRepository` -> `CategoryRepository`).
- Registro de handlers/mediators ou factories se houver um mecanismo de resolução por convenção.
- Registro de serviços de infraestrutura (banco, cache, mensageria, etc.).
- Registro de middlewares e adaptadores pequenos quando necessário (por exemplo: `IClock`, `IEmailSender`).

Boas práticas
- Separe registros por responsabilidade em classes estáticas de extensão (um método por conjunto de responsabilidades).
  - Ex.: `AddInfrastructure(this IServiceCollection services, IConfiguration config)`, `AddPresentation(this IServiceCollection services)`.
- Prefira lifetimes adequados:
  - `Singleton` para serviços sem estado e caros para criar.
  - `Scoped` para repositórios/DbContext e serviços que acompanham a requisição.
  - `Transient` para serviços leves e sem estado por instância.
- Evite realizar lógica de inicialização complexa durante o registro; apenas configure e registre tipos e factories.
- Use `IConfiguration` para configurar serviços que dependem de settings (ex.: connection strings, opções).

Exemplo de fluxo de registro (conceitual)
1. `Program.cs` chama `builder.Services.AddPresentation().AddInfrastructure(configuration)`.
2. `AddInfrastructure` registra DbContext, repositórios, serviços de infra e chama `AddApplication`.
3. `AddApplication` registra handlers, facades e quaisquer services da camada Application que precisem de DI.

Testabilidade
- Manter os registros centralizados facilita testes de integração (substituir implementações reais por doubles/mocks no ambiente de teste).
- Em testes unitários preferir injetar mocks diretamente nas classes testadas; o módulo IoC não precisa ser carregado.

Boas práticas de manutenção\
- Ao adicionar novos repositórios/handlers, crie métodos de extensão pequenos e composáveis para manter a clareza.

Notas finais
- A camada IoC é uma peça de infraestrutura: ela conecta as camadas, mas não deve conter lógica de negócio. Mantenha-a simples e previsível 
para facilitar evolução e diagnóstico.