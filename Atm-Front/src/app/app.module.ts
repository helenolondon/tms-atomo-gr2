import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AtomComponentsModule } from './infra/atom/atom-components.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// Prime-ng
import { PrimeNGModule } from './infra/terceiros/prime-ng.module';
import {ConfirmationService, MessageService} from './infra/terceiros/prime-ng';

import { PropriedadesUsuarioComponent } from './modulos/usuarios/propriedades-usuario/propriedades-usuario.component';
import { AtomComponentsControlesModule } from './infra/atom/components/controles/atom-components-controles.module';
import { GerenciadorUsuariosComponent } from './modulos/usuarios/gerenciador-usuarios/gerenciador-usuarios.component';
import { SiteComponentsModule } from './site/site-components.module';
import { ConfiguracoesAcessoComponent } from './modulos/configuracoes/usuarios/configuracoes-acesso/configuracoes-acesso.component';
import { AlterarSenhaUsuarioComponent } from './modulos/usuarios/alterar-senha-usuario/alterar-senha-usuario.component';
import { GerenciadorClientesComponent } from './modulos/clientes/gerenciador-clientes/gerenciador-clientes.component';
import { PropriedadesClienteComponent } from './modulos/clientes/propriedades-cliente/propriedades-cliente.component';
import { AtmTokenInterceptor } from './modulos/autenticacao/atm-token-interceptor';
import { RegistrarSeComponent } from './modulos/usuarios/registrar-se/registrar-se.component';
import { BotaoPagamentoComponent } from './modulos/usuarios/registrar-se/botao-pagamento/botao-pagamento.component';
import { GerenciadorEmpresasComponent } from './modulos/empresas/gerenciador-empresas/gerenciador-empresas.component';
import { PropriedadesEmpresaComponent } from './modulos/empresas/propriedades-empresa/propriedades-empresa.component';

@NgModule({
  declarations: [
    AppComponent,
    GerenciadorUsuariosComponent,
    PropriedadesUsuarioComponent,
    ConfiguracoesAcessoComponent,
    AlterarSenhaUsuarioComponent,
    GerenciadorClientesComponent,
    PropriedadesClienteComponent,
    RegistrarSeComponent,
    BotaoPagamentoComponent,
    GerenciadorEmpresasComponent,
    PropriedadesEmpresaComponent
  ],
  imports: [
    BrowserModule,    
    AppRoutingModule,
    BrowserAnimationsModule,    
    HttpClientModule,
    SiteComponentsModule,
    AtomComponentsModule,
    AtomComponentsControlesModule,
    FormsModule,
    PrimeNGModule,
    ReactiveFormsModule
  ],
  providers: [
    // Prime-ng
    ConfirmationService, 
    MessageService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AtmTokenInterceptor,
      multi: true
  }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
