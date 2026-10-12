import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-configuracoes-acesso',
  templateUrl: './configuracoes-acesso.component.html',
  styleUrls: ['./configuracoes-acesso.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ConfiguracoesAcessoComponent implements OnInit {

  permissoes =  
    [
        {
            "label": "CONFIGURAÇÕES",
            "expandedIcon": "fa fa-folder-open",
            "collapsedIcon": "fa fa-folder",
            "children": [{
                    "label": "Usuários",
                    "data": "Work Folder",
                    "expandedIcon": "fa fa-folder-open",
                    "collapsedIcon": "fa fa-folder",
                    "children": [
                      {
                        "label": "Gerenciar usuários", "icon": "fa fa-file-word-o"
                      }, {
                        "label": "Alterar senha", "icon": "fa fa-file-word-o"
                      },
                      { 
                        "label": "Configurações de acesso", "icon": "fa fa-file-word-o"
                      }
                  ]
                }
              ]
        }
    ]



  constructor() { }

  ngOnInit() {
  }

}
