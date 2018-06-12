namespace Core.Interfaces
{
	public interface ITicker
	{
		string Symbol { get; set; }

		string Security { get; set; }
		string Sector { get; set; }
		string SubIndustry { get; set; }

	}
}
