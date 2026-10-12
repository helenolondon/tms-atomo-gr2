import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { SideMenuComponent } from './side-menu/side-menu.component';
import { NaoAutorizadoComponent } from './login/nao-autorizado/nao-autorizado.component';
import { IndexComponent } from './index.component';
import { DevExtremeModule } from 'devextreme-angular';
import { AtomAlertComponent } from '../infra/atom/components/mensagens/atom-alert/atom-alert.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AtmComponentComponent } from '../infra/atom/components/atm-component/atm-component.component';
import { AtmMenuComponent } from './atm-menu/atm-menu.component';
import { LMenuComponent } from './l-menu/l-menu.component';
import { RouterModule } from '@angular/router';
import { AngularMaterialModule } from '../infra/terceiros/angular-material.module';

@NgModule({
  declarations: [
    AtmMenuComponent,
    LMenuComponent,
    AtmComponentComponent,
    AtomAlertComponent,
    LoginComponent,
    NaoAutorizadoComponent,
    IndexComponent,    
    SideMenuComponent
  ],
  imports: [
    CommonModule,
    DevExtremeModule,
    FormsModule,
    ReactiveFormsModule,
    AngularMaterialModule,    
    FormsModule,
    ReactiveFormsModule,
    RouterModule
  ],
  exports:[
    AtmComponentComponent,
    LoginComponent,
    NaoAutorizadoComponent,
    IndexComponent,    
    SideMenuComponent,
    AtomAlertComponent,
    AtmMenuComponent,
    LMenuComponent
  ]
})
export class SiteComponentsModule { }
