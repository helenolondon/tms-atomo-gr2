import { UsuarioODT } from "../../usuarios/odts/usuario-odt";

export class ArquivoODT{
    public codArquivo: number;
    public nomeArquivo: string;
    public tipoArquivo: string;
    public tag: string;
    public referencia: string;
    public dataArquivo: Date;
    public tamanho: number;
    public dataCadastro: Date;
    public usuario: UsuarioODT;
}