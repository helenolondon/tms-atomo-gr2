import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AtmButtonComponent } from './atm-button.component';
import { AtmCheckboxComponent } from './atm-checkbox.component';
import { AtmDateEditorComponent } from './atm-date-editor.component';
import { AtmServerResponseErrorComponent } from './atm-server-response-error.component';
import { AtmTextEditorComponent } from './atm-text-editor.component';
import { AtmTextAreaComponent } from './atm-text-area.component';
import { AtmValMessageComponent } from './atm-val-message.component';
import { PrimeNGModule } from 'src/app/infra/terceiros/prime-ng.module';
import { FormsModule } from '@angular/forms';
import { AtomComponentsDirectivesModule } from '../../directives/atom-components-directives.module';
import { AtmPanelComponent } from './atm-panel.component';
import { AtmFieldsetComponent } from './atm-fieldset.component';
import { AtmDropdownComponent } from './atm-dropdown.component';
import { AtmTextbuttonComponent } from './atm-textbutton.component';
import { AtmMaskEditorComponent } from './atm-mask-editor.component';
import { AtmAutocompleteComponent } from './atm-autocomplete.component';
import { AtmFileUploadComponent } from './atm-fileupload.component';
import { AtmGridComponent } from './atm-grid/atm-grid.component';
import { AtmRadiobuttonComponent } from './atm-radiobutton.component';

@NgModule({
  declarations: [
    AtmButtonComponent,
    AtmCheckboxComponent,
    AtmDateEditorComponent,
    AtmServerResponseErrorComponent,
    AtmTextEditorComponent,
    AtmTextAreaComponent,
    AtmValMessageComponent,
    AtmPanelComponent,
    AtmFieldsetComponent,
    AtmDropdownComponent,
    AtmTextbuttonComponent,
    AtmMaskEditorComponent,
    AtmAutocompleteComponent,
    AtmFileUploadComponent,
    AtmGridComponent,
    AtmRadiobuttonComponent
  ],
  imports: [
    CommonModule,
    PrimeNGModule,
    FormsModule,
    AtomComponentsDirectivesModule
  ],
  exports:[
    AtmButtonComponent,
    AtmCheckboxComponent,
    AtmDateEditorComponent,
    AtmServerResponseErrorComponent,
    AtmTextEditorComponent,
    AtmTextAreaComponent,
    AtmValMessageComponent,
    AtmPanelComponent,
    AtmFieldsetComponent,
    AtmDropdownComponent,
    AtmTextbuttonComponent,
    AtmMaskEditorComponent,
    AtmAutocompleteComponent,
    AtmFileUploadComponent,
    AtmGridComponent,
    AtmRadiobuttonComponent
  ]
})
export class AtomComponentsControlesModule { }