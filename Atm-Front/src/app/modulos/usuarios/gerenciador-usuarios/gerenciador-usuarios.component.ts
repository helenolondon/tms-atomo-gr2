/**
 * Autor: Heleno Sales Mesquita
 * Data : 11/02/2020
 * 
 * Página do gerenciador de usuários
 */
import { AfterViewInit, Component, ComponentFactory, ComponentFactoryResolver, OnInit, ViewChild } from '@angular/core';
import { UsuariosService } from '../services/usuarios.service';
import { UsuarioResponse } from '../responses/usuario-response';
import { DxDataGridComponent } from 'devextreme-angular';
import notify from 'devextreme/ui/notify';
import { ConfirmationService, Message } from 'primeng-lts/api';
import { TOUCH_BUFFER_MS } from '@angular/cdk/a11y';
import { map } from 'rxjs/operators';
import { Form } from '@angular/forms';
import { AtmGerenciadorComponent } from 'src/app/infra/atom/components/atm-gerenciador/atm-gerenciador.component';
import { PropriedadesUsuarioComponent } from '../propriedades-usuario/propriedades-usuario.component';
import { Observable } from 'rxjs';

@Component({
  selector: 'gerenciador-usuarios',
  templateUrl: './gerenciador-usuarios.component.html',
  styleUrls: ['./gerenciador-usuarios.component.scss'],
  providers: [ConfirmationService]
})
export class GerenciadorUsuariosComponent implements AfterViewInit {

  @ViewChild(AtmGerenciadorComponent, {static: true}) atmGerenciador: AtmGerenciadorComponent | undefined;

  public usuarios: UsuarioResponse[];

  public colunas = [
    { field: "nome", header: "Nome" },
    { field: "email", header: "E-mail" },
    { field: "login", header: "Login" },
    { field: "telefone", header: "Telefone" }
  ];

  pFormM: any = {
    nome: "",
    email: "",
    login: "",
    telefone: ""
  };

  ngAfterViewInit(): void {
    this.atmGerenciador.componentFactory = this.componentFactoryResolver.resolveComponentFactory(PropriedadesUsuarioComponent);
  }

  constructor(private usuariosservice: UsuariosService, private confirmationService: ConfirmationService,
    public componentFactoryResolver: ComponentFactoryResolver) { 
    this.pesquisar(null, null, null, null);
  };

  private pesquisar(nome: string, email: string, login: string, telefone: string){
    this.usuariosservice.RetUsarios(nome, email, login, telefone)
    .subscribe(response => {
      this.usuarios = response;
    });
  };
  
  getUsuario = (model) => {
    return this.usuariosservice.RetUsuarioPorCodigo(model.id);
  }

  excluirUsuario = (model) => {
    return this.usuariosservice.ExcluirUsuario(model.id);
  }

  onPesquisaSubmit(){
    this.pesquisar(this.pFormM.nome, this.pFormM.email, this.pFormM.login, this.pFormM.telefone);
  }
}
