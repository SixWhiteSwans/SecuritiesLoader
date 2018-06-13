export class Ticker{

  constructor(){}
  public Source:string;
  public Symbol:string;
  public Securtity:string;
  public Sector:string;
  public SubIndustry:string;

}


export class TimeSeriesDataPoint{

  public Source:string;
  public Symbol:string;
  public Date:Date;
  public Close:Number;

  constructor(){}

}

export class SelectorItem {
  label: string;
  value: any;
}
