/**
 * Autor: Heleno Sales Mesquita
 * Data : 06/08/2020
 * 
 * Página do gerenciador
 */
import { Component, OnInit, ViewChild, Input, ComponentFactoryResolver, ViewContainerRef, AfterViewInit, ViewChildren, QueryList, ChangeDetectorRef, Type, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService, LazyLoadEvent, Message } from 'primeng-lts/api';
import { TabView, TabViewNav } from 'primeng-lts/tabview';
import { Observable } from 'rxjs';
import { DialogsService } from 'src/app/infra/services/dialogs.service';
import { PersistenceService } from 'src/app/infra/services/persistence.service';
import { SystemService } from 'src/app/infra/services/system.service';
import { getObjectPropertyValue } from 'src/app/infra/utils/hsm-utils';
import { AtmComponentHostDirective } from '../../directives/atm-component-host.directive';
import { AtmModalComponent } from '../atm-modal/atm-modal.component';
import { IAtmPropriedadesComponent } from '../atm-propriedades/IAtm-Propriedades-Component';
import { AtmButtonComponent } from '../controles/atm-button.component';
import { AtmGerenciadorGridBtnDef } from './atm-gerenciador-grid-btn-def';
import { AtmGerenciadorLazyLoadingPageInfo } from './atm-gerenciador-lazy-loading-page-info';
import { AtmGerenciadorLazyLoadingPageSortByInfo } from './atm-gerenciador-lazy-loading-page-sort-by-info';
import { AtmGerenciadorToolBarBtnDef } from './atm-gerenciador-toolbar-btn-def';

@Component({
  selector: 'atm-gerenciador',
  templateUrl: './atm-gerenciador.component.html',
  styleUrls: ['./atm-gerenciador.component.scss'],
  providers: [ConfirmationService]
})
export class AtmGerenciadorComponent implements AfterViewInit {
  @ViewChild('mdConfiguracoes', {static: false}) mdConfiguracoes: AtmModalComponent;
  @ViewChild(TabView, {static: true}) tabView: TabView;
  @ViewChildren(AtmComponentHostDirective) tabsHost: QueryList<AtmComponentHostDirective>;

  @Input("show-editar-button") showEditarButton: boolean = true;
  @Input("show-excluir-button") showExcluirButton: boolean = true;
  @Input('dataSource') dataSource;
  @Input("colunas") cols;
  @Input("storage-key") storageKey: string = null;
  @Input('col-editar-tab') colEditarTab: string;
  @Input() getModelApi: (model: any) => Observable<any>;
  @Input() deleteModelApi: (model: any) => Observable<any>;
  @Input('titulo') managerTitulo: string;
  @Input("left-grid-buttons") leftGridCustomButtons: AtmGerenciadorGridBtnDef[] = [];
  @Input("right-grid-buttons") rightGridCustomButtons: AtmGerenciadorGridBtnDef[] = [];
  @Input("toolbar-buttons") toolbarCustomButtons: AtmGerenciadorToolBarBtnDef[] = [];
  @Input("enable-multiple-selection") enableMultipleSelection: boolean = false;
  @Input("lazy-load-records") lazyLoadRecords: boolean = false;
  @Output('onToolbarButtonClick') onToolbarButtonClickEv: EventEmitter<any> = new EventEmitter();
  @Output('onLazyLoadRecords') onLazyLoadEv: EventEmitter<AtmGerenciadorLazyLoadingPageInfo> = new EventEmitter();
  @Output() onRefresh: EventEmitter<any> = new EventEmitter();
  @Output() onCustomButtonClick: EventEmitter<any> = new EventEmitter();

  public componentFactory: any; 

  totalRecords = 0;

  private loading: boolean = false

  filtroVisivel = false;
  mdNovoVisivel: boolean = false;
  activeTabIndex = 0;
  selectedObjects: any[] = [];
  rowData: any;
  lazyLoadingPageInfo: AtmGerenciadorLazyLoadingPageInfo = new AtmGerenciadorLazyLoadingPageInfo(1, 15);
  configuracoesFormGroup: FormGroup = this.criarConfiguracoesFormGroup(); 

  tabs: any[] = [
  ]

  constructor(private confirmationService: ConfirmationService, public cdRef: ChangeDetectorRef,
    private dialogs: DialogsService, private systemService: SystemService, 
    private componentFactoryResolver: ComponentFactoryResolver, private persistenceService: PersistenceService) { 
      this.loadGridSettings();
    }

  ngAfterViewInit(): void {
    this.cdRef.detectChanges();

    if(this.systemService.StartSearchEnabled && this.onRefresh != null){
      this.onRefresh.emit();
    };
  }

  ngOnChanges(){
    this.loadGridSettings();
  }

  public getTextAlign(col){
    if(col.textAlign != undefined && col.textAlign != null){
      if(col.textAlign == 'right'){
        return col.textAlign;
      }
    }

    return 'left';
  }

  setLoading(value: boolean){
    this.loading = value;
    this.cdRef.detectChanges();
  }

  getLoading(){
    return this.loading;
  }

  loadGridSettings(){
    
    let gridSettings = this.persistenceService.getItemObject(this.storageKey + "-gridSettings");

    if(gridSettings != null){
      this.lazyLoadingPageInfo.numberOfRows = gridSettings['numberOfRows'];
    }
  }
  
  public onNovo(): void {
    this.novaTabProps('Novo');
  }

  criarConfiguracoesFormGroup(): FormGroup{
    let frm = new FormGroup({
      qtdLinhasNaPagina: new FormControl(15, Validators.required)
    });

    return frm;
  }

  public _onCustomButtonClick(row, key){
    if(this.onCustomButtonClick != undefined){
      this.onCustomButtonClick.emit({row, key});
    };
  }

  public onEditarClick(model){
    let tabTitulo = 'Editar';

    if(this.componentFactory == undefined){
      return;
    };

    if(this.colEditarTab != undefined && this.colEditarTab != null){
      tabTitulo = getObjectPropertyValue(model, this.colEditarTab);
    }

    this.getModelApi(model).subscribe(async (model) => {
      await this.novaTabProps(tabTitulo).setModel(model);
    });
  };

  public onExcluirClick(data){

    this.confirmationService.confirm({
          message: 'Deseja realmente excluir?',
          accept: () => {
              if(this.deleteModelApi != undefined){
                this.deleteModelApi(data).subscribe(
                  { 
                    next: () => {
                      this.refresh();
                      this.dialogs.sucesso("Exclusão realizada com sucesso");
                    },
                    error: () => {
                      this.dialogs.erro("Falha ao realizar a exclusão")
                    }
                });
              }
          }
      });
  }

  public onToolbarCustomButtonsClick(btnKey: string){
    this.onToolbarButtonClickEv.emit({key: btnKey})
  }

  public onTabClose(e){
    this.tabs.splice(e.index - 1, 1);
  }

  public onNovoClick(){
    if(!this.mdNovoVisivel){
      this.mdNovoVisivel = true;
    }
  }

  public onExportarExcel() {
    import("xlsx").then(xlsx => {
      const worksheet = xlsx.utils.json_to_sheet(this.dataSource);
      const workbook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
      const excelBuffer: any = xlsx.write(workbook, { bookType: 'xlsx', type: 'array' });

      this.saveAsExcelFile(excelBuffer, "exportar");
    });
  }

  public onToogleFiltros() {
    this.filtroVisivel = !this.filtroVisivel;
  }

  onConfiguracoesSubmit(){
    this.lazyLoadingPageInfo.numberOfRows = this.configuracoesFormGroup.get("qtdLinhasNaPagina").value;
    this.refresh();

    this.persistenceService.setObject(this.storageKey + "-gridSettings", {
      numberOfRows: this.lazyLoadingPageInfo.numberOfRows
    });
  }

  onLazyLoadRecords(e: LazyLoadEvent){
    if(!this.lazyLoadRecords){
      return;
    };

    this.lazyLoadingPageInfo.firstRow = e.first;
    this.lazyLoadingPageInfo.numberOfRows = e.rows;

    let sortByList: AtmGerenciadorLazyLoadingPageSortByInfo[] = [];

    if(e.multiSortMeta != null){
      e.multiSortMeta.forEach(value => {
        if(value.order == -1)
          sortByList.push(new AtmGerenciadorLazyLoadingPageSortByInfo("a", value.field));
        else
          sortByList.push(new AtmGerenciadorLazyLoadingPageSortByInfo("d", value.field));
      });
    };

    this.lazyLoadingPageInfo.sortByList = sortByList;

    this.onLazyLoadEv.emit();    
  }

  onConfigClick(){
    this.configuracoesFormGroup.get("qtdLinhasNaPagina").setValue(this.lazyLoadingPageInfo.numberOfRows);
    this.mdConfiguracoes.show();
  }

  public exportarPDF() {
    import("jspdf").then(jsPDF => {
      import("jspdf-autotable").then(x => {
        const doc = new jsPDF.default(0, 0);
        doc.autoTable([], this.dataSource);
        doc.save('primengTable.pdf');
      })
    })
  };

  public saveAsExcelFile(buffer: any, fileName: string): void {
    import("file-saver").then(FileSaver => {
      let EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
      let EXCEL_EXTENSION = '.xlsx';
      const data: Blob = new Blob([buffer], {
        type: EXCEL_TYPE
      });
      FileSaver.saveAs(data, fileName + '_export_' + new Date().getTime() + EXCEL_EXTENSION);
    });
  };

  refresh() {
    this.onRefresh.emit();
  };

  AddTab(titulo: string, component: Type<unknown>): void {
    this.novaTabProps(titulo,  this.componentFactoryResolver.resolveComponentFactory(component))
  }

  public novaTabProps(titulo: string, componentFactory: any = null): IAtmPropriedadesComponent{
    
    if(this.componentFactory == undefined && componentFactory == null){
      return;
    }
    
    this.tabs.push({
      header: titulo
    });

    this.cdRef.detectChanges();

    // Alterar indice 2 vezes para corrigir bug na tabView que atrapalha selecionar a última tab.
    this.tabView.activeIndex = this.tabView.tabs.length - 1;
    this.tabView.activeIndex = this.tabView.tabs.length - 1;

    const viewContainerRef = this.tabsHost.last.viewContainerRef;
    viewContainerRef.clear();
   
    return viewContainerRef.createComponent<IAtmPropriedadesComponent>(componentFactory == null? this.componentFactory: componentFactory).instance;
  }

  public getRowDataFieldValue(rowData, fieldName): any{
    return getObjectPropertyValue(rowData, fieldName);
  }
}
