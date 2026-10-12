import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { observable, Observable, throwError } from "rxjs";
import { LookupItemResponse } from "../../compartilhado/LookupItemResponse";
import { ClientesService } from "../services/clientes.service";
import { catchError } from "rxjs/operators";
import { DialogsService } from "src/app/infra/services/dialogs.service";
import { HttpError } from "src/app/infra/services/models/HttpError";

@Injectable({providedIn: 'root'})
export class ClientesTiposDePessoaResolver implements Resolve<LookupItemResponse[]>{
    constructor(private clientesService: ClientesService, private dialogs: DialogsService) {
    }
    
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<LookupItemResponse[]> {
        return this.clientesService
        .ListarTiposDePessoa()
        .pipe(catchError((error: HttpError): Observable<LookupItemResponse[]>  => {
            if(!error.getErroTratado()){
                this.dialogs.erro("Erro ao carregar os tipos de pessoa");
                return throwError("Erro ao carregar os tipos de pessoa na api de clientes")
            }
        }))        
    }    
}