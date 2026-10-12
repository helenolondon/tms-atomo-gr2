import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CepPipe } from './cep.pipe';
import { CpfCnpjPipe } from './cpf-cnpj.pipe';

@NgModule({
  declarations: [
    CepPipe,
    CpfCnpjPipe
  ],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports:[
    CepPipe,
    CpfCnpjPipe
  ]
})
export class AtomComponentsPipesModule { }