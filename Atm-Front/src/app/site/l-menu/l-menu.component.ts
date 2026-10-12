import { Component, OnInit, ViewChild, Input, Output, EventEmitter } from '@angular/core';
import { AutenticaService } from 'src/app/modulos/autenticacao/services/autentica.service';
import { MatDrawer } from '@angular/material/sidenav';
import { Router, RouterLink, NavigationEnd } from '@angular/router';
import { Route } from '@angular/compiler/src/core';

@Component({
  selector: 'l-menu',
  templateUrl: './l-menu.component.html',
  styleUrls: ['./l-menu.component.scss']
})
export class LMenuComponent implements OnInit {

  @ViewChild("drawer",{ static: true }) drawer: MatDrawer;

  @Output('on-sair') _onSair:  EventEmitter<any> = new EventEmitter();
  
  urlCorrente: string = null;

  links: any[] = [
    {
      texto: "Empresas",
      link: "empresas"
    }
  ];

  public drawerOpened: boolean = false;

  constructor(private autenticaSvr: AutenticaService, private router: Router) { 
    router.events.subscribe(event => {

      if (event instanceof NavigationEnd ) {
        this.urlCorrente = event.url;
      }
    });
   }

  public Autenticado(): boolean{
    return this.autenticaSvr.Autenticado;
  }

  public toggleDrawer(){
    this.drawerOpened = !this.drawerOpened;
  }

  public onSairClick(){
    this._onSair.emit();
  }

  ngOnInit() {
  }
}
