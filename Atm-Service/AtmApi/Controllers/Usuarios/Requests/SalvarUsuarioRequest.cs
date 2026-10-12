using AtmService.ODT;

namespace AtmApi.Controllers.Usuarios.Requests
{
    /// <summary>
    /// Representa os dados de um usuário a serem salvos
    /// </summary>
    public class SalvarUsuarioRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Telefone { get; set; }
        public bool Supervisor { get; set; }

        public UsuarioODT ODT()
        {
            var temp = new UsuarioODT();

            temp.Email = this.Email;
            temp.Id = this.Id;
            temp.Login = this.Login;
            temp.Nome = this.Nome;
            temp.Telefone = this.Telefone;
            temp.Supervisor = this.Supervisor;
            
            return temp;
        }
    }
}
