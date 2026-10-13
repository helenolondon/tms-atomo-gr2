---
description: "Use when creating, scaffolding, or wiring a new Angular business module in Atm-Front, or when asked to follow the Clientes module architecture."
applyTo:
  - "src/app/modulos/**"
  - "src/app/app.module.ts"
  - "src/app/app-routing.module.ts"
---

# Criar módulos de negócio no frontend

Use esta instrução como registro do padrão de módulos já verificado no projeto. Não faça uma varredura ampla do workspace nem releia todo o módulo Clientes a cada solicitação. Use os padrões abaixo diretamente; consulte apenas o arquivo de referência relevante quando precisar confirmar uma API de componente ou um fluxo que ainda não esteja descrito.

Referências verificadas:

- `src/app/modulos/marcas/`: exemplo recente de módulo CRUD enxuto com `AtmGerenciadorComponent`, formulário reativo, serviço HTTP, modelos e integração central.
- `src/app/modulos/clientes/`: referência de módulo com mais campos, validadores, formulários, resolvers e colaboração com outros módulos.
- `src/app/modulos/empresas/`: referência alternativa de CRUD simples, inclusive callbacks do gerenciador.
- `src/app/infra/atom/components/atm-gerenciador/atm-gerenciador.component.ts` e `.html`: contrato do gerenciador e carregamento dinâmico dos formulários.
- `src/app/infra/atom/components/atm-propriedades/IAtm-Propriedades-Component.ts`: contrato `setModel`.
- `src/app/infra/services/http-global-settings.service.ts`: URL-base e tratamento de erros HTTP.
- `src/styles.scss`: estilos globais compartilhados, incluindo alinhamento e largura das ações de formulário.

Siga o padrão, não copie bugs, dados de Clientes/Marcas, código de depuração ou detalhes específicos de outro negócio. Não altere o backend para uma tarefa somente de frontend.

## Organização

Crie a funcionalidade em `src/app/modulos/<plural-do-modulo>/`, mantendo cada responsabilidade em sua pasta:

- `gerenciador-<plural>/`: tela de pesquisa/listagem; componente, template e SCSS.
- `propriedades-<singular>/`: formulário de inclusão/edição; componente, template e SCSS.
- `services/`: serviço Angular responsável pelas chamadas HTTP do recurso.
- `requests/` e `responses/`: modelos das entradas e respostas da API, com os nomes concretos acordados com o endpoint.
- `odts/`: somente quando a tela precisar de um modelo de leitura/projeção próprio.
- `data-resolvers/`: somente para dados necessários antes de ativar uma rota.

Não crie pastas vazias nem um `NgModule` por funcionalidade: os componentes de negócio seguem declarados no `src/app/app.module.ts`, como Clientes e Empresas.

## Listagem e formulário

- Use `AtmGerenciadorComponent` (`<atm-gerenciador>`) para a tela de gerenciamento quando o fluxo for de lista com inclusão, edição e exclusão. Configure fonte de dados, colunas, título e callbacks conforme o contrato do componente compartilhado.
- No gerenciador, obtenha a referência com `@ViewChild(AtmGerenciadorComponent, { static: true })`; em `ngAfterViewInit`, atribua `componentFactory` usando `ComponentFactoryResolver` para o componente de propriedades e implemente `AfterViewInit`.
- O componente de propriedades deve implementar `IAtmPropriedadesComponent` e fornecer `setModel(model)` para carregar tanto inclusão quanto edição. A criação, cancelamento, edição e gravação devem funcionar por meio desse contrato e do `AtmGerenciadorComponent`.
- No Angular 8, inclua em `AppModule.entryComponents` todo componente de propriedades instanciado dinamicamente pelo gerenciador, além de declará-lo em `declarations`. Sem isso, Novo/Editar não conseguem criar a aba de propriedades.
- Alinhe as ações de formulário consistentemente em todos os módulos usando as classes globais definidas em `src/styles.scss`: `form-actions` e `form-action` deixam Cancelar à esquerda e Salvar à direita, com largura igual; para formulário com apenas Salvar, use também `form-actions-end`. Não crie regras locais divergentes para dimensões ou alinhamento dos botões.
- Todas as telas de propriedades devem aplicar `class="property-screen"` ao elemento raiz `<atm-component>`. O estilo global em `src/styles.scss` reserva `10px` de padding uniforme nos quatro lados, incluindo a borda inferior; não substitua por espaçamento local desigual.
- Em todo `<atm-fieldset>`, o último campo deve manter `10px` de padding inferior (o dobro dos `5px` dos demais campos), aplicado globalmente em `src/styles.scss` ao último `.grid-item` do último contêiner do fieldset. Preserve a estrutura de contêiner/campo para o espaçamento se aplicar.
- Use formulários reativos, validações explícitas e controles compartilhados de `infra/atom`. Marque obrigatórios somente os campos definidos como obrigatórios pelo requisito/contrato; para texto opcional limitado, aplique `Validators.maxLength` sem `Validators.required`.
- Para propriedades de texto longo, reutilize o controle compartilhado `<atm-text-area>` (implementado como `ControlValueAccessor`); não introduza um `<textarea>` nativo com aparência e integração locais.
- Mantenha limites de caracteres e propriedades camelCase do contrato, respeitando o JSON real da API (por exemplo, `codMarca`, `nome`, `descricao`, `observacao`). Envie texto vazio quando o banco/endpoint exigir uma string não nula e a propriedade for opcional na interface.
- Não suponha que a resposta é um objeto simples ou aninhado: confirme o contrato apenas na API relacionada ao novo módulo. Trate campos condicionais e lookups somente quando fizerem parte do requisito.

## Serviço e integração

- Injete `HttpClient` e use `HttpGlobalSettingsService.ServerUrl` para compor endpoints; trate falhas HTTP pelo `HandleHttpError`, seguindo os serviços existentes.
- Defina métodos tipados para listar, obter, salvar e excluir somente nas operações necessárias. Use `HttpParams` para filtros opcionais e modelos em `requests/` e `responses/` quando aplicável.
- Reutilize serviços de outros módulos para dados compartilhados (por exemplo, endereços e dados fiscais), em vez de duplicar endpoints.
- Acrescente resolvers e guards à rota somente quando necessários; prefira buscar dados do formulário pela rota/resolvers já estabelecidos quando esse for o padrão do fluxo.
- Exiba retorno de sucesso e erros de validação/servidor usando `DialogsService` e os controles de mensagem compartilhados; não engula erros nem mostre sucesso antes da resposta da API.

## Registro e verificação

- Declare os componentes em `src/app/app.module.ts`; para componentes dinâmicos, registre-os também em `entryComponents` (Angular 8). Registre a rota e os imports, guards e resolvers necessários em `src/app/app-routing.module.ts`.
- Acrescente o acesso no menu de cadastros em `src/app/site/l-menu/l-menu.component.ts` (`AtmMenuItem`) quando o módulo fizer parte da navegação do usuário.
- Não edite o backend para uma tarefa de frontend. Se a solicitação incluir API/persistência, examine e siga o módulo equivalente de Clientes nas camadas `AtmApi`, `AtmService`, `AtmServiceDominio` e `AtmServicePersistencia`; mantenha contratos alinhados entre frontend e backend.
- Execute a validação mais específica disponível; para uma verificação geral do frontend, use `npm run build` e/ou `npm run lint` a partir de `Atm-Front`.
