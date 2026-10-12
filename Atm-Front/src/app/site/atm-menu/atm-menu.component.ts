import { Component, OnInit, Input, Output, ViewChild } from '@angular/core';
import { AtmMenuItem } from './AtmMenuItem';
import { trigger, state, style, animate, transition} from '@angular/animations';
import { AtmMenuClickEventArgs } from './AtmMenuClickEvent';
import { isNullOrUndefined } from 'util';
import { Router } from '@angular/router';

@Component({
  selector: 'atm-menu',
  templateUrl: './atm-menu.component.html',
  styleUrls: ['./atm-menu.component.scss'],
  animations: [
    trigger('menuFade', [
      transition(':enter', [
        style({opacity: 0}),
        animate('0.3s', style({opacity: 1}))
      ]), 
      transition(':leave', [
        animate('0.25s', style({opacity: 0}))
      ]),     
    ]),
    
    // Each unique animation requires its own trigger. The first argument of the trigger function is the name
    trigger('rotatedState', [
        state('default', style({ transform: 'rotate(90deg)' })),
        state('rotated', style({ transform: 'rotate(0)' })),
        transition('rotated => default', animate('130ms ease-out')),
        transition('default => rotated', animate('130ms ease-in'))]
        )      
      ]
})
export class AtmMenuComponent implements OnInit {
  
  public subMenu: AtmMenuComponent;
  private _menuSelecionado: AtmMenuItem;
  private parentMenu: AtmMenuComponent;

  @Input('menu-itens') menuItens: AtmMenuItem[];
  @Input('menu-ident') menuIdent: number = 1;
  @Input('parent-menu-item') parentMenuItem: AtmMenuItem = null;
  @Input('parent-menu-component') 
  set setParentMenu(value: AtmMenuComponent) {
    this.parentMenu = value;
    this.parentMenu.subMenu = this;
  }

  constructor(private router: Router) { 
    this._menuSelecionado = null;
  }

  ngOnInit() {

    if(!isNullOrUndefined(this.menuItens))
    {
      this.menuItens.forEach(m => {
        this.setParentMenuOnItens(m);
      });
    }
  }

  private setParentMenuOnItens(item: AtmMenuItem): void{
    
    if(isNullOrUndefined(item) || isNullOrUndefined(item.itens))
    {
      return;
    }
    
    item.itens().forEach(_item => {
      if(_item.itens && _item.itens.length > 0)
      {
        this.setParentMenuOnItens(_item);
      }

      _item.parent = item;
    })
  }

  public menuClick(menu: AtmMenuItem){
    
    if(menu.itens().length > 0)
    {
      menu.aberto = !menu.aberto;
      return;
    }

    this.Selecionar(menu, true);

    if(menu.onClick){
      menu.onClick(new AtmMenuClickEventArgs(menu));
    }

    if(menu.link){
      this.router.navigate([menu.link]);
    }
  }

  deSelMenu(menu: AtmMenuItem){
    if(menu.itens().length > 0)
    {
      menu.itens().forEach(m => {
        this.deSelMenu(m);
      });      
    }
    menu.selecionado = false;
  }

  public Selecionar(menu: AtmMenuItem, habilitado: boolean){
    if(menu.parent)
    {
      menu.parent.itens().forEach(mn => {
        if(mn !== menu)
        {
          this.deSelMenu(mn);
        }
      });
      
      if(habilitado)
      {
        this.Selecionar(menu.parent, true);
      }
    }
    
    if(menu.itens && menu.itens.length > 0 && !habilitado)
    {
      menu.itens().forEach(mn => {
        this.Selecionar(mn, false);
      });
    }

    menu.selecionado = habilitado;
  }

  public retParentMenu(){
    return this.parentMenu != undefined ? this.parentMenu: null;
  }

  public retParentMenuItem(){
    return this.parentMenuItem != undefined ? this.parentMenuItem: null;
  } 

  public retButtonIdent(): string {
    let nivel = 10;
    let menu = this.parentMenu;

    while(menu != null)
    {
      nivel += 10; 

      menu = menu.parentMenu;
    }
    
    return nivel.toString() + 'px';
  }

  public menuSelecionado(menu: AtmMenuItem): boolean {
    return this._menuSelecionado && this._menuSelecionado === menu;
  }

  public retMenuState(menu: AtmMenuItem): string {
    if(menu.aberto)
    {  
      return 'default'
    }
    else
    {
      return 'rotated';
    }
  }
}
