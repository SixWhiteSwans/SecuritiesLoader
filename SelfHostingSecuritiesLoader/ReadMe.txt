The loader is designed around Entity frame work interacting with the sql server.

There are 4 data formats: csv/excel/json/xml. The oledb connection is used for excel and csv, and serialization for json,xml

As opposed to using sqlbulkcopy, ef bulk save is used. The business logic is extracted from the sql server db, and no store procs are written.

Tables: 
Tickers:			-securities data, ref data goldern source
TimeSeriesDataPoint - timeseries data for securities
DataPointErrorLog	- datapoints which are invalid wrt data validation
ProcessDataLog		- job monitoring


The schema in the datatables can be improved by indexing and clustered index for the timeseries. This will improve the speed of the query. In addition the tickers can align a collection of timeseries data,

data is validated based on:
-golder source data in the Ticker db

-data formatting : business day, NaN, securities which are not in goldern source
				also: outliers can be monitored, and further data scrubbing techniques can be applied.

The batch jobs are considered to be a bulk copy thus the raw timeseries data is deleted and re populated.
The ticker data if already existing in the table is updated as opposed to being deleted. 

Running this project will create the db and populate the source data.

