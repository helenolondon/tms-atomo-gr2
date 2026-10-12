import { Component, forwardRef, Input, EventEmitter, Output } from '@angular/core';
import { FormControl, NG_VALIDATORS, NG_VALUE_ACCESSOR } from '@angular/forms';

const noop = () => {
};

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => AtmTextbuttonComponent), 
    multi: true
  };
  
  export const CUSTOM_INPUT_CONTROL_VALIDATOR: any = {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => AtmTextbuttonComponent),
    multi: true
  };

@Component({
  selector: 'atm-textbutton',
  templateUrl: './atm-textbutton.component.html',
  styleUrls: ['./atm-textbutton.component.scss'],
  providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR, CUSTOM_INPUT_CONTROL_VALIDATOR]
})
export class AtmTextbuttonComponent {
  @Input() mask: string
  @Input() icon: string;

  @Output("onButtonClick") OnButtonClickCallBack: EventEmitter<any> = new EventEmitter()
  @Output("onBlur") OnBlurCallBack: EventEmitter<any> = new EventEmitter()
  
  constructor() { }

  getIcon(){
    return 'pi pi-' + this.icon;
  }

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

      if(this.OnBlurCallBack != null && this.OnBlurCallBack != undefined){
        this.OnBlurCallBack.emit()
      }
  }

  onBlurEvent() {
    if(this.OnBlurCallBack != null && this.OnBlurCallBack != undefined){
      this.OnBlurCallBack.emit(this.value)
    }
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
  
  onButtonClick(value){
    this.OnButtonClickCallBack.emit(value)
  }
}
