import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { observable, Observable, throwError } from "rxjs";
import { LookupItemResponse } from "../../compartilhado/LookupItemResponse";
import { catchError } from "rxjs/operators";
import { DialogsService } from "src/app/infra/services/dialogs.service";
import { FiscalService } from "../services/fiscal.service";

@Injectable({providedIn: 'root'})
export class SituacoesInscricaoEstadualResolver implements Resolve<LookupItemResponse[]>{
    constructor(private fiscalService: FiscalService, private dialogs: DialogsService) {
    }
    
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<LookupItemResponse[]> {
        return this.fiscalService
        .ListarSituacoesInscricaoEstadual()
        .pipe(catchError((err: any): Observable<LookupItemResponse[]>  => {
            this.dialogs.erro("Erro ao carregar a lista situações da inscrição estadual");
            return throwError("Erro ao carregar a lista situações da inscrição estadual na api do fiscal")
        }))        
    }    
}