export class RegistrarUsuarioRequest {
    
    constructor(
        public nome : string,
        public cpfCnpj: string,
        public telefone : string,
        public email : string,
        public advogado: boolean,
        public numoab : string
    ) { }
}