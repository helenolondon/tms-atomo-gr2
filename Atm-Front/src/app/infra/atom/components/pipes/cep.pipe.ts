import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'cep'
})
export class CepPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): unknown {
    let result: string = value;

    if(result != undefined && result != null){
      result = result.replace(".", "").replace("/", "").replace("-", "")
      
      // 29.108-480
      // 29108480
      if(result.length <= 8){
        if(result.length == 7){
          result = "0" + result;
        };
        
        if(result.length == 8){
          return result.substring(0, 2) + "." + result.substring(2, 5) + "-" + result.substring(5, 8);
        };
      };
    };

    return value;
  };
}
