import { BairroResponse } from "./bairro-response";
import { CidadeResponse } from "./cidade-response";
import { EstadoResponse } from "./estado-response";

export class EnderecoResponse{
    cep: string;
    logradouro: string;
    tipo: string;
    bairro: BairroResponse;
    cidade: CidadeResponse;
    estado: EstadoResponse;
}