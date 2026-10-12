import { CnaeResponse } from "../../fiscal/services/responses/CnaeResponse";

export class EmpresaODT{
    codEmpresa: number;
    nomeFantasia: string;
    razaoSocial: string;
    cnpj: string;
    situacaoIE: number;
    inscricaoEstadual: string;
    cnaePrincipal: number;
    inscricaoMunicipal: string;
    inscricaoSuframa: string;
    microEmpresa: boolean;
    cep: string;
    logradouro: string;
    complemento: string;
    numero: string;
    codEstado: number;
    codCidade: number;
    codBairro: number;
    telefone: string;
    responsavel: string;
    contato: string;
    email: string;
    observacao: string;

    cnae: CnaeResponse;
}