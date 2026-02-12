# Testes de Integração

Resumo
- Esta pasta contém testes de integração que inicializam a aplicação WebApi em memória usando `WebApplicationFactory<Program>` e um `HttpClient` 
para chamar endpoints reais.

Bibliotecas usadas
- `Microsoft.AspNetCore.Mvc.Testing` — fornece `WebApplicationFactory` e `TestServer` para hospedar a aplicação em memória.
- `Microsoft.EntityFrameworkCore.InMemory` — provedor EF Core InMemory usado para isolar o banco durante os testes.
- `FluentAssertions` — helpers para asserts legíveis (opcional).
- `xUnit` + `Microsoft.NET.Test.Sdk` — runner e framework de testes.

Arquitetura do teste
- `CustomWebApplicationFactory` (`CustomWebApplicationFactory.cs`):
  - Herdamos `WebApplicationFactory<Program>` para customizar o pipeline de inicialização do host durante os testes.
  - Removemos descritores do `AppDbContext` previamente registrados pelo projeto (para evitar conflito de providers) e registramos `AppDbContext` 
  apontando para um banco InMemory com nome único (`Guid.NewGuid()`).
  - Construímos um `ServiceProvider` isolado apenas para realizar o seed inicial dos dados (categorias, produtos) e garantir que o estado exista 
  antes do teste.

Observações importantes sobre EF e providers
- Apenas um provider EF deve estar registrado por `IServiceProvider`. Para evitar conflitos entre o provider real (ex.: SqlServer) e o InMemory, 
o factory remove os descritores relacionados ao `AppDbContext` antes de registrar o InMemory.
- Se a aplicação original usar `UseInternalServiceProvider` ou configurar providers de forma não padrão isso pode causar conflito. Ajuste a 
inicialização da aplicação para não usar `UseInternalServiceProvider` ou garanta remoção completa dos descritores no factory.

Seed de dados
- O seed é executado no `CustomWebApplicationFactory` após registrar o contexto InMemory. Criamos entidades de domínio (ex.: `Category`, `Product`) 
diretamente e chamamos `db.SaveChanges()` para que os endpoints consultem dados reais durante os testes.

Requisitos para os testes
- O projeto de testes deve referenciar o projeto WebApi:
  - `dotnet add CleanArch.IntegratedTests/CleanArch.IntegratedTests.csproj reference src/CleanArch.WebApi/CleanArch.WebApi.csproj`
- Se `Program.cs` usa top-level statements, adicione `src/CleanArch.WebApi/ProgramPartial.cs` com `public partial class Program { }` para que 
`WebApplicationFactory<Program>` funcione.

Como executar
- Restaurar e rodar testes:
  - `dotnet restore`
  - `dotnet test CleanArch.IntegratedTests/CleanArch.IntegratedTests.csproj`

Problemas comuns e soluções
- Erro: "Only a single database provider can be registered" — remover todos os descritores do `AppDbContext` no factory (feito aqui) ou garantir 
que a app não registre providers extras.
- Erro: não encontrar `Program` — adicione o `ProgramPartial.cs` ou verifique a `ProjectReference` para o WebApi.
- Serialização/formatter errors em TestServer — alinhe versões de `Microsoft.AspNetCore.Mvc.Testing` e runtime (.NET) e evite references diretas 
conflitantes para `System.Text.Json`.

Boas práticas
- Use nomes de banco InMemory exclusivos por teste para isolar casos paralelos.
- Seed apenas o necessário para o teste.
- Prefira verificar comportamento via HTTP (status codes e payloads) em testes de integração.

Este arquivo documenta o setup de testes de integração do projeto e serve como guia rápido para manutenção e criação de novos testes.
