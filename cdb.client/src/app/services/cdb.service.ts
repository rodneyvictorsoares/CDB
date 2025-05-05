import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CDBCalculoRequest } from '../models/cdb-calculo-request';
import { CDBCalculoResponse } from '../models/cdb-calculo-response';
import { environment } from '../../enviroments/environment';

@Injectable({ providedIn: 'root' })
export class CdbService {

  private readonly api = 'https://localhost:5133/api/CDB';

  constructor(private http: HttpClient) { }

  calcular(body: CDBCalculoRequest): Observable<CDBCalculoResponse> {
    return this.http.post<CDBCalculoResponse>(`${this.api}/calcular`, body);
  }
}
