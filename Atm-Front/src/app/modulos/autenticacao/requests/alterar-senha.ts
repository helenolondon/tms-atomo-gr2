export class AlterarSenhaRequest{
    constructor(login: string, senha: string, senhaAntiga: string) {
        this.senha = senha;
        this.senhaAntiga = senhaAntiga;
        this.login = login;
    }
    
    private _login : string;
    public get login() : string {
        return this._login;
    }
    public set login(v : string) {
        this._login = v;
    }

    private _senha : string;
    public get senha() : string {
        return this._senha;
    }
    public set senha(v : string) {
        this._senha = v;
    }
    
    private _senhaAntiga : string;
    public get senhaAntiga() : string {
        return this._senhaAntiga;
    }
    public set senhaAntiga(v : string) {
        this._senhaAntiga = v;
    }
}