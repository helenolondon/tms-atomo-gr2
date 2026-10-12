import { AtmGerenciadorLazyLoadingPageSortByInfo } from "./atm-gerenciador-lazy-loading-page-sort-by-info";

export class AtmGerenciadorLazyLoadingPageInfo{
    firstRow: number;
    numberOfRows: number;
    sortByList?: AtmGerenciadorLazyLoadingPageSortByInfo[];

    constructor (firstRow: number, numberOfRows: number){
        this.firstRow = firstRow;
        this.numberOfRows = numberOfRows;
    };
    
    getSortByListString(): string {
        let sortListString = "";

        this.sortByList.forEach(value => {
            sortListString = sortListString + ";" + value.getString()
        });

        if(sortListString[0] == ";"){
            sortListString = sortListString.substring(1);
        };

        return sortListString;
    };
}