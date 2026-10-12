import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'atm-component',
  templateUrl: './atm-component.component.html',
  styleUrls: ['./atm-component.component.scss']
})
export class AtmComponentComponent implements OnInit {
  
  private _erros: Array<string>;
  public get erros(): Array<string> { return this._erros;  };
  
  constructor() { 
    this._erros = [""];
  }

  ngOnInit() {
  }
}
