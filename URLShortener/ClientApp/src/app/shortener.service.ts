import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ShortenerService {

  constructor(private http:HttpClient) { }

  shorten(url: string) {
    return this.http.post("/api/shorten", null, { params: new HttpParams().set('url', url) });
  }
}
