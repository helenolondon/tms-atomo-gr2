import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'atm-fieldset',
  templateUrl: './atm-fieldset.component.html',
  styleUrls: ['./atm-fieldset.component.scss']
})
export class AtmFieldsetComponent implements OnInit {
  @Input() title: string;
  
  constructor() { }

  ngOnInit() {
  }

}
