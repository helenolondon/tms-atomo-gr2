import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import {  Observable, throwError } from "rxjs";
import { LookupItemResponse } from "../../compartilhado/LookupItemResponse";
import { ClientesService } from "../services/clientes.service";
import { catchError } from "rxjs/operators";
import { DialogsService } from "src/app/infra/services/dialogs.service";
import { HttpError } from "src/app/infra/services/models/HttpError";

@Injectable({providedIn: 'root'})
export class EstadosCivisResolver implements Resolve<LookupItemResponse[]>{
    constructor(private clientesService: ClientesService, private dialogs: DialogsService) { }
    
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<LookupItemResponse[]> {
        return this.clientesService
        .ListarEstadosCivis()
        .pipe(catchError((error: HttpError): Observable<LookupItemResponse[]>  => {
            if(!error.getErroTratado()){
                this.dialogs.erro("Erro ao carregar a lista de estados civis");
                return throwError("Erro ao carregar a lista de estados civis na api de clientes")
            }
        }))        
    }    
}