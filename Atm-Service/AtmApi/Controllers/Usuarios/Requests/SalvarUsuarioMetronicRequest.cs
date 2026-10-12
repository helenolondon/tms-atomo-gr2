using AtmService.ODT;

namespace AtmApi.Controllers.Usuarios.Requests
{
    public class SalvarUsuarioMetronicRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Surname { get; set; }

        public UsuarioODT ODT()
        {
            var temp = new UsuarioODT();

            temp.Email = this.Email;
            temp.Id = this.Id;
            temp.Login = this.Login;
            temp.Nome = this.Name;
            temp.Telefone = this.PhoneNumber;
            
            return temp;
        }
    }
}
