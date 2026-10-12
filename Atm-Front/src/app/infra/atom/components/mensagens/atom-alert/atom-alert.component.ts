import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'atom-alert',
  template: 
  `<div>
	  <ng-content></ng-content><button type="button" (click)="onFecharClick()">X</button>
  </div>`,
  styleUrls: ['./atom-alert.component.scss']
})
export class AtomAlertComponent implements OnInit {
  private _hidden;

  @HostBinding("hidden") 
  get hidden() { return this._hidden; };
  set hidden(val: boolean) { this._hidden = val; };

  onFecharClick(){
    this._hidden = true;
  }

  constructor() { 
    this._hidden = true;
  }

  ngOnInit() {
  }

}
