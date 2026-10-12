import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AtmFocusDirective } from './atm-focus.directive';
import { AtmComponentHostDirective } from './atm-component-host.directive';


@NgModule({
  declarations: [
    AtmFocusDirective,
    AtmComponentHostDirective
  ],
  exports: [
    AtmFocusDirective,
    AtmComponentHostDirective
  ],
  imports: [
    CommonModule
  ]
})
export class AtomComponentsDirectivesModule { }
