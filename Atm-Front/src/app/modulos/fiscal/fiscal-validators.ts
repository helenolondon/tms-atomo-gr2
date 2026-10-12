import { isDevMode } from "@angular/core";
import { AbstractControl, ValidatorFn } from "@angular/forms";
import { SystemService } from "src/app/infra/services/system.service";

enum TipoDeDocumento{
  cpf = 1,
  cnpj = 2
};

export namespace FiscalValidadors{

  export const cpfValidator = cpfCnpjValidatorFn(TipoDeDocumento.cpf); 
  export const cnpjValidator = cpfCnpjValidatorFn(TipoDeDocumento.cnpj);  

  function cpfCnpjValidatorFn(tipoDeDocumento: TipoDeDocumento): ValidatorFn {
    
    return (control: AbstractControl): {[key: string]: any} | null => {
      
      let innerValue: string = control.value;
      let soma = 0;
      let d1: number = null;
      let d2: number = null;
      let pesos: number[] = [];
      let tamanhoMaximo = tipoDeDocumento == TipoDeDocumento.cnpj ? 12: 9;

      if(innerValue == null || innerValue.length == 0){
        return null;
      };
      
      let innerValueOnlyNumbers: string = "";

      for(let i = 0; i < innerValue.length; i++){
        if(innerValue[i] in ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9']){
          innerValueOnlyNumbers += innerValue.slice(i, i + 1);
        };
      };

      innerValue = innerValueOnlyNumbers.slice(0, -2);

      if(innerValue.length < tamanhoMaximo){
        return {'cpfCnpjInvalido': {value: control.value}};
      };

      if(tipoDeDocumento == TipoDeDocumento.cnpj){
        pesos = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
      } else {
        pesos = [10, 9, 8, 7, 6, 5, 4, 3, 2];
      }

      let digIguais: boolean = true;

      for(let i = 0; i < innerValue.length; i++){
        if(innerValue[0] != innerValue[i]){
          digIguais = false;
        };

        soma += Number.parseInt(innerValue[i]) * pesos[i];
      };

      if(digIguais && !isDevMode()){
        return {'cpfCnpjInvalido': {value: control.value}};
      }

      d1 = soma % 11;
      d1 = 11 - d1;
      d1 = d1 > 9? 0: d1;

      innerValue = (innerValue + d1.toString());
      soma = 0;

      if(tipoDeDocumento == TipoDeDocumento.cnpj){
        pesos = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
      } else {
        pesos = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];
      }
      
      for(let i = 0; i < innerValue.length; i++){
        soma += Number.parseInt(innerValue[i]) * pesos[i];
      };

      d2 = soma % 11;
      d2 = 11 - d2;
      d2 = d2 > 9? 0: d2;

      innerValue = (innerValue + d2.toString());

      return (innerValue != innerValueOnlyNumbers) ? {'cpfCnpjInvalido': {value: control.value}}: null;
    }
  }
}
