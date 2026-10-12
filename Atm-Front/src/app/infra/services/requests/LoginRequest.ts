/**
 * Autor: Heleno Sales Mesquita
 * Data : 23/01/2020
 * 
 * Requisição de autenticação
 */

import { Token } from '@angular/compiler/src/ml_parser/lexer';

export class LoginRequest{
	login: string;
	senha: string;

	constructor(login: string, senha: string){
		this.login = login;
		this.senha = senha;
	}
}