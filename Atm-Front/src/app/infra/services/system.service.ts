import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SystemService {
  
  StartSearchEnabled = this.isStartSearchEnabled();
  loginStoreKey = this.getLoginStoreKey();

  constructor() { }

  private getLoginStoreKey(){
    return "AtmLoginInfo";
  }

  private isStartSearchEnabled(){
    return true;
  }
}
