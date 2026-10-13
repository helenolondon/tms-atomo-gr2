import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './site/login/login.component';
import { IndexComponent } from './site/index.component';
import { AutenticaGuard } from './modulos/autenticacao/route-guards/autentica.guard';
import { NaoAutorizadoComponent } from './site/login/nao-autorizado/nao-autorizado.component';
import { GerenciadorUsuariosComponent } from './modulos/usuarios/gerenciador-usuarios/gerenciador-usuarios.component';
import { PropriedadesUsuarioComponent } from './modulos/usuarios/propriedades-usuario/propriedades-usuario.component';
import { PageNotFoundComponent } from './site/page-not-found/page-not-found.component';
import { ConfiguracoesAcessoComponent } from './modulos/configuracoes/usuarios/configuracoes-acesso/configuracoes-acesso.component';
import { AlterarSenhaUsuarioComponent } from './modulos/usuarios/alterar-senha-usuario/alterar-senha-usuario.component';
import { PropriedadesClienteComponent } from './modulos/clientes/propriedades-cliente/propriedades-cliente.component';
import { GerenciadorClientesComponent } from './modulos/clientes/gerenciador-clientes/gerenciador-clientes.component';
import { EstadosCivisResolver } from './modulos/clientes/data-resolvers/EstadosCivisResolver';
import { SituacoesInscricaoEstadualResolver } from './modulos/fiscal/data-resolver/SituacoesInscricaoEstadualResolver';
import { ClientesTiposDePessoaResolver } from './modulos/clientes/data-resolvers/ClientesTiposDePessoaResolver';
import { RegistrarSeComponent } from './modulos/usuarios/registrar-se/registrar-se.component';
import { GerenciadorEmpresasComponent } from './modulos/empresas/gerenciador-empresas/gerenciador-empresas.component';
import { PropriedadesEmpresaComponent } from './modulos/empresas/propriedades-empresa/propriedades-empresa.component';
import { GerenciadorMarcasComponent } from './modulos/marcas/gerenciador-marcas/gerenciador-marcas.component';
import { GerenciadorProdutoTiposComponent } from './modulos/produtotipos/gerenciador-produto-tipos/gerenciador-produto-tipos.component';
import { GerenciadorProdutoTipoAgressividadesComponent } from './modulos/produtotipoagressividades/gerenciador-produto-tipo-agressividades/gerenciador-produto-tipo-agressividades.component';
import { GerenciadorUnidadesDeMedidaComponent } from './modulos/unidadesdemedida/gerenciador-unidades-de-medida/gerenciador-unidades-de-medida.component';
import { GerenciadorTiposVeiculoComponent } from './modulos/tipoveiculo/gerenciador-tipos-veiculo/gerenciador-tipos-veiculo.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent, data: {id: "Login"}},
  { path: 'nao-autorizado', component: NaoAutorizadoComponent, canActivate: [AutenticaGuard]},
  { path: 'index', component: IndexComponent, canActivate: [AutenticaGuard]},
  { path: 'usuarios', component: GerenciadorUsuariosComponent, canActivate: [AutenticaGuard]},
  { path: 'propriedades-usuarios', component: PropriedadesUsuarioComponent, canActivate: [AutenticaGuard]},
  { 
    path: 'clientes', 
    component: GerenciadorClientesComponent, 
    canActivate: [AutenticaGuard],
    resolve: {
      listaTiposDeClientes: ClientesTiposDePessoaResolver,
      listaEstadosCivis: EstadosCivisResolver,
      listaSitucoesDaInscricaoEstadual: SituacoesInscricaoEstadualResolver
    }
  },  
  { 
    path: 'propriedades-cliente', 
    component: PropriedadesClienteComponent,
    canActivate: [AutenticaGuard]
  },
  { 
    path: 'registrar-se', 
    component: RegistrarSeComponent,
    canActivate: []
  },

  {
    path: 'empresas',
    component: GerenciadorEmpresasComponent,
    resolve: {
      listaSitucoesDaInscricaoEstadual: SituacoesInscricaoEstadualResolver
    }
  },

  {
    path: 'marcas',
    component: GerenciadorMarcasComponent,
    canActivate: [AutenticaGuard]
  },

  {
    path: 'produtotipos',
    component: GerenciadorProdutoTiposComponent,
    canActivate: [AutenticaGuard]
  },

  {
    path: 'produtotipoagressividade',
    component: GerenciadorProdutoTipoAgressividadesComponent,
    canActivate: [AutenticaGuard]
  },

  {
    path: 'unidadesdemedida',
    component: GerenciadorUnidadesDeMedidaComponent,
    canActivate: [AutenticaGuard]
  },

  {
    path: 'tipoveiculo',
    component: GerenciadorTiposVeiculoComponent,
    canActivate: [AutenticaGuard]
  },

  {
    path: 'propriedades-empresa',
    component: PropriedadesEmpresaComponent,
    canActivate: []
  },

  { path: 'alterar-senha', component: AlterarSenhaUsuarioComponent, canActivate: [AutenticaGuard]},  
  { path: 'configuracoes-acesso', component: ConfiguracoesAcessoComponent, canActivate: [AutenticaGuard]},
  { path: ''   , redirectTo: "/processos", pathMatch: "full" },
  { path: '**'   , component: PageNotFoundComponent, canActivate: [AutenticaGuard]}
]

@NgModule({
  declarations: [PageNotFoundComponent],
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
