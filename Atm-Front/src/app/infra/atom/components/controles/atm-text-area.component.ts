import { Component, forwardRef, Input } from '@angular/core';
import { ControlValueAccessor, FormControl, NG_VALIDATORS, NG_VALUE_ACCESSOR, Validator } from '@angular/forms';

const noop = () => { };

const ATM_TEXT_AREA_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => AtmTextAreaComponent),
  multi: true
};

const ATM_TEXT_AREA_VALIDATOR: any = {
  provide: NG_VALIDATORS,
  useExisting: forwardRef(() => AtmTextAreaComponent),
  multi: true
};

@Component({
  // Internal shared controls use the "atm" prefix, matching the existing control library.
  // tslint:disable-next-line:component-selector
  selector: 'atm-text-area',
  templateUrl: './atm-text-area.component.html',
  styleUrls: ['./atm-text-area.component.scss'],
  providers: [ATM_TEXT_AREA_VALUE_ACCESSOR, ATM_TEXT_AREA_VALIDATOR]
})
export class AtmTextAreaComponent implements ControlValueAccessor, Validator {
  @Input() id: string;
  @Input() rows = 4;
  @Input() maxlength: number = null;
  // tslint:disable-next-line:no-input-rename
  @Input('style') inputStyle: any = { width: '100%' };
  @Input() focus = false;

  private innerValue: any = '';
  disabled = false;

  private onTouchedCallback: () => void = noop;
  private onChangeCallback: (value: any) => void = noop;

  get value(): any {
    return this.innerValue;
  }

  set value(value: any) {
    if (value !== this.innerValue) {
      this.innerValue = value;
      this.onChangeCallback(value);
    }
  }

  validate(control: FormControl): any {
    return null;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  onBlur(): void {
    this.onTouchedCallback();
  }

  writeValue(value: any): void {
    if (value !== this.innerValue) {
      this.innerValue = value;
    }
  }

  registerOnChange(fn: (value: any) => void): void {
    this.onChangeCallback = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouchedCallback = fn;
  }
}
