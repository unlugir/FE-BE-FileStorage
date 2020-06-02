import { Component, OnInit } from '@angular/core';
import { FileService } from '../services/fileService';
import { AccountService } from '../services/accountService';
import { FileData } from '../models/fileData';
import { map, catchError } from 'rxjs/operators';
import { HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { of } from 'rxjs';
import { AppComponent } from '../app.component';
import { NotifierService } from 'angular-notifier';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  fileDataList: FileData[];
  loading = true;
  fileIsUploading = false;

  constructor(private fileService: FileService, private accountService: AccountService,private notifier: NotifierService) {
    AppComponent.userLoggedIn = true;
    this.accountService.getCurrentUser().subscribe(res=>{AppComponent.user = res},err=>{console.log(err)});
    fileService.getAllFilesData().subscribe(res=>{
      this.loading = false;
      console.log(res.length);
      this.fileDataList = res.reverse();
    },err=>{
      this.notifier.notify("error","Oop! Cant load files list!");
    });
    
  }

  ngOnInit() {
  }
  pseudoClick(){
    document.getElementById("fileUploader").click();
  }
  
  download(id,name){
    console.log("Downloading "+id+" "+name);
    this.fileService.download(id).subscribe(x => {
      // It is necessary to create a new blob object with mime-type explicitly set
      // otherwise only Chrome works like it should
      let newBlob = new Blob([x], { type: "application/octet-stream" });
      console.log(newBlob.size);
      
      // IE doesn't allow using a blob object directly as link href
      // instead it is necessary to use msSaveOrOpenBlob
      if (window.navigator && window.navigator.msSaveOrOpenBlob) {
          window.navigator.msSaveOrOpenBlob(newBlob);
          return;
      };
      const data = window.URL.createObjectURL(newBlob);

            var link = document.createElement('a');
            link.href = data;
            link.download = name;
            // this is necessary as link.click() does not work on the latest firefox
            link.dispatchEvent(new MouseEvent('click', { bubbles: true, cancelable: true, view: window }));

            setTimeout(function () {
                // For Firefox it is necessary to delay revoking the ObjectURL
                window.URL.revokeObjectURL(data);
                link.remove();
            }, 100);
    })
  }
  updateFileList(){
    
    this.loading = true;
    setTimeout(()=>
    this.fileService.getAllFilesData().subscribe(res=>{
      this.loading = false;
      console.log(res.length);
      this.fileDataList = res.reverse();
    },err=>{
      this.notifier.notify("error","Oops! Cant update files list!");
    }),700);
  }
  deleteFile(id){
    console.log("Deleting file "+id);
    this.fileService.deleteFile(id).subscribe(res=>{
      this.notifier.notify("success","File deleted!");
      this.updateFileList();
    },err=>{
      this.notifier.notify("warning","Error on deleting!");
    });
  }

  settings(){
    this.notifier.notify("info","Setting unavailable!");
  }
  uploadFile(files) { 
    this.notifier.notify("warning","Uploading started!"); 
    this.fileIsUploading = true;
    let fileToUpload = files.item(0);
    if (fileToUpload == null || fileToUpload == undefined)
    {
      this.fileIsUploading = false;
      return;
    }
    let formData = new FormData(); 
    formData.append("file", fileToUpload, fileToUpload.name);   
    this.fileService.upload(formData).subscribe(res=>{
      console.log(res)
      if(res["type"]==4){
      this.updateFileList();
      this.fileIsUploading = false;
      this.notifier.notify("success","File uploaded!");
      }
    },err=>{
      console.log(err);
      this.notifier.notify("warning","Error on uploading!");
      this.fileIsUploading = false;
    });
  }
}
