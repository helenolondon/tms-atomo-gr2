import { stringify } from '@angular/compiler/src/util';
import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, EventEmitter, HostListener, Input, OnInit, Output, ViewChild } from '@angular/core';

@Component({
  selector: 'atm-button',
  templateUrl: './atm-button.component.html',
  styleUrls: ['./atm-button.component.scss', './css/custom-controls.scss']
})
export class AtmButtonComponent implements OnInit, AfterViewInit {
  private oldLabel: string = null;

  @Input('label')  btnLabel: string;
  @Input('submit') submit: boolean = false; 
  @Input('disabled') disabled: boolean = false;
  @Input('type') btnBtnType: string;
  
  getBtnClass(){
    if(this.btnBtnType == undefined){
      return 'ui-button-raised'
    } else if(this.btnBtnType == "primary") {
      return 'ui-button-raised ui-button-success'
    } else if(this.btnBtnType == "secondary") {
      return 'ui-button-raised ui-button-secondary'
    }
  }

  @ViewChild('btn', {static: false}) btn: ElementRef;

  @Output('onClick') btnClickEvent: EventEmitter<AtmButtonComponent> = new EventEmitter();

  public aguarde(mensagem: string | null): void {
    if(mensagem){
      this.oldLabel = this.btnLabel;
      this.disabled = true;
      this.btnLabel = mensagem;
    }
    else{
      this.btnLabel = this.oldLabel;
      this.disabled = false;
      this.oldLabel = null;
    }
  }

  constructor() { 
  }

  ngAfterViewInit(): void {
  }

  btnClick(){
    this.btnClickEvent.emit(this);
  }

  ngOnInit() {
  }

}
