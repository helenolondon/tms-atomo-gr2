import { noop } from 'rxjs';
import { Component, EventEmitter, forwardRef, Input, OnInit, Output, ViewChild } from '@angular/core';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { AutoComplete } from 'primeng-lts/autocomplete';

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => AtmAutocompleteComponent),
  multi: true
};

@Component({
  selector: 'atm-autocomplete',
  templateUrl: './atm-autocomplete.component.html',
  styleUrls: ['./atm-autocomplete.component.scss'],
  providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR]
})
export class AtmAutocompleteComponent implements ControlValueAccessor {
  
  @ViewChild('autocomplete', {static: false}) autocomplete: AutoComplete;

  @Input('disabled') disabled = false;
  @Input('results') results: any[];
  @Input('displayField') displayField: string;
  @Input('key') key: string;  
  
  @Output("onSearch") onSearch: EventEmitter<string> = new EventEmitter();
  @Output("onChange") onChange: EventEmitter<string> = new EventEmitter(); 

  //The internal data model
  private innerValue: any = '';
  comboValue: null;

  public validate(c: FormControl) {
    return null;
  }

  //Placeholders for the callbacks which are later providesd
  //by the Control Value Accessor
  private onTouchedCallback: () => void = noop;
  private onChangeCallback: (_: any) => void = noop;

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

  ngAfterViewInit() {
    this.autocomplete.onOverlayAnimationDone = (event) => {};
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

  onSelectOrUnselect(e){
    this.value = e;
    this.onChange.emit(e);
  }

  onClear(){
    this.value = null;
  }

  search(event){
    if(this.onSearch != null){
      this.onSearch.emit(event.query);
    }
  }
}
