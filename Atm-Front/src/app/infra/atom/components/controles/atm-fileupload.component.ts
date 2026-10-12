import { HttpHeaders } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FileUpload } from 'primeng-lts/components/fileupload/fileupload';

@Component({
  selector: 'atm-file-upload',
  templateUrl: './atm-fileupload.component.html',
  styleUrls: ['./atm-fileupload.component.scss']
})
export class AtmFileUploadComponent {
  @ViewChild('fileUpload', {static: false}) fileUpload: any;
  @Input('post-url') postUrl: string = "";
  @Input('tag') tag: string = "";
  @Input('reference') referenceKey: string = "";
  @Input('multiple') multiple: boolean = true;
  @Input('choose-label') chooseLabel: string = 'Carregar arquivo';
  @Input('max-file-size') maxFileSize: number = 52428800;

  @Output("onUpload") onUploadEV: EventEmitter<any> = new EventEmitter();
  @Output("onError") onErrorEV: EventEmitter<any> = new EventEmitter();

  headers: HttpHeaders = this.createHeaders();

  createHeaders(){
    let headers: HttpHeaders = new HttpHeaders();

    if(this.tag != null && this.tag != undefined){
      headers = headers.append("tag", this.tag.toString());
    }
    
    if(this.referenceKey != null && this.referenceKey != undefined){
      headers = headers.append("reference", this.referenceKey.toString());
    }

    return headers;
  }

  _onUpload(e){
    
    if(this.onUploadEV != null){
      this.onUploadEV.emit();
    }

    this.fileUpload.clear();
  }
  _onError(e){
    if(this.onErrorEV != null){
      this.onErrorEV.emit(e);
    };
  }

  ngOnChanges(){
    this.headers = this.createHeaders()
  }
}
