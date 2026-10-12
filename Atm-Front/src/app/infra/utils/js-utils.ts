import * as _ from 'lodash';

export function deepCloneObject(sourceObject){
    return _.cloneDeep(sourceObject); 
}

export function loadFileDialog(onLoadCallBack: (files: FileList) => void, mimeType: string = null, allowMultiple: boolean = false) {
    let input = document.createElement('input');
    input.type = 'file';
    input.multiple = allowMultiple;

    if(mimeType != null){
        input.accept = mimeType
    };

    input.onchange = () => {
        onLoadCallBack(input.files);
    };

    input.click();  
}

export function replaceStringChars(stringSource: string, oldChars: string[], newChars: string[]){
    
    oldChars.forEach((char, index) => {
        let replaceChar = newChars != null? newChars[index]: "";

        while(stringSource.indexOf(char) != -1){
            stringSource = stringSource.replace(char, replaceChar);
        };
    });
    
    return stringSource;
}