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
# Feature template — Como adicionar novas features (MANDATÓRIO)

Este documento descreve o padrão do repositório e os passos obrigatórios para adicionar uma nova feature seguindo a arquitetura existente.
Sempre que for solicitado "criar nova funcionalidade", siga estes passos exatamente.

Resumo da arquitetura
- API (controllers): AtmApi\Controllers\<Feature>\*
- Requests/DTOs: AtmApi\Controllers\<Feature>\Requests\*
- Serviços de aplicação: AtmService\<Feature>\* (interface + implementação)
- Objetos de domínio (ODN) e regras: AtmServiceDominio\ODN\* e AtmServiceDominio\ObjetosDeDominio\*
- Persistência:
  - ODP (objetos de persistência): AtmServicePersistencia\ODP\*
  - Repositório interface: AtmServiceInfra\ (I<Feature>Repositorio)
  - Repositório implementação: AtmServicePersistencia\<Feature>Repositorio.cs
- Fabricação / Registro:
  - Adicionar repositório em IRepositorios e Repositorios (factory).
  - Adicionar domínio (Dominios) para expor I<Feature>OD.
  - Adicionar service em IAtmServices e AtmServices.
  - Adicionar DbSet em AtmDbContext quando existir ODP.

Regras obrigatórias (sempre)
1. Crie um Request POCO
   - Caminho: AtmApi\Controllers\<Feature>\Requests\<Ação>Request.cs
   - Use propriedades públicas simples e DataAnnotations quando necessário.

2. Crie o Controller
   - Caminho: AtmApi\Controllers\<Feature>\<Feature>Controller.cs
   - Herde AtmController.
   - Encapsule lógica em this.Executar(() => { ... }).
   - Use this.services.<Feature>Service para lógica de aplicação.
   - Sempre chamar this.services.Commit() após operações que alteram persistência.

3. Crie o Service de aplicação
   - Interface: AtmService\<Feature>\I<Feature>Service.cs
   - Implementação: AtmService\<Feature>\<Feature>Service.cs
   - Service deve depender do domínio (I<Feature>OD) e expor métodos necessários.
   - Adicione a interface em IAtmServices (AtmService\IAtmServices.cs) e a propriedade de implementação em AtmServices.cs.

4. Crie o domínio (OD)
   - ODN: AtmServiceDominio\ODN\<Feature>ODN.cs (Objeto de Negócio)
   - OD (objeto de domínio): AtmServiceDominio\ObjetosDeDominio\<Feature>OD.cs
     - Herde AtmObjetoDominio<TODP> conforme padrão.
     - Implementar Salvar/Excluir e validações.
   - No Dominios.cs adicione propriedade I<Feature>OD que instancia new <Feature>OD(reps.<Feature>Repositorio).

5. Crie ODP (Objeto de Persistência) e repositório
   - ODP: AtmServicePersistencia\ODP\<Feature>ODP.cs (classe simples com propriedades mapeadas)
   - Interface repositório: AtmServiceInfra\I<Feature>Repositorio.cs (herda IRepositorio<<Feature>ODP>)
   - Implementação: AtmServicePersistencia\<Feature>Repositorio.cs (herda Repositorio<<Feature>ODP>)
   - Adicione DbSet<<Feature>ODP> Testes em AtmDbContext.cs se for gerenciar via EF.

6. Atualize a fábrica de repositórios
   - IRepositorios (AtmServicePersistencia\IRepositorios.cs): adicione propriedade I<Feature>Repositorio <Feature>Repositorio { get; }
   - Repositorios.cs: implemente a propriedade lazy-iniciando new <Feature>Repositorio(_context).

7. Atualize Dominios para expor o OD
   - Dominios.cs: adicione campo I<Feature>OD e propriedade pública que cria a instância usando reps.<Feature>Repositorio.

8. Atualize IAtmServices e AtmServices
   - IAtmServices: adicione I<Feature>Service <Feature>Service { get; }
   - AtmServices: implemente lazy-init do service usando dom.<Feature>OD e instancia a implementação do service.

9. Migração / Banco
   - Se adicionou ODP e DbSet, criar migration EF Core e aplicar, ou garantir que tabela exista (TS_TESTE no seu caso).
   - Conferir sequências (TS_SEQUENCIAS) se Id for gerado via sequência; implementar AntesDeSalvar no OD para obter sequência se necessário.

10. Testes e documentação
    - Adicionar testes unitários/integrados.
    - Atualizar docs/ (ex.: AtmApi/Docs/<feature>.md) com rota e exemplos request/response.
    - Incluir exemplo de chamada e erros esperados.

Checklist prático (copiar/colar ao abrir PR)
- [ ] Criado Request em AtmApi\Controllers\<Feature>\Requests
- [ ] Criado Controller que herda AtmController e usa this.Executar
- [ ] Criada interface e implementação do Service em AtmService
- [ ] Adicionado service em IAtmServices e AtmServices
- [ ] Criado ODN em AtmServiceDominio\ODN
- [ ] Criado OD em AtmServiceDominio\ObjetosDeDominio e exposto em Dominios.cs
- [ ] Criado ODP em AtmServicePersistencia\ODP
- [ ] Criada interface I<Feature>Repositorio em AtmServiceInfra
- [ ] Criado <Feature>Repositorio em AtmServicePersistencia e registrado em Repositorios.cs
- [ ] Atualizado AtmDbContext.cs (DbSet) quando aplicável
- [ ] Criada migration / conferida tabela no banco
- [ ] Adicionados testes mínimos e documentação de endpoint
- [ ] Build Solution — sem erros

Observações e boas práticas
- Siga nomes e namespaces existentes.
- Use exceções de domínio (NaoEncontradoSE, ValidacaoDE, etc.) para erros esperados — AtmController.Executar traduz para HTTP adequados.
- Preserve transações via this.services.Commit() (Dominios.Salvar).
- Prefira inserir lógica de persistência no repositório; controllers apenas orquestram.
- Se dúvida sobre mapeamento EF ou sequências, verificar padrões existentes (ex.: TD_ARQUIVOS_SEQ).

Se desejar, ao pedir “criar nova funcionalidade X” eu gero automaticamente:
- skeleton completo (arquivos e trechos a alterar em IAtmServices, Repositorios, Dominios, AtmDbContext)
- migrations EF Core iniciais (se você confirmar detalhes da tabela)
Peça “criar feature <Nome> com campos …” e eu gero o patch pronto seguindo este checklist.
