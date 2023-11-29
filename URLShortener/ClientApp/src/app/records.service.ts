import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RecordsService {

  constructor(private http: HttpClient) { }
  getMultiple(count: number) {
    return this.http.get("/api/records", { params: new HttpParams().set("count", count) });
  }
}
