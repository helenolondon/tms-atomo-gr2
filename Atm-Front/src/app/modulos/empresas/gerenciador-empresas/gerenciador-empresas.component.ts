import { Component, ComponentFactoryResolver, OnInit, ViewChild } from '@angular/core';
import { AtmGerenciadorComponent } from 'src/app/infra/atom/components/atm-gerenciador/atm-gerenciador.component';
import { EmpresaODT } from '../odts/empresa-odt';
import { EmpresasService } from '../services/empresas.service';
import { PropriedadesEmpresaComponent } from '../propriedades-empresa/propriedades-empresa.component';

@Component({
  selector: 'gerenciador-empresas',
  templateUrl: './gerenciador-empresas.component.html',
  styleUrls: ['./gerenciador-empresas.component.scss']
})
export class GerenciadorEmpresasComponent {
  @ViewChild(AtmGerenciadorComponent, {static: true}) atmGerenciador: AtmGerenciadorComponent | undefined;
  
  listaDeEmpresas: EmpresaODT[];

  public colunas = [
    { field: "razaoSocial", header: "Razão social" },
    { field: "nomeFantasia", header: "Nome fantasia" },
    { field: "cnpj", header: "CNPJ", type: "cpfCnpj" },
    { field: "contato", header: "Contato" },
    { field: "responsavel", header: "Responsável" },
    { field: "email", header: "Email" },
    { field: "telefone", header: "Telefone" }
  ];

  pFormM: any = {
    razaoSocial: "",
    nomeFantasia: "",
    cnpj: "",
    contato: "",
    responsavel: "",
    email: "",
    telefone: ""
  };

  ngAfterViewInit(): void {
    this.atmGerenciador.componentFactory = this.componentFactoryResolver.resolveComponentFactory(PropriedadesEmpresaComponent);
  }

  constructor(public componentFactoryResolver: ComponentFactoryResolver, private empresasService: EmpresasService){
    this.pesquisar();
  }

  private pesquisar(razaoSocial: string = null, nomeFantasia: string = null, cnpj: string = null, responsavel: string = null, 
    inscricaoEstadual: string = null, telefone: string = null, email: string = null){
    
    this.empresasService.ListarEmpresas(razaoSocial, nomeFantasia, cnpj, responsavel, inscricaoEstadual, telefone, email)
    .subscribe((response: EmpresaODT[]) => {
      this.listaDeEmpresas = response;
    });
  };
  
  getModel = (model: EmpresaODT) => {
    return this.empresasService.ObterEmpresa(model.codEmpresa);
  }

  excluirModel = (model: EmpresaODT) => {
    return this.empresasService.ExcluirEmpresa(model.codEmpresa);
  }

  onPesquisaSubmit(){
    this.pesquisar(this.pFormM.razaoSocial, this.pFormM.nomeFantasia, this.pFormM.cnpj, this.pFormM.responsavel, this.pFormM.inscricaoEstadual, 
      this.pFormM.telefone, this.pFormM.email);
  }
}
