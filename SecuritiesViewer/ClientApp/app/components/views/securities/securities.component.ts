import { Component, OnInit } from '@angular/core';
import { SecuritiesService } from "../../services/securities.service";
import {SelectorItem, Ticker, TimeSeriesDataPoint} from "../../model/data-model";

@Component({
  selector: 'app-securities',
  templateUrl: './securities.component.html',
  styleUrls: ['./securities.component.css']
})
export class SecuritiesComponent implements OnInit {

  public showData=true;
  public tickers: Array<Ticker> ;
  public selectorList = new Array<SelectorItem>();
  public timeSeriesDataPoint: Array<TimeSeriesDataPoint> ;
  public selectorPlaceHolder = "Tickers";


  constructor(private securitiesService:SecuritiesService) {
    this.tickers = new Array<Ticker>();
    this.timeSeriesDataPoint = new Array<TimeSeriesDataPoint>();
  }

  ngOnInit() {

    this.securitiesService.getTickers().subscribe((tickers:Array<Ticker>)=>{
      this.tickers=[];
      this.tickers=tickers;
      console.log(tickers);

    },error=>{
      console.log(error);
    },()=>{

      this.selectorList =   this.tickers.map(t=>{
        var selector = new SelectorItem();
        selector.label=t.Symbol;
        selector.value=t.Symbol;
        return selector;

      });


    });

  }

  submit():void{

    this.showData=false;
    let startDate = "2018-01-01";
    let endDate = "2018-06-01";

    let symbol:string=null;
    let sector="Health Care";
    let subIndustry:string=null;
    let source:string=null;
    let orderbyField:string=null;
    let orderBy:string=null;

    this.securitiesService.getSecuritiesTimeSeriesDataPoints(startDate,endDate,symbol,sector,subIndustry,source,orderbyField,orderBy).subscribe((tsdps:Array<TimeSeriesDataPoint>)=>{
      this.timeSeriesDataPoint=[];
      this.timeSeriesDataPoint=tsdps;

      console.log(this.timeSeriesDataPoint);

    },error=>{
      console.log(error);
    },()=>{

    });

  }

  onUpdatedItem(event:Event){




  }


}
