# Camada de domínio (coração do software)

Resumo
- A camada de domínio representa o modelo do negócio: entidades, regras e comportamentos que descrevem o estado
  e as invariantes do sistema. Esta camada é independente de infraestrutura e frameworks — ela descreve o problema,
  não a tecnologia que o resolve.

Propósito na Clean Architecture
- Segregar a lógica de negócio das preocupações de aplicação e infraestrutura.
- As dependências apontam para dentro: nada na camada de domínio depende de infra ou apresentação.
- Fornecer um conjunto testável e consistente de objetos de domínio (entidades, value objects e regras) que garantam
  que o estado do sistema esteja sempre válido.

Responsabilidades desta camada
- Modelar entidades e seus comportamentos (métodos que representam operações de negócio).
- Validar invariantes do domínio (validações de negócio, não validações de entrada HTTP).
- Definir interfaces que representam contratos com a infraestrutura (repositórios, unit of work), sem fornecer
  implementações.

O que foi implementado nesta camada
- Pastas e artefatos principais:
  - `Entities`: classes que representam o modelo do domínio (ex.: `Category`, `Product`).
  - `Interfaces`: abstrações como `ICategoryRepository` usadas pela aplicação para acessar dados.
  - `Validation`: regras e utilitários de validação de domínio.
  - `Base` de entidades: classe base para propriedades comuns (Id, CreatedAt, UpdatedAt).

- Padrões e práticas adotadas:
  - Uso de construtores parametrizados e propriedades com setters restritos para manter invariantes.
  - Entidades com métodos de comportamento (por exemplo `Update`, `Activate`) em vez de apenas DTOs anêmicos.
  - Exposição de interfaces para repositórios; a implementação fica na infraestrutura.

- Testes e qualidade:
  - O domínio foi projetado para ser testável isoladamente — escreva testes unitários para invariantes e comportamentos
    do modelo.

Boas práticas e limites
- Não colocar código de infraestrutura (EF, SQL, HTTP, logging específico) aqui.
- Validações de contrato (ex.: formato de JSON, campos obrigatórios na API) devem ficar na camada de apresentação.
- Regras de negócio complexas devem residir no domínio ou em serviços de domínio, não na camada de aplicação.

Notas finais
- Esta camada deve permanecer estável e independente para permitir evolução do restante do sistema (trocar DB,
  frameworks ou UI) sem afetar o núcleo do negócio.

