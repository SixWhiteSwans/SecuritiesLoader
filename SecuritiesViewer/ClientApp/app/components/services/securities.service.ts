import { Injectable } from '@angular/core';
//import { HttpClient } from "@angular/common/http";
import { Http } from "@angular/http";
import {Observable} from "rxjs/Observable";
import {Ticker, TimeSeriesDataPoint} from "../model/data-model";

import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/observable/throw';
import 'rxjs/observable/from';


@Injectable()
export class SecuritiesService {

  private controller='api/timeseries/';
  private BASE_URL = 'http://localhost:8080/';

	//private httpClient:HttpClient
  constructor(private http:Http) { }

  getUnAuthorizedWithBaseUrl(url:string):Observable<any>{

    var uri =this.BASE_URL+url;

	  console.log('http uri is: ' + uri);

	  //httpClient is a new version of angular
    return this.http.get(uri);

  }

  getTickers(): Observable<any>{

    let url=  this.controller+'tickers';
    console.log('http url is: ' + url);
	  return this.getUnAuthorizedWithBaseUrl(url);
  }

  getSecuritiesTimeSeriesDataPoints(startDate:string,endDate:string,symbol:string,sector:string,subIndustry:string,source:string,orderbyField:string,orderBy:string): Observable<any>{

    //string start_date, string end_date, sector=null,  subIndustry = null,  source = null,  order=null,  orderField=null
    //&symbol=${symbol}

	  var query = `start_date=${startDate}&end_date=${endDate}&symbol=${symbol}&sector=${sector}&subIndustry=${subIndustry}&source=${source}&orderBy=${orderBy}&orderbyField=${orderbyField}`;

    let url=  this.controller+'securitydatapoints?'+ query;
    console.log('http url is: ' + url);
	  return this.getUnAuthorizedWithBaseUrl(url);

  }

}
