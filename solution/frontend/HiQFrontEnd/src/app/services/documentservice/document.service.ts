import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { IUploadDocumentResponse } from 'src/app/data/response/IUploadDocumentResponse';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {
  private baseUrl = 'https://localhost:7244';

  constructor(private httpClient: HttpClient) { }

  uploadFile(file: File): Observable<IUploadDocumentResponse>
  {
    const formData = new FormData();
    formData.append('file', file);
    
    return this.httpClient.post<IUploadDocumentResponse>(`${this.baseUrl}/upload`, formData).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );    
  }
}
