import { Component, EventEmitter, Input, Output } from '@angular/core';
import { getObjectPropertyValue } from 'src/app/infra/utils/hsm-utils';
import { AtmGridColumn } from './atm-grid-column';
import { AtmGridColumnTextAlign } from './atm-grid-column-text-align';
import { AtmGridColumnType } from './atm-grid-column-type';
import { AtmGridCustomButton } from './atm-grid-custom-button';

@Component({
  selector: 'atm-grid',
  templateUrl: './atm-grid.component.html',
  styleUrls: ['./atm-grid.component.scss']
})
export class AtmGridComponent {
  @Input('data-source') dataSource: Array<any> = [];
  @Input("columns") columns: AtmGridColumn[];
  @Input("show-excluir-button") showBtnExcluir = true;
  @Input("show-editar-button") showBtnEditar = true;
  @Input("left-custom-grid-buttons") leftGridCustomButtons: AtmGridCustomButton[] = [];
  @Input("right-custom-grid-buttons") rightGridCustomButtons: AtmGridCustomButton[] = [];

  @Output() onRefresh: EventEmitter<any> = new EventEmitter();
  @Output("onCustomButtonClick") onCustomButtonClickEv: EventEmitter<any> = new EventEmitter();
  @Output("onEditar") onEditarClickEv: EventEmitter<any> = new EventEmitter();
  @Output("onExcluir") onExcluirClickEv: EventEmitter<any> = new EventEmitter();

  public atmGridColumnType: AtmGridColumnType;

  getColumnType(column: AtmGridColumn): string{
    return column.type;
  }

  getTextAlign(col: AtmGridColumn){
    if(col.textAlign != undefined && col.textAlign != null){
      return col.textAlign;
    };

    return 'left';
  }

  getRowDataFieldValue(rowData, fieldName): any{
    return getObjectPropertyValue(rowData, fieldName);
  }

  onCustomButtonClick(rowData, key){
    this.onCustomButtonClickEv.emit({rowData, key})
  }

  onExcluirClick(e){
    this.onExcluirClickEv.emit(e)
  }
  
  onEditarClick(e){
    this.onEditarClickEv.emit(e);
  }
}
