import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'cpfCnpj'
})
export class CpfCnpjPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): unknown {
    let result: string = value;

    if(result != undefined && result != null){
      result = result.replace(".", "").replace("/", "").replace("-", "")
      // 999.999.999-99
      if(result.length <= 11){
        if(result.length == 10){
          result = "0" + result;
        };
        
        if(result.length == 11){
          return result.substring(0, 3) + "." + result.substring(3, 6) + "." + result.substring(6, 9) + "-" + result.substring(9, 11);
        };
      };

      //99.999.99/999-99
      if(result.length <= 14){
        if(result.length == 13){
          result = "0" + result;
        };
        
        if(result.length == 14){
          return result.substring(0, 2) + "." + result.substring(2, 5) + "." + result.substring(5, 8) + "/" + result.substring(8, 12) + "-" + 
          result.substring(12, 14);
        };
      };
    };

    return value;
  }
}
