import { AtmGridColumnTextAlign } from "./atm-grid-column-text-align";
import { AtmGridColumnType } from "./atm-grid-column-type";

export class AtmGridColumn{
    public header: string;
    public field: string; 
    public type: AtmGridColumnType;
    public textAlign?: AtmGridColumnTextAlign;
}