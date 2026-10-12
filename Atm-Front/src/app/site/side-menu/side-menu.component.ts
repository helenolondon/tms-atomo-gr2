import { Component, OnInit } from '@angular/core';
import { AutenticaService } from 'src/app/modulos/autenticacao/services/autentica.service';
import { AtmMenuItem } from '../atm-menu/AtmMenuItem';

@Component({
  selector: 'side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.scss']
})
export class SideMenuComponent implements OnInit {

  private itensDeMenu: AtmMenuItem = null;

  constructor(private autenticaService: AutenticaService) { 
  }

  self = this;

  public menuItens(){
    
    let self = this;

    if(this.itensDeMenu == null)
    {
      this.itensDeMenu = new AtmMenuItem("#")
      .AddItem(new AtmMenuItem("CADASTROS")
        .AddItem(new AtmMenuItem("Empresas", null, "empresas"))
        .AddItem(new AtmMenuItem("Clientes", null, "clientes"))
      )
      .AddItem(new AtmMenuItem("CONFIGURAÇÕES")
        .AddItem(new AtmMenuItem("Usuários")
          .AddItem(new AtmMenuItem("Gerenciar usuários", null, "/usuarios"))
          .AddItem(new AtmMenuItem("Alterar senha", null, '/alterar-senha'))
        )
        .AddItem(new AtmMenuItem("Configurações do sistema"))
      ).AddItem(new AtmMenuItem("SAIR", onSairClick));
    }
    
    function onSairClick(){
      self.autenticaService.LogOut();
    }

    return this.itensDeMenu;
  }
    
  ngOnInit() {
  }
}
