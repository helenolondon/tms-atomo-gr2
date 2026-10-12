import { LoginModel } from 'src/app/site/models/Login.model';
import { LoginResponse } from 'src/app/infra/services/responses/LoginResponse';

export abstract class LoginModelMapper{
	public static MapFromLoginResponse(response: LoginResponse): LoginModel{
		let temp = new LoginModel();

		temp.eMail = response.eMail;
		temp.login = response.login;
		temp.senha = null;
		temp.token = response.token;
		
		return temp;
	}
}