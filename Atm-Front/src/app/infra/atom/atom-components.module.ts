/**
 * Autor: Heleno Sales Mesquita
 * Data : 26/01/2020
 * 
 * Components do Atom
 */

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AtomServerErrorComponent } from './components/mensagens/atom-server-error.component';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { AtmGerenciadorComponent } from './components/atm-gerenciador/atm-gerenciador.component';
import { DevExtremeModule } from 'devextreme-angular';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { PrimeNGModule } from '../terceiros/prime-ng.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AtomComponentsControlesModule } from './components/controles/atom-components-controles.module';
import { AtmComponentHostDirective } from './directives/atm-component-host.directive';
import { AtomComponentsDirectivesModule } from './directives/atom-components-directives.module';
import { AtmTabsComponent } from './components/atm-tabs/atm-tabs.component';
import { AtmModalComponent } from './components/atm-modal/atm-modal.component';
import { AtmPrimengDialogsContainersComponent } from './components/mensagens/atm-primeng-dialogs-containers.component';
import { AtomComponentsPipesModule } from './components/pipes/atom-components-pipes.module';

@NgModule({
  declarations: [
    AtomServerErrorComponent,
    AtmGerenciadorComponent,
    AtmTabsComponent,
    AtmModalComponent,
    AtmPrimengDialogsContainersComponent
  ],
  imports: [
    AppRoutingModule,
    CommonModule,    
    PrimeNGModule,
    FormsModule,
    AtomComponentsDirectivesModule,
    ReactiveFormsModule,
    AtomComponentsControlesModule,
    AtomComponentsPipesModule
  ],
  exports:[
    AtmGerenciadorComponent,
    AtomServerErrorComponent,
    AtmTabsComponent,
    AtmModalComponent,
    AtmPrimengDialogsContainersComponent
  ],
  entryComponents: [AtomServerErrorComponent]
})
export class AtomComponentsModule { }
