# Diretrizes do frontend

- O frontend fica em `Atm-Front` e usa Angular 8, TypeScript 3.5, RxJS 6 e TSLint. Preserve essas versões e os padrões compatíveis já usados no projeto.
- As funcionalidades ficam em `src/app/modulos`; componentes de negócio são declarados no `AppModule` e as rotas são registradas em `app-routing.module.ts`.
- Reutilize componentes e serviços compartilhados de `src/app/infra` e módulos existentes antes de criar alternativas locais.
- Ao criar um módulo de negócio, siga `.github/instructions/novo-modulo-angular.instructions.md` e use `src/app/modulos/clientes` como referência estrutural.
- Não altere `Atm-Service` para uma tarefa apenas de interface. Para uma implementação full-stack, siga também as camadas existentes de API, serviços, domínio e persistência.
