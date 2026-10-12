import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'atm-modal',
  templateUrl: './atm-modal.component.html',
  styleUrls: ['./atm-modal.component.scss']
})
export class AtmModalComponent {
  @Input('width') width: string = null;
  @Input("resizable") resizable: boolean = false;
  @Input("title") title: string = "";

  public display: boolean = false;

  public close(){
    this.display = false;
  }

  public show(){
    this.display = true;
  }

  getWith(){
    return this.width;
  }
}
