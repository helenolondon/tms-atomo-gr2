import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'atm-server-response-error',
  templateUrl: './atm-server-response-error.component.html',
  styleUrls: ['./atm-server-response-error.component.scss']
})
export class AtmServerResponseErrorComponent implements OnInit {

  @Input("erros") erros = [];

  constructor() { }

  ngOnInit() {
  }

}
