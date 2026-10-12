/**
 * Autor: Heleno Sales Mesquita
 * Data : 18/01/2020
 * 
 * Representa o modelo de dados do formulário de login.
 */

import { UsuarioODT } from "src/app/modulos/usuarios/odts/usuario-odt";

export class LoginModel{
	login: string;
	senha: string;
	token: string;
	eMail: string;

	usuario: UsuarioODT;
}