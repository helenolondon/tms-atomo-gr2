import { Component, forwardRef, Input, OnInit } from '@angular/core';
import { AbstractControl, ControlValueAccessor, FormControl, NG_VALIDATORS, NG_VALUE_ACCESSOR, ValidationErrors, Validator } from '@angular/forms';
import { isNullOrUndefined } from 'util';
import { AtmTextEditorComponent } from './atm-text-editor.component';

@Component({
  selector: 'atm-val-message',
  templateUrl: './atm-val-message.component.html',
  styleUrls: ['./atm-val-message.component.scss']
})
export class AtmValMessageComponent implements OnInit {

  @Input("control") formControl: FormControl;
  @Input("valid") _valid: boolean = undefined; 

  get valid(): boolean {

    if(this._valid != undefined){
      if(this._valid){
        return true;
      } else {
        return false;
      }
    };

    if(this.formControl != undefined){
      return !(this.formControl.invalid && 
        (this.formControl.touched || this.formControl.dirty || this.formControl.parent.touched));
    }
  }

  constructor() { 

  }

  ngOnInit() {
  }
}
