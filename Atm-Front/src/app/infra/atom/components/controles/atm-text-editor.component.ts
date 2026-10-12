import { Component, forwardRef, HostBinding, Injector, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { NgControl, NG_VALIDATORS, Validator } from '@angular/forms';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';

const noop = () => {
};

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => AtmTextEditorComponent),
    multi: true
  };
  
  export const CUSTOM_INPUT_CONTROL_VALIDATOR: any = {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => AtmTextEditorComponent),
    multi: true
  };

@Component({
  selector: 'atm-text-editor',
  templateUrl: './atm-text-editor.component.html',
  styleUrls: ['./atm-text-editor.component.scss'],
  providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR, CUSTOM_INPUT_CONTROL_VALIDATOR]
})
export class AtmTextEditorComponent implements ControlValueAccessor, Validator {
  
  @Input('style') style = { width: '100%' };
  @Input('type') tipo: string = "text";
  @Input('focus') focus: boolean = false;
  @Input('feedback') feedback: boolean = true;
  @Input('maxlength') maxlength: number = null; 

  //The internal data model
  private innerValue: any = '';
  public disabled = false;

  public validate(c: FormControl) {
    return null;
  }

  //Placeholders for the callbacks which are later providesd
  //by the Control Value Accessor
  private onTouchedCallback: () => void = noop;
  private onChangeCallback: (_: any) => void = noop;

  //get accessor
  get value(): any {
      return this.innerValue;
  };

  //set accessor including call the onchange callback
  set value(v: any) {
      if (v !== this.innerValue) {
          this.innerValue = v;
          this.onChangeCallback(v);
      }
  }

  setDisabledState(isDisabled: boolean) {
    this.disabled = isDisabled;
  }

  //Set touched on blur
  onBlur() {
      this.onTouchedCallback();
  }

  //From ControlValueAccessor interface
  writeValue(value: any) {
      if (value !== this.innerValue) {
          this.innerValue = value;
      }
  }

  //From ControlValueAccessor interface
  registerOnChange(fn: any) {
      this.onChangeCallback = fn;
  }

  //From ControlValueAccessor interface
  registerOnTouched(fn: any) {
      this.onTouchedCallback = fn;
  }
}