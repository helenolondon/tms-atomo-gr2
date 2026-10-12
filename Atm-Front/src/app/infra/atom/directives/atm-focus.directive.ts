import { AfterContentInit, Directive, ElementRef, Input } from '@angular/core';

@Directive({
  selector: '[AtmFocus]'
})
export class AtmFocusDirective implements AfterContentInit {
  @Input('AtmFocus') focus: Boolean;


  constructor(private el: ElementRef) {
   }

   public ngAfterContentInit() {
    if(this.focus){
        setTimeout(() => {
          this.el.nativeElement.focus();
      }, 500);
    }
  }
}
