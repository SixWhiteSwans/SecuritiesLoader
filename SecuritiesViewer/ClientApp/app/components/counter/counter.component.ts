import { Component, OnInit } from '@angular/core';
import { SecuritiesService } from "./../services/securities.service";
import { SelectorItem, Ticker, TimeSeriesDataPoint } from "./../model/data-model";

@Component({
    selector: 'counter',
    templateUrl: './counter.component.html'
})
export class CounterComponent {
	public showData = true;
	public tickers: Array<Ticker>; 
	public selectorList = new Array<SelectorItem>();
	public timeSeriesDataPoint: Array<TimeSeriesDataPoint>;
	public selectorPlaceHolder = "Tickers";

	constructor(private securitiesService: SecuritiesService) {
		this.tickers = new Array<Ticker>();
		
	}

	ngOnInit() {

		this.securitiesService.getTickers().subscribe((tickerdata) => {

			let ticks = tickerdata.json();

			var tickers = ticks.map((data: any) => {
					let ticker = new Ticker();
					ticker.Symbol = data.Symbol;
					ticker.SubIndustry = data.SubIndustry;
					ticker.Securtity = data.Securtity;
					ticker.Source = data.Source;

					return ticker;

				});
	
			this.tickers = [];
			this.tickers = tickers;
			console.log(tickers);

		}, error => {
			console.log(error);
		}, () => {

			this.selectorList = this.tickers.map(t => {
				var selector = new SelectorItem();
				selector.label = t.Symbol;
				selector.value = t.Symbol;
				return selector;

			});


		});

	}

}
