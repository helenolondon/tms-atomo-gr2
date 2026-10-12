export class AtmGerenciadorLazyLoadingPageSortByInfo extends Object{
    order: string;
    fieldName: string;
    
    constructor(order: string, fieldName: string ){
        super();
        
        this.order = order;
        this.fieldName = fieldName;
    }

    public getString(){
        return  this.order + "|" + this.fieldName
    }
}