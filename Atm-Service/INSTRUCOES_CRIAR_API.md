# Feature: Criar API

Este documento descreve o passo a passo utilizado para criar a feature `Marcas` e deve ser seguido como padrão para criação de **qualquer nova API** na solução `Atm-Service`.

## Diretrizes de execução (otimização)

- Usar respostas curtas e objetivas para reduzir consumo de tokens.
- Evitar repetição de contexto já conhecido.
- Reutilizar padrões já existentes da solução sem reexplicar blocos completos quando não for necessário.
- **Não criar arquivos `.sql`** durante a implementação da API.
- Quando necessário, apenas informar o modelo esperado para o banco, sem gerar script.

A arquitetura da solução é dividida em camadas:

- `AtmServicePersistencia` (Infra / Repositórios / ODP - Objetos de Persistência / EF)
- `AtmServiceDominio` (Domínio / ODN - Objetos de Negócio / Objetos de Domínio)
- `AtmService` (Serviços de aplicação / ODT - Objetos de Transporte)
- `AtmApi` (Controllers da API)

Todo fluxo de dados segue sempre o mesmo caminho:

```
Controller (AtmApi)
   -> Service (AtmService) - usa ODT
      -> Objeto de Domínio - OD (AtmServiceDominio) - usa ODN
         -> Repositório (AtmServicePersistencia) - usa ODP
            -> AtmDbContext (EF)
```

Use como exemplo de referência a feature `Marcas`, descrita a seguir.

---

## Passo 1 - Criar o ODP (Objeto de Persistência)

Local: `AtmServicePersistencia/ODP/<Nome>ODP.cs`

- Deve herdar de `ObjetoDePersistencia` (que já contém `DataCad`, `UsuarioCad`, `DataAlt`, `UsuarioAlt`).
- Decorar a classe com `[Table("NOME_DA_TABELA")]`.
- Decorar a chave primária com `[Key]`.
- Usar `[Column("NOME_DA_COLUNA")]` em todas as propriedades para mapear explicitamente para o nome real da coluna no banco.
- Usar `[Required]` e `[MaxLength(n)]` conforme as regras de obrigatoriedade e tamanho das colunas.

Exemplo (`MarcaODP.cs`):

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmServiceInfra.ODP
{
    [Table("TD_MARCAS")]
    public class MarcaODP : ObjetoDePersistencia
    {
        [Key]
        [Column("CODMARCA")]
        public int CodMarca { get; set; }

        [Required]
        [MaxLength(250)]
        [Column("NOME")]
        public string Nome { get; set; }

        [Required]
        [MaxLength(250)]
        [Column("DESCRICAO")]
        public string Descricao { get; set; }

        [Required]
        [MaxLength(500)]
        [Column("OBSERVACAO")]
        public string Observacao { get; set; }
    }
}
```

**Importante**: sempre conferir a estrutura real da tabela antes de definir os campos (nomes de colunas, tamanhos e obrigatoriedade).

---

## Passo 2 - Registrar o ODP no `AtmDbContext`

Local: `AtmServicePersistencia/EF/AtmDbContext.cs`

Adicionar uma propriedade `DbSet<T>` para a nova entidade. Sem esse passo o Entity Framework não reconhece o tipo e ocorre o erro:
`Cannot create a DbSet for '<Tipo>' because this type is not included in the model for the context.`

```csharp
public DbSet<MarcaODP> Marcas { get; set; }
```

---

## Passo 3 - Criar o repositório

Local: `AtmServicePersistencia/<Feature>/I<Feature>Repositorio.cs` e `<Feature>Repositorio.cs`

- A interface deve herdar de `IRepositorio<TODP>`.
- A implementação deve herdar de `Repositorio<TODP>` e implementar a interface.
- Adicionar métodos de consulta específicos da feature (ex: `Listar()`), usando `this.dbSet`.

Exemplo (`IMarcasRepositorio.cs`):

```csharp
using AtmServiceInfra.ODP;
using System.Collections.Generic;

namespace AtmServiceInfra.Marcas
{
    public interface IMarcasRepositorio : IRepositorio<MarcaODP>
    {
        List<MarcaODP> Listar();
    }
}
```

Exemplo (`MarcasRepositorio.cs`):

```csharp
using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceInfra.Marcas
{
    public class MarcasRepositorio : Repositorio<MarcaODP>, IMarcasRepositorio
    {
        public MarcasRepositorio(AtmDbContext context) : base(context)
        {
        }

        public List<MarcaODP> Listar()
        {
            return this.dbSet.OrderBy(marca => marca.Nome).ToList();
        }
    }
}
```

---

## Passo 4 - Registrar o repositório em `IRepositorios` / `Repositorios`

Local: `AtmServicePersistencia/IRepositorios.cs` e `AtmServicePersistencia/Repositorios.cs`

- Adicionar a propriedade da interface do repositório em `IRepositorios`.
- Implementar em `Repositorios` com inicialização *lazy* (criação sob demanda, cacheada em campo privado).

```csharp
// IRepositorios.cs
IMarcasRepositorio MarcasRepositorio { get; }
```

```csharp
// Repositorios.cs
private IMarcasRepositorio marcasRepositorio;

public IMarcasRepositorio MarcasRepositorio
{
    get
    {
        if (this.marcasRepositorio == null)
        {
            this.marcasRepositorio = new MarcasRepositorio(_context);
        }

        return this.marcasRepositorio;
    }
}
```

---

## Passo 5 - Criar o ODN (Objeto de Negócio)

Local: `AtmServiceDominio/ODN/<Nome>ODN.cs`

- Deve herdar de `ObjetoDeNegocio`.
- Implementar `ToODP` e `FromODP` fazendo o de/para com o ODP correspondente.
- Propriedades devem espelhar as propriedades de negócio (podem ter nomes diferentes do ODP, mas aqui seguimos os mesmos nomes).

Exemplo (`MarcaODN.cs`):

```csharp
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;

namespace AtmServiceDominio.ODN
{
    public class MarcaODN : ObjetoDeNegocio
    {
        public int CodMarca { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            var odp = destino as MarcaODP ?? new MarcaODP();
            odp.CodMarca = this.CodMarca;
            odp.Nome = this.Nome;
            odp.Descricao = this.Descricao;
            odp.Observacao = this.Observacao;
            return base.ToODP(odp);
        }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            if (obj == null)
            {
                return;
            }

            var odp = obj as MarcaODP;
            this.CodMarca = odp.CodMarca;
            this.Nome = odp.Nome;
            this.Descricao = odp.Descricao;
            this.Observacao = odp.Observacao;
            base.FromODP(odp);
        }
    }
}
```

---

## Passo 6 - Criar o Objeto de Domínio (OD)

Local: `AtmServiceDominio/ObjetosDeDominio/<Feature>/I<Feature>OD.cs` e `<Feature>OD.cs`

- A interface deve herdar de `IAtmObjetoDominio`.
- A implementação deve herdar de `AtmObjetoDominio<TODP>` e implementar a interface.
- No construtor, recebe o repositório da feature.
- Sobrescrever `ObterID` para retornar a chave primária do ODN (usada pelo `Salvar` genérico da classe base para decidir entre inserir/atualizar).
- Sobrescrever `AntesDeSalvar` para gerar o próximo valor de sequência ao inserir (`ObterProximoValorDeSequencia`), seguindo o padrão `TD_<TABELA>_SEQ`.
- Adicionar métodos de consulta (`Listar`, `ObterPorId`, etc).

Exemplo (`IMarcasOD.cs`):

```csharp
using AtmServiceDominio.ODN;
using System.Collections.Generic;

namespace AtmServiceDominio.ObjetosDeDominio.Marcas
{
    public interface IMarcasOD : IAtmObjetoDominio
    {
        List<MarcaODN> Listar();
        MarcaODN ObterPorId(int codMarca);
    }
}
```

Exemplo (`MarcasOD.cs`):

```csharp
using AtmServiceDominio.ODN;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra;
using AtmServiceInfra.Marcas;
using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceDominio.ObjetosDeDominio.Marcas
{
    public class MarcasOD : AtmObjetoDominio<MarcaODP>, IMarcasOD
    {
        public MarcasOD(IMarcasRepositorio repositorio) : base(repositorio)
        {
        }

        public List<MarcaODN> Listar()
        {
            return (this.repositorio as IMarcasRepositorio)
                .Listar()
                .Select(marca => new MarcaODN
                {
                    CodMarca = marca.CodMarca,
                    Nome = marca.Nome,
                    Descricao = marca.Descricao,
                    Observacao = marca.Observacao
                })
                .ToList();
        }

        public MarcaODN ObterPorId(int codMarca)
        {
            var odp = this.repositorio.Obter(codMarca);
            return odp == null ? null : new MarcaODN
            {
                CodMarca = odp.CodMarca,
                Nome = odp.Nome,
                Descricao = odp.Descricao,
                Observacao = odp.Observacao
            };
        }

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            var marca = obj as MarcaODN;
            return marca != null && marca.CodMarca > 0 ? new object[] { marca.CodMarca } : null;
        }

        protected internal override void AntesDeSalvar(MarcaODP obj, bool inserindo)
        {
            base.AntesDeSalvar(obj, inserindo);

            if (inserindo)
            {
                obj.CodMarca = this.repositorio.ObterProximoValorDeSequencia("TD_MARCAS_SEQ");
            }
        }
    }
}
```

---

## Passo 7 - Registrar o OD em `Dominios`

Local: `AtmServiceDominio/Dominios.cs`

- Adicionar campo privado e propriedade pública com inicialização *lazy*.
- Definir `NomeSingular` e `NomePlural` (usados em mensagens de erro/validação).

```csharp
IMarcasOD marcasOD = null;

public IMarcasOD MarcasOD
{
    get
    {
        if (marcasOD == null)
        {
            marcasOD = new MarcasOD(reps.MarcasRepositorio);
            marcasOD.NomeSingular = "Marca";
            marcasOD.NomePlural = "Marcas";
        }

        return marcasOD;
    }
}
```

---

## Passo 8 - Criar o ODT (Objeto de Transporte / DTO da API)

Local: `AtmService/ODT/<Nome>ODT.cs`

- Classe simples (POCO), sem herança de domínio.
- Construtor padrão + construtor que recebe o ODN.
- Métodos `FromODN` e `ToODN` para conversão.

Exemplo (`MarcaODT.cs`):

```csharp
using AtmServiceDominio.ODN;

namespace AtmService.ODT
{
    public class MarcaODT
    {
        public int CodMarca { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }

        public MarcaODT()
        {
        }

        public MarcaODT(MarcaODN marca)
        {
            FromODN(marca);
        }

        public void FromODN(MarcaODN marca)
        {
            if (marca == null)
            {
                return;
            }

            this.CodMarca = marca.CodMarca;
            this.Nome = marca.Nome;
            this.Descricao = marca.Descricao;
            this.Observacao = marca.Observacao;
        }

        public MarcaODN ToODN()
        {
            return new MarcaODN
            {
                CodMarca = this.CodMarca,
                Nome = this.Nome,
                Descricao = this.Descricao,
                Observacao = this.Observacao
            };
        }
    }
}
```

---

## Passo 9 - Criar o Serviço de aplicação

Local: `AtmService/<Feature>/I<Feature>Service.cs` e `<Feature>Service.cs`

- A interface define os métodos expostos para a API (sempre em termos de ODT).
- A implementação herda de `AtmService` (classe base de serviços, que fornece `ExecutarAcaoDeDominio` para tratamento de erros de domínio) e implementa a interface.
- Recebe o(s) OD(s) necessário(s) no construtor.
- Converte ODN <-> ODT.

Exemplo (`IMarcasService.cs`):

```csharp
using AtmService.ODT;
using System.Collections.Generic;

namespace AtmService.Marcas
{
    public interface IMarcasService
    {
        List<MarcaODT> Listar();
        MarcaODT ObterPorId(int codMarca);
        MarcaODT Salvar(MarcaODT marca);
        void Excluir(int codMarca);
    }
}
```

Exemplo (`MarcasService.cs`):

```csharp
using AtmService.ODT;
using AtmServiceDominio.ObjetosDeDominio.Marcas;
using AtmServiceDominio.ODN;
using System.Collections.Generic;
using System.Linq;

namespace AtmService.Marcas
{
    public class MarcasService : AtmService, IMarcasService
    {
        private readonly IMarcasOD marcasOD;

        public MarcasService(IMarcasOD marcasOD)
        {
            this.marcasOD = marcasOD;
        }

        public List<MarcaODT> Listar()
        {
            return this.marcasOD.Listar().Select(marca => new MarcaODT(marca)).ToList();
        }

        public MarcaODT ObterPorId(int codMarca)
        {
            MarcaODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                var marca = this.marcasOD.ObterPorId(codMarca);
                if (marca != null)
                {
                    resultado = new MarcaODT(marca);
                }
            });
            return resultado;
        }

        public MarcaODT Salvar(MarcaODT marca)
        {
            MarcaODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                resultado = new MarcaODT(this.marcasOD.Salvar(marca.ToODN()) as MarcaODN);
            });
            return resultado;
        }

        public void Excluir(int codMarca)
        {
            this.ExecutarAcaoDeDominio(() => this.marcasOD.Excluir(codMarca));
        }
    }
}
```

---

## Passo 10 - Registrar o serviço em `IAtmServices` / `AtmServices`

Local: `AtmService/IAtmServices.cs` e `AtmService/AtmServices.cs`

```csharp
// IAtmServices.cs
IMarcasService MarcasService { get; }
```

```csharp
// AtmServices.cs
private IMarcasService marcasService;

public IMarcasService MarcasService
{
    get
    {
        if (this.marcasService == null)
        {
            this.marcasService = new MarcasService(dom.MarcasOD);
        }

        return this.marcasService;
    }
}
```

---

## Passo 11 - Criar o Controller da API

Local: `AtmApi/Controllers/<Feature>/<Feature>Controller.cs`

- Deve herdar de `AtmController`.
- Decorar com `[Route("api/<rota>")]` e `[ApiController]`.
- **Sempre** decorar com `[AutorizarUsuarioLogado]` para exigir autenticação, assim como as demais features da API (exceto rotas públicas explicitamente de login/autenticação).
- Usar `this.services.<Feature>Service` (nunca instanciar o service/domínio diretamente no controller).
- Usar `this.Executar(() => { ... })` para tratamento padronizado de erros (igual às demais controllers).
- Chamar `this.services.Commit()` após qualquer operação de escrita (`POST`/`DELETE`).

Exemplo (`MarcasController.cs`):

```csharp
using AtmApi.Controllers.Autenticacao.ActionFilters;
using AtmService.ODT;
using Microsoft.AspNetCore.Mvc;

namespace AtmApi.Controllers.Marcas
{
    [Route("api/marcas")]
    [ApiController]
    [AutorizarUsuarioLogado]
    public class MarcasController : AtmController
    {
        [HttpGet]
        public IActionResult ListarMarcas()
        {
            return this.Executar(() => Ok(this.services.MarcasService.Listar()));
        }

        [HttpGet("{codMarca:int}")]
        public IActionResult ObterMarca(int codMarca)
        {
            return this.Executar(() =>
            {
                var marca = this.services.MarcasService.ObterPorId(codMarca);
                if (marca == null)
                {
                    return NotFound();
                }

                return Ok(marca);
            });
        }

        [HttpPost]
        public IActionResult SalvarMarca(MarcaODT request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            return this.Executar(() =>
            {
                var marca = this.services.MarcasService.Salvar(request);
                this.services.Commit();
                return Ok(marca);
            });
        }

        [HttpDelete("{codMarca:int}")]
        public IActionResult ExcluirMarca(int codMarca)
        {
            return this.Executar(() =>
            {
                this.services.MarcasService.Excluir(codMarca);
                this.services.Commit();
                return NoContent();
            });
        }
    }
}
```

---

## Passo 12 - Compilar e validar

- Rodar o build da solução e corrigir eventuais erros de compilação.
- Conferir se o novo `DbSet` foi registrado no `AtmDbContext` (evita o erro `Cannot create a DbSet for '<Tipo>'`).
- Conferir se o controller está protegido com `[AutorizarUsuarioLogado]`.
- Conferir se os nomes de coluna (`[Column]`) batem exatamente com a estrutura real da tabela no banco (evita erros como `Field '<COLUNA>' doesn't have a default value`).

---

## Resumo do checklist

1. [ ] Criar `<Nome>ODP` em `AtmServicePersistencia/ODP`, com `[Table]`, `[Key]` e `[Column]` mapeando a tabela real.
2. [ ] Registrar `DbSet<<Nome>ODP>` em `AtmDbContext`.
3. [ ] Criar `I<Feature>Repositorio` / `<Feature>Repositorio` em `AtmServicePersistencia/<Feature>`.
4. [ ] Registrar o repositório em `IRepositorios` / `Repositorios`.
5. [ ] Criar `<Nome>ODN` em `AtmServiceDominio/ODN`.
6. [ ] Criar `I<Feature>OD` / `<Feature>OD` em `AtmServiceDominio/ObjetosDeDominio/<Feature>`.
7. [ ] Registrar o OD em `Dominios`.
8. [ ] Criar `<Nome>ODT` em `AtmService/ODT`.
9. [ ] Criar `I<Feature>Service` / `<Feature>Service` em `AtmService/<Feature>`.
10. [ ] Registrar o serviço em `IAtmServices` / `AtmServices`.
11. [ ] Criar `<Feature>Controller` em `AtmApi/Controllers/<Feature>`, protegido com `[AutorizarUsuarioLogado]`.
12. [ ] Compilar a solução e validar todos os pontos acima.
