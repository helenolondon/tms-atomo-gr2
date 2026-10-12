import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, ActivatedRouteSnapshot, Route, Router, RouterStateSnapshot } from '@angular/router';
import { AtmModalComponent } from 'src/app/infra/atom/components/atm-modal/atm-modal.component';
import { DialogsService } from 'src/app/infra/services/dialogs.service';
import { HttpError } from 'src/app/infra/services/models/HttpError';
import { deepCloneObject } from 'src/app/infra/utils/js-utils';
import { IAtmPropriedadesComponent } from '../../../infra/atom/components/atm-propriedades/IAtm-Propriedades-Component';
import { BairroResponse } from '../../enderecos/responses/bairro-response';
import { CidadeResponse } from '../../enderecos/responses/cidade-response';
import { EnderecoResponse } from '../../enderecos/responses/endereco-response';
import { EstadoResponse } from '../../enderecos/responses/estado-response';
import { EnderecosService } from '../../enderecos/services/enderecos.service';
import { FiscalValidadors } from '../../fiscal/fiscal-validators';
import { FiscalService } from '../../fiscal/services/fiscal.service';
import { SalvarEmpresaRequest } from '../requests/salvar-empresa-request';
import { EmpresaODT } from '../odts/empresa-odt';
import { EmpresasService } from '../services/empresas.service';

@Component({
  selector: 'propriedades-empresa',
  templateUrl: './propriedades-empresa.component.html',
  styleUrls: ['./propriedades-empresa.component.scss']
})
export class PropriedadesEmpresaComponent implements IAtmPropriedadesComponent {
  @ViewChild('mdPrecoTarefa', {static: false}) mdPrecoTarefa: AtmModalComponent;

  // Variáveis
  tiposDeIE = [];
  listaDeUfs = []
  listaDeCidades = [];  
  listaDeBairros = [];
  listaDeCnae = [];
  originalForm = null;
  empresaForm = this.criaEmpresaForm();
  serverErros = null;

  // Construtor
  constructor(private fb: FormBuilder, private enderecosService: EnderecosService, private fiscalService: FiscalService, 
    private dialogsService: DialogsService, private router: ActivatedRoute, private empresaService: EmpresasService) { 
    this.populaLookups();
    this.populaUf();
    this.populaCidades(null);
    this.populaBairros(null);
    this.populaCnaes();
  }

  // Getters e setters
  public get cnpj() : AbstractControl {
    return this.empresaForm.get('cnpj');
  }

  public get codigo() : AbstractControl {
    return this.empresaForm.get('codigo');
  }

  // Implementações de interfaçes do angular
  // Implementações de interfaces próprias
  
  //setModel de IAtmPropriedadesComponent
  async setModel(model: EmpresaODT){
    // Popula lookups
    await this.populaUf();    
    await this.populaCidades(model.codEstado);
    await this.populaBairros(model.codCidade);

    try{
      this.empresaForm.get('codigo').setValue(model.codEmpresa);
      this.empresaForm.get('nomeFantasia').setValue(model.nomeFantasia);
      this.empresaForm.get('cnpj').setValue(model.cnpj);
      this.empresaForm.get('inscricaoEstadual').setValue(model.inscricaoEstadual);
      this.empresaForm.get('razaoSocial').setValue(model.razaoSocial);
      this.empresaForm.get('inscricaoMunicipal').setValue(model.inscricaoMunicipal);
      this.empresaForm.get('inscricaoSuframa').setValue(model.inscricaoSuframa);
      this.empresaForm.get('microEmpresa').setValue((Boolean)(model.microEmpresa));
      this.empresaForm.get('cep').setValue(model.cep);
      this.empresaForm.get('logradouro').setValue(model.logradouro);
      this.empresaForm.get('complemento').setValue(model.complemento);
      this.empresaForm.get('numero').setValue(model.numero);
      this.empresaForm.get('uf').setValue(model.codEstado);
      this.empresaForm.get('cidade').setValue(model.codCidade);
      this.empresaForm.get('responsavel').setValue(model.responsavel);
      this.empresaForm.get('bairro').setValue(model.codBairro);
      this.empresaForm.get('telefone').setValue(model.telefone);
      this.empresaForm.get('contato').setValue(model.contato);
      this.empresaForm.get('email').setValue(model.email);
      this.empresaForm.get('observacoes').setValue(model.observacao);

      if(model.cnae != undefined && model.cnae != null){
        this.empresaForm.get("cnaePrincipal").setValue(model.cnae);
      };

      this.originalForm = deepCloneObject(this.empresaForm);
    } catch (e) {
      console.log(e);
      throw "Erro ao carregar dados do usuário";      
    }
  };

  // Procedimentos

  criaEmpresaForm() { 
    let form = new FormGroup(
    {
      codigo: new FormControl({value: 0, disabled: true}, [Validators.required]),
      email: new FormControl('', [Validators.required]),
      dataCadastro: new FormControl({value: new Date(), disabled: true}),
      inscricaoEstadual: new FormControl('', [Validators.required]),
      cep: new FormControl('', [Validators.required]),
      logradouro: new FormControl('', [Validators.required]),
      complemento: new FormControl(''),
      numero: new FormControl('', [Validators.required]),
      uf: new FormControl('', [Validators.required]),
      cidade: new FormControl('', [Validators.required]),
      bairro: new FormControl('', [Validators.required]),
      telefone: new FormControl('', []),
      contato: new FormControl('', []),
      observacoes: new FormControl(''),
      nomeFantasia: new FormControl('', [Validators.required]),
      responsavel: new FormControl('', [Validators.required]),
      razaoSocial: new FormControl('', [Validators.required]),
      inscricaoMunicipal: new FormControl(''),
      inscricaoSuframa: new FormControl(''),
      cnaePrincipal: new FormControl('', [Validators.required]),
      microEmpresa: new FormControl(false),
      dataAberturaEmpresa: new FormControl(),
      cnpj: new FormControl('', [FiscalValidadors.cnpjValidator, Validators.required])            
    });

    this.originalForm = deepCloneObject(form);

    return form;
  }

  populaLookups(){
    this.tiposDeIE = this.router.snapshot.data['listaSitucoesDaInscricaoEstadual'];
  }

  populaUf(){
    return this.enderecosService.RetEstados().toPromise().then((e: EstadoResponse[]) =>{
      this.listaDeUfs = [
        { descricao: '--', codigo: null}
      ];

      e.forEach(estado => {
        this.listaDeUfs.push({descricao: estado.uf, codigo: estado.codigoEstado})
      });
    })    
  }

  populaCnaes(){
    this.empresaForm.get("cnaePrincipal").setValue(null);
    
    this.listaDeCnae = [
      { descricao: '--', codigo: null}
    ];
  }

  populaCidades(codigoEstado): Promise<any> {

    this.empresaForm.get("cidade").setValue(null);
    
    this.listaDeCidades = [
      { descricao: '--', codigo: null}
    ];

    if(codigoEstado == null){
      return;
    }

    return this.enderecosService.RetCidades(codigoEstado).toPromise<CidadeResponse[]>().then((e: CidadeResponse[]) =>{
      e.forEach(cidade => {
        this.listaDeCidades.push({descricao: cidade.nomeCidade, codigo: cidade.codigoCidade})
      });
    });
  }

  populaBairros(codigoCidade){

    this.empresaForm.get("bairro").setValue(null);

    this.listaDeBairros = [
      { descricao: '--', codigo: null}
    ];

    if(codigoCidade == null){
      return;
    }

    return this.enderecosService.RetBairros(codigoCidade).toPromise<BairroResponse[]>().then((e: BairroResponse[]) =>{
      e.forEach(bairro => {
        this.listaDeBairros.push({descricao: bairro.nomeBairro, codigo: bairro.codigoBairro})
      });
    })    
  }

  buscaCep(cep){

    this.enderecosService.RetEndereco(cep).subscribe(async (endereco: EnderecoResponse) => {
      
      if(endereco == null){
        this.dialogsService.erro("CEP não encontrado");
      }

      this.empresaForm.get("logradouro").setValue(endereco.logradouro);
      this.empresaForm.get("uf").setValue(endereco.estado.codigoEstado);
      
      if(endereco.logradouro != this.empresaForm.get("cidade").value){
        await this.populaCidades(endereco.estado.codigoEstado);
        this.empresaForm.get("cidade").setValue(endereco.cidade.codigoCidade);
      }
      
      if(this.empresaForm.get("bairro").value != endereco.bairro.codigoBairro){
        await this.populaBairros(endereco.cidade.codigoCidade);
        this.empresaForm.get("bairro").setValue(endereco.bairro.codigoBairro);
      }
    });
  }

  viewToModel(): SalvarEmpresaRequest {
    let cnae = this.empresaForm.get('cnaePrincipal').value;
    debugger;
    return {
      
      "codEmpresa": this.empresaForm.get('codigo').value,
      "nomeFantasia": this.empresaForm.get('nomeFantasia').value,
      "cnpj": this.empresaForm.get('cnpj').value,
      "inscricaoEstadual": this.empresaForm.get('inscricaoEstadual').value,
      "cnaePrincipal": cnae != null? cnae.codigoCnae: null,
      "razaoSocial": this.empresaForm.get('razaoSocial').value,
      "inscricaoMunicipal": this.empresaForm.get('inscricaoMunicipal').value,
      "inscricaoSuframa": this.empresaForm.get('inscricaoSuframa').value,
      "microEmpresa": this.empresaForm.get('microEmpresa').value,
      "cep": this.empresaForm.get('cep').value,
      "logradouro": this.empresaForm.get('logradouro').value,
      "complemento": this.empresaForm.get('complemento').value,
      "numero": this.empresaForm.get('numero').value,
      "codEstado": this.empresaForm.get('uf').value,
      "codCidade": this.empresaForm.get('cidade').value,
      "codBairro": this.empresaForm.get('bairro').value,
      "telefone": this.empresaForm.get('telefone').value,
      "contato": this.empresaForm.get('contato').value,
      "responsavel": this.empresaForm.get('responsavel').value,
      "eMail": this.empresaForm.get('email').value,
      "observacao": this.empresaForm.get('observacoes').value
    }    
  }

  postEmpresa(model: SalvarEmpresaRequest){
    console.log(model);

    this.empresaService.SalvarEmpresa(model).subscribe((response: EmpresaODT) =>{
      this.serverErros = null;
      this.dialogsService.sucesso("Empresa salva com sucesso");

      this.setModel(response);
    },
    (error: HttpError) => {
      if(error.httpErrorResponse.status == 400){
        this.serverErros = error.httpErrorResponse.error; 
      } else {
        if(!error.getErroTratado()){
          console.log(error);
          this.dialogsService.erro("Erro ao tentar salvar empresa");
        }
      }
    })
  }

  // Eventos

  onSubmit(){
    if(!this.empresaForm.valid){
      this.empresaForm.markAsTouched();
      return;
    }
    
    this.postEmpresa(this.viewToModel());
  }

  onResetForm(){
    this.empresaForm = deepCloneObject(this.originalForm);
  }

  onBuscaCanes(e){   
   return this.fiscalService.RetListaCnaes(e)
    .subscribe((cnaes) =>{
      this.listaDeCnae = cnaes == null? []: cnaes;
    })
  }

  onUfchange(codigoEstado){
    this.populaCidades(codigoEstado);
    this.populaBairros(null);
  }

  onCidadeChange(codigoCidade){
    this.populaBairros(codigoCidade);
  }

  onBuscaCepClick(cep){
    this.buscaCep(cep);    
  }

  onBuscaCepBlur(cep){
    if(cep != null && cep != "" && this.empresaForm.get("logradouro").value == ""){
      this.buscaCep(cep);    
    }
  }
}
