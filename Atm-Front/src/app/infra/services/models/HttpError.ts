import { HttpErrorResponse } from '@angular/common/http';

export class HttpError{

	eClientSide: boolean;
	httpErrorResponse: HttpErrorResponse;
	private erroTratado: boolean;
		
	public get naoAutorizado() : boolean {
		return this.httpErrorResponse && this.httpErrorResponse.status == 401;
	}

	public getErroTratado(){
		return this.erroTratado;
	}
	
	get validacao(){
		return ((this.httpErrorResponse.status >= 400) && (this.httpErrorResponse.status < 500));
	}

	public setErroTratado(tratado: boolean){
		this.erroTratado = tratado;
	}

	constructor(eClientSite: boolean, response: HttpErrorResponse) {
		this.httpErrorResponse = response;
		this.eClientSide = eClientSite;
		this.erroTratado = false;
	}
}