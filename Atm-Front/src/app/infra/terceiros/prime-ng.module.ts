/**
 * Autor: Heleno Sales Mesquita
 * Data.: 24/07/2020 
 * 
 * Neste módulo serão colocadas as dependêncios do devextreme
 */

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckboxModule } from 'primeng-lts/checkbox';
import { DialogModule } from 'primeng-lts/dialog';
import { ButtonModule } from 'primeng-lts/button';
import { SidebarModule } from 'primeng-lts/sidebar';
import { ConfirmDialogModule}  from 'primeng-lts/confirmdialog';
import { ToastModule } from 'primeng-lts/toast';
import { TableModule } from 'primeng/table';
import { MultiSelectModule } from 'primeng/multiselect';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { ProgressBarModule } from 'primeng/progressbar';
import { TooltipModule } from 'primeng/tooltip';
import { InputTextModule } from 'primeng/inputtext';
import {InputTextareaModule} from 'primeng/inputtextarea';
import { PasswordModule } from 'primeng/password';
import { TabViewModule } from 'primeng/tabview';
import {TreeModule} from 'primeng/tree';
import {PanelModule} from 'primeng/panel';
import {FieldsetModule} from 'primeng/fieldset';
import {InputMaskModule} from 'primeng/inputmask';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {FileUploadModule} from 'primeng/fileupload';
import {RadioButtonModule} from 'primeng/radiobutton';

@NgModule({
  declarations: [
    
  ],
  imports: [
    CommonModule
  ],
  providers: [
  ],

  exports: [
    CheckboxModule,
    DialogModule,
    ButtonModule,
    SidebarModule,
    ConfirmDialogModule,
    ToastModule,
    TableModule,
    MultiSelectModule,
    CalendarModule,
    DropdownModule,
    ProgressBarModule,
    TooltipModule,
    InputTextModule,
    InputTextareaModule,
    PasswordModule,
    TabViewModule,
    TreeModule,
    PanelModule,
    FieldsetModule,
    InputMaskModule,
    AutoCompleteModule,
    FileUploadModule,
    RadioButtonModule
  ]
})
export class PrimeNGModule { }
