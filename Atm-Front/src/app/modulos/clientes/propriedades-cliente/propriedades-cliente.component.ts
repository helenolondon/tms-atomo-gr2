import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, ActivatedRouteSnapshot, Route, Router, RouterStateSnapshot } from '@angular/router';
import validator from 'devextreme/ui/validator';
import { MessageService } from 'primeng-lts/api';
import { Observable } from 'rxjs';
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
import { CnaeResponse } from '../../fiscal/services/responses/CnaeResponse';
import { SalvarClienteRequest } from '../requests/salvar-cliente-request';
import { ClienteResponse } from '../responses/cliente-response';
import { ClientesService } from '../services/clientes.service';

@Component({
  selector: 'app-propriedades-cliente',
  templateUrl: './propriedades-cliente.component.html',
  styleUrls: ['./propriedades-cliente.component.scss']
})
export class PropriedadesClienteComponent implements OnInit, IAtmPropriedadesComponent {
  @ViewChild('mdPrecoTarefa', {static: false}) mdPrecoTarefa: AtmModalComponent;

  // Variáveis
  tiposDeCliente = [];  
  tiposDeIE = [];
  estadosCivil = [];
  listaDeUfs = []
  listaDeCidades = [];  
  listaDeBairros = [];
  listaDeCnae = [];
  originalForm = null;
  clienteForm = this.criaClienteForm();
  serverErros = null;

  // Construtor
  constructor(private fb: FormBuilder, private enderecosService: EnderecosService, private fiscalService: FiscalService, 
    private dialogsService: DialogsService, private router: ActivatedRoute, private clientesService: ClientesService) { 
    this.populaLookups();
    this.populaUf();
    this.populaCidades(null);
    this.populaBairros(null);
    this.populaCnaes();
  }

  // Getters e setters
  public get tipo() : string {
    return this.clienteForm.get('tipo').value; 
  }

  public get cpf() : AbstractControl {
    return this.clienteForm.get('cpf');
  }

  public get cnpj() : AbstractControl {
    return this.clienteForm.get('cnpj');
  }

  public get codigo() : AbstractControl {
    return this.clienteForm.get('codigo');
  }

  // Implementações de interfaçes do angular
  ngOnInit(): void {
    this.setValidators(this.clienteForm.get("tipo").value);
    
    this.clienteForm.get("tipo").valueChanges.subscribe((val) => {
      this.setValidators(val);
    });

  }

  // Implementações de interfaces próprias
  
  //setModel de IAtmPropriedadesComponent
  async setModel(model: any){
    // Popula lookups
    await this.populaUf();    
    await this.populaCidades(model.cliente.codigoEstado);
    await this.populaBairros(model.cliente.codigoCidade);

    try{
      debugger;
      this.clienteForm.get('codigo').setValue(model.cliente.codigo);
      this.clienteForm.get('nome').setValue(model.cliente.nome);
      this.clienteForm.get('tipo').setValue(model.cliente.tipo);
      this.clienteForm.get('cpf').setValue(model.cliente.cpfcnpj);
      this.clienteForm.get('cnpj').setValue(model.cliente.cpfcnpj);
      this.clienteForm.get('identidade').setValue(model.cliente.identidade);
      this.clienteForm.get('orgaoexp').setValue(model.cliente.orgaoExpedidor);
      this.clienteForm.get('dataEmissIdent').setValue(new Date(model.cliente.ieDataEmissao));
      this.clienteForm.get('dataNascimento').setValue(new Date(model.cliente.dataNascimento));
      this.clienteForm.get('estadoCivil').setValue(model.cliente.estadoCivil);
      this.clienteForm.get('profissao').setValue(model.cliente.profissao);
      this.clienteForm.get('nacionalidade').setValue(model.cliente.nacionalidade);
      this.clienteForm.get('inscricaoEstadual').setValue(model.cliente.inscricaoEstadual);
      this.clienteForm.get('tipoInscEstadual').setValue(model.cliente.ieSituacao);
      this.clienteForm.get('razaoSocial').setValue(model.cliente.razaoSocial);
      this.clienteForm.get('inscricaoMunicipal').setValue(model.cliente.inscricaoMunicipal);
      this.clienteForm.get('inscricaoSuframa').setValue(model.cliente.inscricaoSuframa);
      this.clienteForm.get('microEmpresa').setValue((Boolean)(model.cliente.microEmpresa));
      this.clienteForm.get('cep').setValue(model.cliente.cep);
      this.clienteForm.get('logradouro').setValue(model.cliente.logradouro);
      this.clienteForm.get('complemento').setValue(model.cliente.complemento);
      this.clienteForm.get('numero').setValue(model.cliente.numero);
      this.clienteForm.get('uf').setValue(model.cliente.codigoEstado);
      this.clienteForm.get('cidade').setValue(model.cliente.codigoCidade);
      this.clienteForm.get('bairro').setValue(model.cliente.codigoBairro);
      this.clienteForm.get('telefone').setValue(model.cliente.telefone);
      this.clienteForm.get('contato').setValue(model.cliente.contato);
      this.clienteForm.get('email').setValue(model.cliente.email);
      this.clienteForm.get('observacoes').setValue(model.cliente.observacoes);

      if(model.cnae != undefined && model.cnae != null){
        this.clienteForm.get("cnaePrincipal").setValue(model.cnae);
      };

      if(this.clienteForm.get("codigo").value > 0){
        this.clienteForm.get("tipo").disable();
      } else {
        this.clienteForm.get("tipo").enable();
      }

      this.originalForm = deepCloneObject(this.clienteForm);
    } catch (e) {
      console.log(e);
      throw "Erro ao carregar dados do usuário";      
    }
  };

  // Procedimentos

  criaClienteForm() { 
    let form = new FormGroup(
    {
      codigo: new FormControl({value: 0, disabled: true}, [Validators.required]),
      nome: new FormControl('', []),
      tipo: new FormControl(2, [Validators.required]),
      email: new FormControl('', [Validators.required]),
      cpf: new FormControl('', [Validators.required, FiscalValidadors.cpfValidator]),
      identidade: new FormControl('', [Validators.required]),
      orgaoexp: new FormControl('', [Validators.required]),
      telefone: new FormControl('', [Validators.required]),
      dataCadastro: new FormControl({value: new Date(), disabled: true}),
      dataNascimento: new FormControl({value: new Date(), disabled: false}),
      dataEmissIdent: new FormControl({value: new Date(), disabled: false}),
      estadoCivil: new FormControl(1, [Validators.required]),
      profissao: new FormControl('', [Validators.required]),
      nacionalidade: new FormControl('', [Validators.required]),
      tipoInscEstadual: new FormControl(0, [Validators.required]),
      inscricaoEstadual: new FormControl('', [Validators.required]),
      cep: new FormControl('', [Validators.required]),
      logradouro: new FormControl('', [Validators.required]),
      complemento: new FormControl(''),
      numero: new FormControl('', [Validators.required]),
      uf: new FormControl('', [Validators.required]),
      cidade: new FormControl('', [Validators.required]),
      bairro: new FormControl('', [Validators.required]),
      contato: new FormControl('', []),
      observacoes: new FormControl(''),
      razaoSocial: new FormControl(''),
      inscricaoMunicipal: new FormControl(''),
      inscricaoSuframa: new FormControl(''),
      cnaePrincipal: new FormControl('', [Validators.required]),
      microEmpresa: new FormControl(false),
      dataAberturaEmpresa: new FormControl(),
      cnpj: new FormControl('', [FiscalValidadors.cnpjValidator])            
    });

    this.originalForm = deepCloneObject(form);

    return form;
  }

  setValidators(val){
    
    let frm = this.clienteForm;

    let ccpf = frm.get("cpf");
    let corgaoExp = frm.get("orgaoexp");
    let estadoCivil = frm.get("estadoCivil");
    let dataEmissIdent = frm.get("dataEmissIdent");
    let dataNascimento = frm.get("dataNascimento");
    let profissao = frm.get("profissao");
    let nacionalidade = frm.get("nacionalidade");
    let tipoInscEstadual = frm.get("tipoInscEstadual");
    let identidade = frm.get("identidade");

    let cnaePrincipal = frm.get("cnaePrincipal");
    let razaoSocial = frm.get("razaoSocial");
    let cnpj = frm.get("cnpj");
    let inscricaoMunicipal = frm.get("inscricaoMunicipal");
    let microEmpresa = frm.get("microEmpresa");

    corgaoExp.clearValidators();
    estadoCivil.clearValidators();
    dataEmissIdent.clearValidators();
    dataNascimento.clearValidators();
    profissao.clearValidators();
    nacionalidade.clearValidators();
    tipoInscEstadual.clearValidators();
    cnaePrincipal.clearValidators();
    razaoSocial.clearValidators();
    cnpj.clearValidators();
    inscricaoMunicipal.clearValidators();
    microEmpresa.clearValidators();
    ccpf.clearValidators();
    identidade.clearValidators();

    if(val == 1){
      identidade.setValidators([Validators.required]);
      ccpf.setValidators([Validators.required, FiscalValidadors.cpfValidator]);
      corgaoExp.setValidators([Validators.required]);
      estadoCivil.setValidators([Validators.required]);
      dataEmissIdent.setValidators([Validators.required]);
      dataNascimento.setValidators([Validators.required]);
      profissao.setValidators([Validators.required]);
      nacionalidade.setValidators([Validators.required]);
      tipoInscEstadual.setValidators([Validators.required]); 
    } else{
      cnaePrincipal.setValidators([Validators.required]);        
      razaoSocial.setValidators([Validators.required]);        
      cnpj.setValidators([Validators.required, FiscalValidadors.cnpjValidator]);        
      inscricaoMunicipal.setValidators([Validators.required]);        
      microEmpresa.setValidators([Validators.required]);        
    }

    identidade.updateValueAndValidity();
    ccpf.updateValueAndValidity();
    corgaoExp.updateValueAndValidity();
    estadoCivil.updateValueAndValidity();
    dataEmissIdent.updateValueAndValidity();
    dataNascimento.updateValueAndValidity();
    profissao.updateValueAndValidity();
    nacionalidade.updateValueAndValidity();
    tipoInscEstadual.updateValueAndValidity();  
    cnaePrincipal.updateValueAndValidity();
    razaoSocial.updateValueAndValidity();
    cnpj.updateValueAndValidity();        
    inscricaoMunicipal.updateValueAndValidity();        
    microEmpresa.updateValueAndValidity();
    
    frm.updateValueAndValidity();
  }

  populaLookups(){
    this.tiposDeCliente = this.router.snapshot.data['listaTiposDeClientes'];
    this.tiposDeIE = this.router.snapshot.data['listaSitucoesDaInscricaoEstadual'];
    this.estadosCivil = this.router.snapshot.data['listaEstadosCivis'];
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
    this.clienteForm.get("cnaePrincipal").setValue(null);
    
    this.listaDeCnae = [
      { descricao: '--', codigo: null}
    ];
  }

  populaCidades(codigoEstado): Promise<any> {

    this.clienteForm.get("cidade").setValue(null);
    
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

    this.clienteForm.get("bairro").setValue(null);

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

      this.clienteForm.get("logradouro").setValue(endereco.logradouro);
      this.clienteForm.get("uf").setValue(endereco.estado.codigoEstado);
      
      if(endereco.logradouro != this.clienteForm.get("cidade").value){
        await this.populaCidades(endereco.estado.codigoEstado);
        this.clienteForm.get("cidade").setValue(endereco.cidade.codigoCidade);
      }
      
      if(this.clienteForm.get("bairro").value != endereco.bairro.codigoBairro){
        await this.populaBairros(endereco.cidade.codigoCidade);
        this.clienteForm.get("bairro").setValue(endereco.bairro.codigoBairro);
      }
    });
  }

  viewToModel(): SalvarClienteRequest {
    let cnae = this.clienteForm.get('cnaePrincipal').value;

    return {
      "codigo": this.clienteForm.get('codigo').value,
      "nome": this.clienteForm.get('nome').value,
      "tipo": this.clienteForm.get('tipo').value,
      "cpfcnpj": this.clienteForm.get('tipo').value == 1? this.clienteForm.get('cpf').value: this.clienteForm.get('cnpj').value,
      "identidade": this.clienteForm.get('identidade').value,
      "orgaoExpedidor": this.clienteForm.get('orgaoexp').value,
      "ieDataEmissao": this.clienteForm.get('dataEmissIdent').value,
      "dataNascimento": this.clienteForm.get('dataNascimento').value,
      "estadoCivil": this.clienteForm.get('estadoCivil').value,
      "profissao": this.clienteForm.get('profissao').value,
      "nacionalidade": this.clienteForm.get('nacionalidade').value,
      "inscricaoEstadual": this.clienteForm.get('inscricaoEstadual').value,
      "ieSituacao": this.clienteForm.get('tipoInscEstadual').value,
      "cnaePrincipal": cnae != null? cnae.codigoCnae: null,
      "razaoSocial": this.clienteForm.get('razaoSocial').value,
      "inscricaoMunicipal": this.clienteForm.get('inscricaoMunicipal').value,
      "inscricaoSuframa": this.clienteForm.get('inscricaoSuframa').value,
      "microEmpresa": this.clienteForm.get('microEmpresa').value,
      "cep": this.clienteForm.get('cep').value,
      "logradouro": this.clienteForm.get('logradouro').value,
      "complemento": this.clienteForm.get('complemento').value,
      "numero": this.clienteForm.get('numero').value,
      "codigoEstado": this.clienteForm.get('uf').value,
      "codigoCidade": this.clienteForm.get('cidade').value,
      "codigoBairro": this.clienteForm.get('bairro').value,
      "telefone": this.clienteForm.get('telefone').value,
      "contato": this.clienteForm.get('contato').value,
      "email": this.clienteForm.get('email').value,
      "observacoes": this.clienteForm.get('observacoes').value
    }    
  }

  postCliente(model: SalvarClienteRequest){
    console.log(model);

    this.clientesService.SalvarCliente(model).subscribe((response: ClienteResponse) =>{
      this.serverErros = null;
      this.dialogsService.sucesso("Cliente salvo com sucesso");

      this.setModel(response);
    },
    (error: HttpError) => {
      if(error.httpErrorResponse.status == 400){
        this.serverErros = error.httpErrorResponse.error; 
      } else {
        if(!error.getErroTratado()){
          console.log(error);
          this.dialogsService.erro("Erro ao tentar salvar cliente");
        }
      }
    })
  }

  // Eventos

  onSubmit(){
    if(!this.clienteForm.valid){
      for(var i in this.clienteForm.controls){
        if(!this.clienteForm.controls[i].valid){
          Object.keys(this.clienteForm.controls).forEach(name =>{
            if (this.clienteForm.controls[i] == this.clienteForm.controls[name]) {
              console.log(name);
            }
          })
        }
      }
      
      this.clienteForm.markAsTouched();
      return;
    }
    
    this.postCliente(this.viewToModel());
  }

  onResetForm(){
    this.clienteForm = deepCloneObject(this.originalForm);
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
    if(cep != null && cep != "" && this.clienteForm.get("logradouro").value == ""){
      this.buscaCep(cep);    
    }
  }
}