import { AfterViewInit, Component, ComponentFactory, ComponentFactoryResolver, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AtmGerenciadorComponent } from '../../../infra/atom/components/atm-gerenciador/atm-gerenciador.component';
import { PropriedadesClienteComponent } from '../propriedades-cliente/propriedades-cliente.component';
import { ListarClientesRequest } from '../requests/listar-clientes-request';
import { ClienteResponse } from '../responses/cliente-response';
import { ListarClientesResponse } from '../responses/listar-clientes-response';
import { ClientesService } from '../services/clientes.service';

@Component({
  selector: 'app-gerenciador-clientes',
  templateUrl: './gerenciador-clientes.component.html',
  styleUrls: ['./gerenciador-clientes.component.scss']
})
export class GerenciadorClientesComponent implements AfterViewInit {

  @ViewChild(AtmGerenciadorComponent, {static: true}) atmGerenciador: AtmGerenciadorComponent | undefined;
  
  clientes: ListarClientesResponse[];

  public colunas = [
    { field: "cliente.nome", header: "Nome" },
    { field: "cliente.razaoSocial", header: "Razão social" },
    { field: "cliente.cpfcnpj", header: "CPFCNPJ", type: "cpfCnpj" },
    { field: "cliente.inscricaoEstadual", header: "Insc./Estadual" },
    { field: "cliente.contato", header: "Contato" },
    { field: "cliente.email", header: "Email" },
    { field: "cliente.telefone", header: "Telefone" }
  ];

  pFormM: any = {
    nome: "",
    email: "",
    login: "",
    telefone: ""
  };

  ngAfterViewInit(): void {
    this.atmGerenciador.componentFactory = this.componentFactoryResolver.resolveComponentFactory(PropriedadesClienteComponent);
  }

  constructor(public componentFactoryResolver: ComponentFactoryResolver, private clienteService: ClientesService){
    this.pesquisar();
  }

  private pesquisar(contato: string = null, cpfCnpj: string = null, email: string = null, identidade: string = null, 
    inscricaoEstadual: string = null, nome: string = null, razaoSocial: string = null, telefone: string = null){
    
    this.clienteService.ListarClientes(contato, cpfCnpj, email, identidade, inscricaoEstadual, nome, razaoSocial, telefone)
    .subscribe((response: ListarClientesResponse[]) => {
      this.clientes = response;
    });
  };
  
  getModel = (model: any) => {
    return this.clienteService.ObterClientePorCodigo(model.cliente.codigo);
  }

  excluirModel = (model) => {
    return this.clienteService.ExcluirCliente(model.cliente.codigo);
  }

  onPesquisaSubmit(){
    this.pesquisar(this.pFormM.contato, this.pFormM.cpfCnpj, this.pFormM.email, this.pFormM.identidade, this.pFormM.inscricaoEstadual, 
      this.pFormM.nome, this.pFormM.razaoSocial, this.pFormM.telefone);
  }
}
