import { AbstractControl, FormControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function passwordMatchValidator(control1: AbstractControl, control2: AbstractControl): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      return control1.value != control2.value ? {message: "Senhas não são iguais"} : null;
    };
  }