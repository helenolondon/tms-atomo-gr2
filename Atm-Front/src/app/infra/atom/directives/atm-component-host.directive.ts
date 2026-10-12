import { ViewContainerRef } from '@angular/core';
import { Directive } from '@angular/core';

@Directive({
  selector: '[appHost]'
})
export class AtmComponentHostDirective {

  constructor(public viewContainerRef: ViewContainerRef) { }

}
