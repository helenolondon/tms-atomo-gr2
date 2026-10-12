export function dateFromString_dd_mm_yyyy__hh_mm(dateStr: string){
    if(dateStr.length != 19){
        throw "Data inválida " + dateStr;
    };

    let dateParts = dateStr.substring(0, 10).split("/");
    let newDateStr = dateParts[2] + "-" + dateParts[1] + "-" + dateParts[0] + " " + dateStr.substring(11, 16);

    return new Date(newDateStr);
}