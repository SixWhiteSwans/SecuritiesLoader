using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.ServiceModel;
using Core.Model;

namespace Core.Interfaces.Services
{
	[ServiceContract]
	public interface ISecuritiesService
	{

		[DataMember]
		string SourceList { get; }

		[OperationContract]
		Task<IEnumerable<Ticker>> GetTickers();

		[OperationContract]
		Task<IEnumerable<DataPoint>> GetSecuritiesTimeSeries(string symbol, DateTime startDate, DateTime endDate,string order,string source);

		[OperationContract]
		Task<IEnumerable<DataPoint>> GetMultiSecuritiesTimeSeries(SecurityQuery securityQuery, DateTime startDate,
			DateTime endDate);

		[OperationContract]
		Task<IEnumerable<DataPoint>> ProcessSourceHierarchy(IEnumerable<DataPoint> timeSeriesDataPoints,
			Queue<string> sourceHierarchyQueue);

	}
}
