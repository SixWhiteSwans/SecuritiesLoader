import { Component, Inject } from '@angular/core';
import { SecuritiesService } from "./../services/securities.service";
import { SelectorItem, Ticker, TimeSeriesDataPoint } from "./../model/data-model";

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {


	public showData = true;
	public tickers: Array<Ticker>;
	public selectorList = new Array<SelectorItem>();
	public timeSeriesDataPoint: Array<TimeSeriesDataPoint>;
	public selectorPlaceHolder = "Tickers";


	constructor(private securitiesService: SecuritiesService) {
		this.tickers = new Array<Ticker>();
		this.timeSeriesDataPoint = new Array<TimeSeriesDataPoint>();
	}


	submit(): void {

		this.showData = false;
		let startDate = "2018-01-01";
		let endDate = "2018-02-01";

		let symbol: string ='' ;
		let sector = "Health Care";
		let subIndustry: string = '';
		let source: string = '';
		let orderbyField: string = '';
		let orderBy: string = '';

		this.securitiesService.getSecuritiesTimeSeriesDataPoints(startDate, endDate, symbol, sector, subIndustry, source, orderbyField, orderBy).subscribe(
			(tsdpsData:any) => {

				let tsdpdata = tsdpsData.json();
				
				var tsdps = tsdpdata.map((data: any) => {

					let tsdp = new TimeSeriesDataPoint();
					tsdp.Symbol = data.Symbol;
					tsdp.Source = data.Source;
					tsdp.Date = data.Date;
					tsdp.Close = data.Close;

					return tsdp;

				});

				

				



			this.timeSeriesDataPoint = [];
			this.timeSeriesDataPoint = tsdps;

			console.log(this.timeSeriesDataPoint);

		}, error => {
			console.log(error);
		}, () => {

		});

	}

}


