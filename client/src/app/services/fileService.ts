import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders } from '@angular/common/http';
import { FileData } from '../models/fileData';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root',
  })
export class FileService{
    apiUrl = "https://localhost:5001/";
    constructor(private http: HttpClient){

    }
    private getHeaders(){
        let headers = new HttpHeaders();
        if (localStorage.getItem("token")){
            headers = new HttpHeaders({"Authorization":"Bearer "+localStorage.getItem("token")});
        }
        
        headers.append('Access-Control-Allow-Headers', 'Content-Type');
        headers.append('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE');
        headers.append('Access-Control-Allow-Origin', '*');
        return headers;
    }
    getAllFilesData():Observable<FileData[]>{
        return this.http.get<FileData[]>(this.apiUrl+"api/file", {headers:this.getHeaders()});
    }

    deleteFile(id){
        return this.http.delete(this.apiUrl+"api/file/"+id,{headers:this.getHeaders()});
    }

    download(id: string){
        
        return this.http.get(this.apiUrl+"api/file/"+id, {headers:this.getHeaders(), responseType: "blob"});
    }
    upload(formData){
       
        return this.http.post<any>(this.apiUrl+"api/file", formData, {  
            headers:this.getHeaders(),
            reportProgress: true,  
            observe: 'events'  });
    }
}