import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AutenticaService } from "./services/autentica.service";

@Injectable()
export class AtmTokenInterceptor implements HttpInterceptor{
    constructor(private auth: AutenticaService) { };

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
        if(this.auth!.Token != undefined && this.auth!.Token != null){
            let request = req.clone({
                setHeaders: {
                    "token": this.auth.Token
                }
            });

            return next.handle(request);
        };
        
        return next.handle(req);
    }
}