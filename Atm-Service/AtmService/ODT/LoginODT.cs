namespace AtmService.ODT
{
    /// <summary>
    /// Representa as informações de umn usuário logado.
    /// </summary>
    public class LoginODT
    {
        public UsuarioODT Usuario { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
