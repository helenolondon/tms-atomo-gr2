import { AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild, ViewChildren } from '@angular/core';

@Component({
  selector: 'atm-tabs',
  templateUrl: './atm-tabs.component.html',
  styleUrls: ['./atm-tabs.component.scss']
})
export class AtmTabsComponent {
  @Input("headers") headers: string[] = [];

  get header1(): string{
    if(this.headers.length > 0){
      return this.headers[0];
    };

    return null;
  }

  get header2(): string{
    if(this.headers.length > 1){
      return this.headers[1];
    };
    
    return null;
  }

  get header3(): string{
    if(this.headers.length > 2){
      return this.headers[2];
    };
    
    return null;
  }

  get header4(): string{
    if(this.headers.length > 3){
      return this.headers[3];
    };
    
    return null;
  }

  get header5(): string{
    if(this.headers.length > 4){
      return this.headers[4];
    };
    
    return null;
  }
}
