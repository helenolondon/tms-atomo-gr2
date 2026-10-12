import { Component, Input, OnInit } from '@angular/core';

const noop = () => {
};

@Component({
  selector: 'atm-radiobutton',
  templateUrl: './atm-radiobutton.component.html',
  styleUrls: ['./atm-radiobutton.component.scss']
})
export class AtmRadiobuttonComponent {
  @Input('label') checkLabel: string;
  @Input('readonly') checkReadonly: boolean = false;
  @Input('radio-value') radioValue: any;
  @Input('group-name') groupName: string;
    
  //The internal data model
  private innerValue: any = '';
  
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
