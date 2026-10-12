import { AlterarSenhaRequest } from "../../autenticacao/requests/alterar-senha";

export class SalvarUsuarioRequest{
    
    constructor(
        public id: number,
        public nome : string,
        public email : string,
        public login : string,
        public telefone : string,
        public senha: string
    ) { }
}