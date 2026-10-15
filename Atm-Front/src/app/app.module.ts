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
import { GerenciadorMarcasComponent } from './modulos/marcas/gerenciador-marcas/gerenciador-marcas.component';
import { PropriedadesMarcaComponent } from './modulos/marcas/propriedades-marca/propriedades-marca.component';
import { GerenciadorProdutoTiposComponent } from './modulos/produtotipos/gerenciador-produto-tipos/gerenciador-produto-tipos.component';
import { PropriedadesProdutoTipoComponent } from './modulos/produtotipos/propriedades-produto-tipo/propriedades-produto-tipo.component';
import { GerenciadorProdutoTipoAgressividadesComponent } from './modulos/produtotipoagressividades/gerenciador-produto-tipo-agressividades/gerenciador-produto-tipo-agressividades.component';
import { PropriedadesProdutoTipoAgressividadeComponent } from './modulos/produtotipoagressividades/propriedades-produto-tipo-agressividade/propriedades-produto-tipo-agressividade.component';
import { GerenciadorUnidadesDeMedidaComponent } from './modulos/unidadesdemedida/gerenciador-unidades-de-medida/gerenciador-unidades-de-medida.component';
import { PropriedadesUnidadeDeMedidaComponent } from './modulos/unidadesdemedida/propriedades-unidade-de-medida/propriedades-unidade-de-medida.component';
import { GerenciadorTiposVeiculoComponent } from './modulos/tipoveiculo/gerenciador-tipos-veiculo/gerenciador-tipos-veiculo.component';
import { PropriedadesTipoVeiculoComponent } from './modulos/tipoveiculo/propriedades-tipo-veiculo/propriedades-tipo-veiculo.component';
import { GerenciadorProdutosComponent } from './modulos/produtos/gerenciador-produtos/gerenciador-produtos.component';
import { PropriedadesProdutoComponent } from './modulos/produtos/propriedades-produto/propriedades-produto.component';
import { GerenciadorProdutoGruposComponent } from './modulos/produtogrupos/gerenciador-produto-grupos/gerenciador-produto-grupos.component';
import { PropriedadesProdutoGrupoComponent } from './modulos/produtogrupos/propriedades-produto-grupo/propriedades-produto-grupo.component';
import { GerenciadorServicoCategoriasComponent } from './modulos/servicocategorias/gerenciador-servico-categorias/gerenciador-servico-categorias.component';
import { PropriedadesServicoCategoriaComponent } from './modulos/servicocategorias/propriedades-servico-categoria/propriedades-servico-categoria.component';

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
    PropriedadesEmpresaComponent,
    GerenciadorMarcasComponent,
    PropriedadesMarcaComponent,
    GerenciadorProdutoTiposComponent,
    PropriedadesProdutoTipoComponent,
    GerenciadorProdutoTipoAgressividadesComponent,
    PropriedadesProdutoTipoAgressividadeComponent,
    GerenciadorUnidadesDeMedidaComponent,
    PropriedadesUnidadeDeMedidaComponent,
    GerenciadorTiposVeiculoComponent,
    PropriedadesTipoVeiculoComponent,
    GerenciadorProdutosComponent,
    PropriedadesProdutoComponent,
    GerenciadorProdutoGruposComponent,
    PropriedadesProdutoGrupoComponent,
    GerenciadorServicoCategoriasComponent,
    PropriedadesServicoCategoriaComponent
  ],
  entryComponents: [
    PropriedadesMarcaComponent,
    PropriedadesProdutoTipoComponent,
    PropriedadesProdutoTipoAgressividadeComponent,
    PropriedadesUnidadeDeMedidaComponent,
    PropriedadesTipoVeiculoComponent,
    PropriedadesProdutoComponent,
    PropriedadesProdutoGrupoComponent,
    PropriedadesServicoCategoriaComponent
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
