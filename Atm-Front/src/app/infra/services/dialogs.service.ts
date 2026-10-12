/**
 * Autor: Heleno Sales Mesquita
 * Data : 22/07/2020
 * 
 * Bibioteca de mensagens.
 */

import { Injectable } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng-lts/api';

@Injectable({
  providedIn: 'root'
})
export class DialogsService {

  // Tempo de fade in e out do toastr
  static readonly TOAST_EASYTIME: number = 500;
  
  // Tempo que o toast aguarda antes de fechar automaticamente
  static readonly TOAST_TIMEOUT: number = 0;
  
  constructor(private primeNgconfirmationService: MessageService, private confirmationService: ConfirmationService) { }

  /** 
   * Mostra um toast no canto superior direito da viewport informando um erro. 
   * @param mensagemDeErroDeConexao Mensagem contendo uma informação de falha de conexão com o servidor
   * */ 
  showErroDeConexao(mensagemDeErroDeConexao: string){
    this.primeNgconfirmationService.add({severity:'error', closable: true, life: 500000, 
      detail: mensagemDeErroDeConexao});
  }

  /** 
   * Mostra um toast no canto superior de sucesso direito da viewport. 
   * @param mensagem Mensagem contendo uma informação operação bem sucedida.
   * */ 
   sucesso(mensagem: string){
    this.primeNgconfirmationService.add({severity:'success', closable: true, life: 3000, 
      detail: mensagem});
  }
  
  alerta(mensagem: string){
    this.primeNgconfirmationService.add({severity:'warn', closable: true, life: 3000, 
      detail: mensagem});
  }

  /** 
   * Mostra um toast no canto superior de erro direito da viewport. 
   * @param mensagem Mensagem contendo uma informação operação mal sucedida.
   * */ 
   erro(mensagem: string){
    this.primeNgconfirmationService.add({severity:'error', closable: true, life: 3000, 
      detail: mensagem});
  }

  /**
   * Tratamento de erro específico para http status 501
   * @param mensagemDeErro mensamge de erro
   */
  showErroHttp501(mensagemDeErro: string){
    this.primeNgconfirmationService.add({severity:'error', closable: true, life: 500000, 
      detail: mensagemDeErro});
  }

  confirmar(mensagem: string, acceptCallBack = () => {}, rejectCallBack = () => {}, titulo: string = 'Confirma'){
      return this.confirmationService.confirm({
        message: mensagem,
        accept: acceptCallBack,
        reject: rejectCallBack,
        acceptLabel: 'Sim',
        rejectLabel: 'Não',
        header: titulo
    });
  }

  showModalMessage(mensagem: string, callBack = () => {}, titulo: string = 'Informação'){
    return this.confirmationService.confirm({
      message: mensagem,
      accept: callBack,
      icon: 'pi pi-info',
      acceptLabel: 'Ok',
      rejectVisible: false,
      header: titulo
  });
}
}
