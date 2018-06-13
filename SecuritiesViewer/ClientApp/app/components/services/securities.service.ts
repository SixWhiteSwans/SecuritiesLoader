import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
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

  constructor(private httpClient:HttpClient) { }

  getUnAuthorizedWithBaseUrl(url:string):Observable<any>{

    var uri =this.BASE_URL+url;

    console.log('http uri is: ' + uri);
    return this.httpClient.get(uri);

  }

  getTickers(): Observable<Array<Ticker>>{

    let url=  this.controller+'tickers';
    console.log('http url is: ' + url);
    return this.getUnAuthorizedWithBaseUrl(url).map((tsdpdata:any)=>{

      var tds = tsdpdata.map(data=>{
        let ticker = new Ticker();
        ticker.Symbol = data.Symbol;
        ticker.SubIndustry = data.SubIndustry;
        ticker.Securtity = data.Securtity;
        ticker.Source = data.Source;

        return ticker;

      });

      return tds;

    });
  }

  getSecuritiesTimeSeriesDataPoints(startDate:string,endDate:string,symbol:string,sector:string,subIndustry:string,source:string,orderbyField:string,orderBy:string): Observable<Array<TimeSeriesDataPoint>>{


    //string start_date, string end_date, sector=null,  subIndustry = null,  source = null,  order=null,  orderField=null

    //&symbol=${symbol}

    var query = `start_date=${startDate}&end_date=${endDate}&sector=${sector}&subIndustry=${subIndustry}&source=${source}&orderBy=${orderBy}&orderbyField=${orderbyField}`;

    let url=  this.controller+'securitydatapoints?'+ query;
    console.log('http url is: ' + url);
    return this.getUnAuthorizedWithBaseUrl(url).map((tsdpdata:any)=>{

      var tsdps = tsdpdata.map(data=>{

        let tsdp = new TimeSeriesDataPoint();
        tsdp.Symbol = data.Symbol;
        tsdp.Source = data.Source;
        tsdp.Date  = data.Date;
        tsdp.Close = data.Close;

        return tsdp;

      });

      return tsdps;

    });

  }

}
