/**
 * Autor: Heleno Sales Mesquita
 * Data : 23/01/2020
 * 
 * Responsta de autenticação
 */

import { Token } from '@angular/compiler/src/ml_parser/lexer';
import { UsuarioODT } from 'src/app/modulos/usuarios/odts/usuario-odt';

export class LoginResponse{
	usuario: UsuarioODT;
	login: string;
	token: string;
	eMail: string;
}