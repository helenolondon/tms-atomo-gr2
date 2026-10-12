using AtmService.ODT;

namespace AtmApi.Controllers.Usuarios.Requests
{
    /// <summary>
    /// Representa os dados de um novo usuário com senha a serem salvos
    /// </summary>
    public class SalvarNovoUsuarioRequest: SalvarUsuarioRequest
    {
        public string Senha { get; set; }
    }
}
