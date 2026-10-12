# Instruções — Módulo Autenticação

Este documento descreve como o módulo de autenticação está organizado e como adicionar endpoints relacionados.

Localização e padrão
- Controller: AtmApi\Controllers\Autenticacao\AutenticacaoController.cs
- Requests: AtmApi\Controllers\Autenticacao\Requests\ (ex.: LoginRequest, AlterarSenhaRequest)
- Serviço: AtmService\Seguranca\SegurancaService.cs
- Domínio: AtmServiceDominio\... (ODN/OD)
- Repositório: AtmServiceInfra.Seguranca e AtmServicePersistencia implementações

Endpoints existentes (exemplo)
- POST api/auth/autenticacao/login
  - Body: LoginRequest { Login, Senha }
  - Retorno: 200 OK com LoginODT (Usuario, Email, Login, Token) ou 401 Unauthorized em credenciais inválidas.
  - Fluxo: controller obtém usuário via UsuariosService.ObterUsuarioPorLogin, chama SegurancaService.Login(..., out token), this.services.Commit().

- GET api/auth/autenticacao/verify
  - Header: token (ou usar Authorization se padronizar)
  - Retorno: 200 OK se token válido, 401 Unauthorized caso contrário.
  - Fluxo: SegurancaService.VerificarToken(token)

- POST api/auth/autenticacao/alterar-senha
  - Body: AlterarSenhaRequest { Login, Senha, NovaSenha, ConfirmaSenha }
  - Valida usuário, chama SegurancaService.AlterarSenha(...), this.services.Commit(), retorna 200 OK.

Regras e pontos importantes ao adicionar endpoints de autenticação
1. Seguir o mesmo namespace e estrutura de pastas.
2. Usar tipos de Request simples e colocá-los em Requests\.
3. Tratar exceções específicas:
   - Ex.: LoginInvalidoSE / LoginInvalidoDE → retornar Unauthorized.
   - Lançar AtmServiceNaoEncontradoSE quando usuário não for encontrado.
4. Uso de token:
   - Seguir padrão existente (ver método Verify).
   - Para leitura do token dentro de controllers, preferir AtmController.ObterTokenDoRequest() se necessário.
5. Commit:
   - Sempre chamar this.services.Commit() quando a operação altera estado persistido.
6. Testes:
   - Garantir casos: login inválido, login válido/tokens gerados, token inválido/verificação, alteração de senha com validações.

Como documentar um novo endpoint de autenticação
- Adicione entrada em AtmApi/Docs/AUTENTICACAO_INSTRUCOES.md (neste mesmo arquivo) descrevendo:
  - Rota completa
  - Método HTTP
  - Request/Response (exemplos JSON)
  - Exceções esperadas e códigos HTTP
  - Dependências (serviços/repositórios usados)
  - Pontos de integração (ex.: front-end espera Token no header Authorization)

Exemplo rápido (documentar no arquivo)
- Rota: POST api/auth/autenticacao/login
- Request:
  {
    "Login": "usuario",
    "Senha": "senha"
  }
- Success Response (200):
  {
    "Usuario": { ... },
    "Email": "...",
    "Login": "...",
    "Token": "ey..."
  }
- Error Response (401):
  "Credenciais inválidas"

Adicionar este documento ao projeto
- Salve este arquivo em AtmApi/Docs/ e, se desejar, adicione links a um README principal.
