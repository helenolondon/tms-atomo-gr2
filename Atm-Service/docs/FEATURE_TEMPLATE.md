# Feature template — Como adicionar novas features

Este documento descreve o padrão do repositório e os passos recomendados para adicionar uma nova feature seguindo a arquitetura existente.

Sumário rápido
- Controladores: AtmApi\Controllers\<Feature>\*
- Requests/DTOs: AtmApi\Controllers\<Feature>\Requests\*
- Serviços: AtmService\* (ex.: SegurancaService.cs)
- Objetos de domínio / ODN: AtmServiceDominio\ODN\* e AtmServiceDominio\ObjetosDeDominio\*
- Persistência / Repositórios: AtmServicePersistencia\* / AtmServiceInfra\*
- Entrada única de serviços nos controllers: IAtmServices (this.services)

Padrões e convenções
1. Controller
   - Herda AtmController e usa this.services para acessar serviços.
   - Envolver a lógica em this.Executar(() => { ... }) para padronizar tratamento de erros.
   - Rotas: [Route("api/[area]/[controller]")] ou seguir o padrão atual do módulo.
   - Métodos HTTP: use atributos [HttpGet], [HttpPost], [Route("nome")] conforme necessidade.

2. Requests / DTOs
   - Colocar em AtmApi\Controllers\<Feature>\Requests.
   - Simples POCOs com propriedades públicas { get; set; }.
   - Nomear como <Ação>Request (ex.: LoginRequest, SalvarClienteRequest).

3. Serviços
   - Implementar em AtmService (ex.: ClientesService.cs).
   - Expôr pelo IAtmServices para uso pelos controllers.
   - Serviços devem lançar exceções de domínio/serviço (ex.: NaoEncontradoSE, ValidacaoDE) para serem tratadas por AtmController.Executar.

4. Domínio / ODN
   - Objetos de negócio em AtmServiceDominio.ODN e regras de validação em ObjetosDeDominio.
   - Converter entre ODN e ODP quando houver persistência.

5. Repositórios / Persistência
   - Adicionar interfaces em AtmServiceInfra e implementações em AtmServicePersistencia.
   - Atualizar IRepositorios para expor o novo repositório e inicializar em implementações.

6. Commit / Unidade de trabalho
   - Quando alterar dados persistidos, chamar this.services.Commit() no controller após operação bem-sucedida.

7. Erros e respostas HTTP
   - Lançar exceções específicas do domínio/serviço.
   - AtmController.Executar já traduz exceções para códigos HTTP apropriados (Unauthorized, NotFound, BadRequest, etc).

Passo-a-passo mínimo para adicionar uma feature
1. Criar Requests/DTOs:
   - AtmApi\Controllers\<Feature>\Requests\<Requests>.cs

2. Criar Controller:
   - AtmApi\Controllers\<Feature>\<Feature>Controller.cs
   - Injetar IAtmServices via construtor (se o projeto seguir DI explícita) ou usar padrão existente.
   - Usar this.Executar para encapsular lógica.

3. Implementar serviço:
   - AtmService\<Feature>Service.cs
   - Adicionar interface ao IAtmServices.

4. Adicionar camada de domínio/ODN:
   - AtmServiceDominio\ODN\<Feature>ODN.cs
   - Regras de validação em ObjetosDeDominio quando aplicável.

5. Persistência:
   - Criar I<Feature>Repositorio em AtmServiceInfra e implementação em AtmServicePersistencia.
   - Incluir no IRepositorios e inicializar na fábrica de repositórios.

6. Registrar/Configurar:
   - Se houver registro manual de serviços ou repositórios, atualizar o local apropriado (ver Dominios/Repositorios).
   - Atualizar qualquer mapeamento AutoMapper / serialização, se existir.

7. Testes:
   - Criar unit/integration tests seguindo o padrão do projeto (se houver pasta de testes).

Boas práticas extras
- Rotas claras e consistentes.
- Reaproveitar objetos e serviços existentes quando possível.
- Nomes em Português seguindo o repositório atual.
- Usar a propriedade Token/Authorization de forma consistente; helper AtmController.ObterTokenDoRequest() quando apropriado.

Como adicionar o MD ao projeto no Visual Studio
- No __Solution Explorer__ clique com o botão direito na pasta desejada e use __Add > New Item__ para criar um arquivo Markdown.
- Verifique em __Properties__ > __Build Action__ que o arquivo seja "Content" se quiser incluí-lo no pacote.
