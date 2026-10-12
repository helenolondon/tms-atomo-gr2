import { Component, forwardRef, Input, EventEmitter, Output, OnInit, AfterViewInit, ChangeDetectorRef, AfterContentInit, OnChanges, SimpleChanges } from '@angular/core';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { noop } from 'rxjs';
import { isArray } from 'util';

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => AtmDropdownComponent),
  multi: true
};

@Component({
  selector: 'atm-dropdown',
  templateUrl: './atm-dropdown.component.html',
  styleUrls: ['./atm-dropdown.component.scss'],
  providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR]
})
export class AtmDropdownComponent implements ControlValueAccessor, OnChanges {
  @Input('disabled') disabled = false;
  @Input('style') style = { width: '100%' };
  @Input('type') tipo: string = "text";
  @Input('focus') focus: boolean = false;
  @Input('feedback') feedback: boolean = true;
  @Input() items: Array<any>;
  @Output("onChange")  onChange: EventEmitter<string> = new EventEmitter(); 

  //The internal data model
  private innerValue: any = '';
  innerItems = [];

  public validate(c: FormControl) {
    return null;
  }

  //Placeholders for the callbacks which are later providesd
  //by the Control Value Accessor
  private onTouchedCallback: () => void = noop;
  private onChangeCallback: (_: any) => void = noop;

  constructor(private cdRef: ChangeDetectorRef) { }
  
  ngOnChanges(changes: SimpleChanges): void {
    this.innerItems = [];

    if(isArray(this.items)){
      this.items.forEach(obj => {
        this.innerItems.push({
          label: obj.descricao,
          value: obj.codigo
        });
      });
    }
  }

  change (arg: any)
  {
    this.onChange.emit(this.innerValue); 
  };
  
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
