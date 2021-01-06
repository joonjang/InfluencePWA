import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService, ApiResult } from '../base.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PrincipleService
    extends BaseService {
    constructor(
        http: HttpClient,
        @Inject('BASE_URL') baseUrl: string) {
        super(http, baseUrl);
    }

    getData<ApiResult>(
        pageIndex: number,
        pageSize: number,
        sortColumn: string,
        sortOrder: string,
        filterColumn: string,
        filterQuery: string
    ): Observable<ApiResult> {
        var url = this.baseUrl + 'api/Principles';
        var params = new HttpParams()
            .set("pageIndex", pageIndex.toString())
            .set("pageSize", pageSize.toString())
            .set("sortColumn", sortColumn)
            .set("sortOrder", sortOrder);

        if (filterQuery) {
            params = params
                .set("filterColumn", filterColumn)
                .set("filterQuery", filterQuery);
        }

        return this.http.get<ApiResult>(url, { params });
    }

  get<Principle>(id): Observable<Principle> {
        var url = this.baseUrl + "api/Principles/" + id;
    return this.http.get<Principle>(url);
    }

  put<Principle>(item): Observable<Principle> {
      var url = this.baseUrl + "api/Principles/" + item.id;
    return this.http.put<Principle>(url, item);
    }

  post<Principle>(item): Observable<Principle> {
      var url = this.baseUrl + "api/Principles";
    return this.http.post<Principle>(url, item);
  }

  getPrincipleType<PrincipleType>(id): Observable<PrincipleType> {
    var url = this.baseUrl + "api/PrincipleTypes/" + id;
    return this.http.get<PrincipleType>(url);
  }

  getPrincipleTypes<ApiResult>(
        pageIndex: number,
        pageSize: number,
        sortColumn: string,
        sortOrder: string,
        filterColumn: string,
        filterQuery: string
    ): Observable<ApiResult> {
      var url = this.baseUrl + 'api/PrincipleTypes';
        var params = new HttpParams()
            .set("pageIndex", pageIndex.toString())
            .set("pageSize", pageSize.toString())
            .set("sortColumn", sortColumn)
            .set("sortOrder", sortOrder);

        if (filterQuery) {
            params = params
                .set("filterColumn", filterColumn)
                .set("filterQuery", filterQuery);
        }

        return this.http.get<ApiResult>(url, { params });
    }

    isDupePrinciple(item): Observable<boolean> {
      var url = this.baseUrl + "api/Principles/IsDupePrinciple";
        return this.http.post<boolean>(url, item);
    }
}
