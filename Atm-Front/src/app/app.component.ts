import { Component } from '@angular/core';
import { AutenticaService } from './modulos/autenticacao/services/autentica.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less'],
  providers: []
})
export class AppComponent {
  title = 'atm-front';

  constructor(private autenticador: AutenticaService) {
  }

  onSairClick(){
    this.autenticador.LogOut();
  }
}
