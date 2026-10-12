import { LookupItemResponse } from "../../compartilhado/LookupItemResponse";
import { ClienteResponse } from "./cliente-response";

export class ListarClientesResponse{
    cliente: ClienteResponse;
    cnae: LookupItemResponse;
}