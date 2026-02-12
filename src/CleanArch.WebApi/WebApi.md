# Camada WebApi (Presentation)

Resumo
- A camada `WebApi` é responsável por expor a aplicação via HTTP (endpoints REST). Ela serve como a fronteira entre o mundo externo
(clients, navegadores, consumidores) e a lógica da aplicação (camada `Application`).

Responsabilidades principais
- Receber requisições HTTP, mapear para objetos de entrada (DTOs/Requests).
- Validar formato e contratos de entrada (DataAnnotations, FluentValidation, etc.).
- Converter DTOs em `Commands` ou `Queries` e chamar a camada `Application` (handlers, facades).
- Tratar respostas (retornar códigos HTTP apropriados e payloads de saída/`Outputs`).
- Configurar pipeline HTTP: middlewares, autenticação, autorização, CORS, logging e documentação (Swagger).
- Centralizar tratamento de exceções (middleware global) e tradução de exceções em ProblemDetails/HTTP codes.

Boas práticas aplicadas no projeto
- Usar DTOs de entrada (`Requests`/`Inputs`) na WebApi e mapear explicitamente para `Commands`/`Queries` antes de invocar o Application. Isso evita expor 
objetos de aplicação diretamente e facilita versionamento e validação.
- Controllers devem ser finos: apenas mapear entrada, chamar o Facade/handler e retornar o resultado.
- Utilizar `ICategoryFacade` (ou facades equivalentes) quando o controller precisaria injetar 3+ dependências relacionadas — o facade orquestra chamadas aos handlers.
- Validar e normalizar dados na camada de apresentação (ex.: transformar strings, verificar limites). Validações de negócio ficam no Domain/Application quando necessário.
- Usar `CreatedAtAction` com `nameof(...)` para retorno 201 e garantir que exista a rota destino (por exemplo `GetCategoryById`).
- Registrar e utilizar um middleware global de exceções para padronizar respostas de erro (ProblemDetails).

Padrões e detalhes técnicos
- Roteamento e versionamento: os controladores usam rota base `api/v1/<resource>`; altere a versão conforme necessário.
- Model binding: use atributos `[FromBody]`, `[FromQuery]`, `[FromRoute]` explicitamente para clareza.
- Autenticação/Autorização: aplicar `Authorize` em controllers/ações quando necessário e configurar JWT/Identity no `Program.cs`.
- Documentação: habilitar Swagger/OpenAPI para facilitar testes e integração.

Registro de DI e startup
- O projeto expõe extensões de registro (ex.: `AddPresentation()`, `UsePresentation(...)`) que centralizam a configuração de controllers, swagger, filtros e middlewares.
- A `Program.cs` deve chamar as extensões do IoC: `builder.Services.AddPresentation().AddInfrastructure(...)` e no pipeline `app.UseGlobalExceptionHandler(); 
app.UsePresentation(env);`.

Tratamento de erros e validação
- Validações de entrada podem retornar `400 Bad Request` com detalhes; exceções de negócio (`KeyNotFoundException`, `ArgumentException`, etc.) são mapeadas 
pelo middleware global para códigos apropriados.
- Em produção, evitar vazar mensagens sensíveis no `detail` do ProblemDetails.

Mapeamento (DTO -> Command/Query)
- Mapear manualmente (métodos `ToCommand()`/`ToQuery()` nos DTOs de input) é simples e explícito; para mapeamentos mais complexos considere `AutoMapper`.

Testabilidade
- Testes de integração da WebApi podem ser feitos com `WebApplicationFactory<TProgram>`.
- Controllers finos facilitam testes unitários da lógica na camada `Application`.

Segurança e Operações
- Configurar CORS apenas para origens necessárias.
- Configurar limites de requisição, compressão e headers de segurança conforme política da empresa.

Exemplo de fluxo de uma requisição
1. Cliente faz POST `/api/v1/category` com payload JSON.
2. Controller recebe `CreateCategoryInput`, valida, chama `input.ToCommand()`.
3. Controller chama `categoryFacade.CreateAsync(command, cancellationToken)`.
4. Facade/handler executa lógica de aplicação e persiste via repositório (infra).
5. Controller retorna `201 Created` com `CreatedAtAction(nameof(GetCategoryById), new { id = output.Id }, output)`.

Observações finais
- A camada WebApi deve ser a menor possível em termos de lógica: sua função é adaptar o mundo externo para a aplicação e vice-versa.
- Documente convenções de rotas, versões e mapeamentos para manter consistência entre endpoints.