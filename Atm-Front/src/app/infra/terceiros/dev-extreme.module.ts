/**
 * Autor: Heleno Sales Mesquita
 * Data.: 16/01/2020 
 * 
 * Neste módulo serão colocadas as dependêncios do devextreme
 */

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import notify from 'devextreme/ui/notify';
import { 
  DxDataGridModule, 
  DxSelectBoxModule, 
  DxDropDownButtonModule, 
  DxToolbarModule, 
  DxButtonGroupModule 
} from 'devextreme-angular';

import { 
  DxButtonModule, 
  DxTextBoxModule,
  DxFormModule, 
  DxLoadIndicatorModule, 
  DxCheckBoxModule 
} from "devextreme-angular";

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule    
  ],
  exports: [
    DxButtonModule,
    DxTextBoxModule,
    DxFormModule,
    DxLoadIndicatorModule,
    DxCheckBoxModule,
    DxDataGridModule,
    DxSelectBoxModule,
    DxDropDownButtonModule,
    DxToolbarModule,
    DxButtonGroupModule
  ]
})
export class DevExtremeModule { }
