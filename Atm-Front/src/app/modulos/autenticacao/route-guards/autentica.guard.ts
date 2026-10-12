import { HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, ActivatedRoute } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { DialogsService } from 'src/app/infra/services/dialogs.service';
import { AutenticaService } from '../services/autentica.service';

@Injectable({
  providedIn: 'root'
})
export class AutenticaGuard implements CanActivate {
  
  constructor(private router: Router, private activeRoute: ActivatedRoute, private autentica: AutenticaService,
    private dialogs: DialogsService){
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    
    if(!this.autentica.Autenticado)
    {
      this.autentica.LogOut();
      return of(false);
    }
    
    return this.autentica.verifyToken()
    .pipe(
      catchError((err: HttpResponse<any>, caught: Observable<any>) => {
        if(err.status < 200 || err.status > 299){
          this.dialogs.alerta("Sessão expirada")
          this.autentica.LogOut();
          return of(false);
        };

        return of(true);
      })      
    )
  }
}
