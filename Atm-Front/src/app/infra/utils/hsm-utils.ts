
/**
 * This function retrieves the value of a property inside an object, no matter how deep it is.
*/
export function getObjectPropertyValue(object, fullPropertyName): any{
    
    let propertyNames: Array<string> = fullPropertyName.split('.');
    let objectPropertyValue: any = object;

    for(let i = 0; i < propertyNames.length; i++){
      let property = propertyNames[i];
      
      if((property in objectPropertyValue)){
        objectPropertyValue = objectPropertyValue[property];
      } else {
        return null;
      }
    };

    return objectPropertyValue;
  }