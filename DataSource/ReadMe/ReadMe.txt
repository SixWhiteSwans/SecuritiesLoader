- The loaders, are built for a bcp, further adpation is required to extract of a specfic date
- We need to be able to rerun the file loads on demand to update the db.
- Logging and timings need to be applied to the services
- fault tolerance also needs to be built in


data clean considerations
-Na
-bad business days good business days
-outliers high and low
-reference data management
- exception reporting


THE PROCESS:
Securities data: is considered the goldern source. No time series data will be saved into the db which is not defined in the goldern source.
 -Thus the existing data source data will be deleted before the data is repopulated.


//Timeseries data: is built to be a complete rerun. 
- Thus the existing data source data will be deleted before the data is repopulated.
// work to do if there is a delta record update. i.e updating a specific data point which si off.
// all data consumed is considered to be handled from the source. No data will be touched unless sign off by an appropriate data owner.
//no data which is NaN will be saved to the database.

// all data errors - not validated data is saved to a data log table called "DataPointErrorLog"
