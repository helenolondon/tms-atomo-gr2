/**
 * Autor: Heleno Sales Mesquita
 * Data : 28/04/2022
 * 
 * Web services de endereços.
 */
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { catchError } from 'rxjs/operators';
import { HttpGlobalSettingsService } from 'src/app/infra/services/http-global-settings.service';
import { BairroResponse } from '../responses/bairro-response';
import { CidadeResponse } from '../responses/cidade-response';
import { EnderecoResponse } from '../responses/endereco-response';
import { EstadoResponse } from '../responses/estado-response';

@Injectable({
  providedIn: 'root'
})
export class EnderecosService {
  
  constructor(private httpClient: HttpClient, private httpSettings: HttpGlobalSettingsService) { }

    /**
   * Traz lista de estados
   */
     public RetEstados(){    
      return this.httpClient
      .get<EstadoResponse[]>(this.httpSettings.ServerUrl + "enderecos/estados" )
      .pipe(
        catchError( (e) => { return this.httpSettings.HandleHttpError( e ) } )
      );      
    }
    
    public RetCidades(codigoEstado){    
      return this.httpClient
      .get<CidadeResponse[]>(this.httpSettings.ServerUrl + "enderecos/estados/" + codigoEstado + "/cidades"  )
      .pipe(
        catchError( (e) => { return this.httpSettings.HandleHttpError( e ) } )
      );      
    }
    
    public RetCidadesPorNome(nomeCidade: string, uf: string = null){    
      let pars = new HttpParams();

      pars = pars.append("nomeCidade", nomeCidade);

      if(uf != null){
        pars = pars.append("uf", uf);
      };
      
      return this.httpClient
      .get<CidadeResponse[]>(this.httpSettings.ServerUrl + "enderecos/cidades", { params: pars}  )
      .pipe(
        catchError( (e) => { return this.httpSettings.HandleHttpError( e ) } )
      );      
    }
    
    public RetBairros(codigoCidade){    
      return this.httpClient
      .get<BairroResponse[]>(this.httpSettings.ServerUrl + "enderecos/cidades/" + codigoCidade + "/bairros"  )
      .pipe(
        catchError( (e) => { return this.httpSettings.HandleHttpError( e ) } )
      );      
    }
    
    public RetEndereco(cep): Observable<EnderecoResponse> {    
      return this.httpClient
      .get<EnderecoResponse>(this.httpSettings.ServerUrl + "enderecos/" + cep )
      .pipe(
        catchError( (e) => { return this.httpSettings.HandleHttpError( e ) } )
      );      
    }
}
