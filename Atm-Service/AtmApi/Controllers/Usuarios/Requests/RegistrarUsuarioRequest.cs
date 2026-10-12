using AtmService.ODT;

namespace AtmApi.Controllers.Usuarios.Requests
{
    /// <summary>
    /// Representa os dados de um usuário a serem salvos
    /// </summary>
    public class RegistrarUsuarioRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CpfCnpj { get; set; }
        public string Telefone { get; set; }
        public bool Advogado { get; set; }
        public string NumOab { get; set; }

        public UsuarioODT ODT()
        {
            var temp = new UsuarioODT();

            temp.Email = this.Email;
            temp.Id = 0;
            temp.Login = this.Email;
            temp.Nome = this.Nome;
            temp.Telefone = this.Telefone;
            
            return temp;
        }
    }
}
