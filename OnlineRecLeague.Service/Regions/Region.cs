namespace OnlineRecLeague.Regions
{
	public interface IRegion
	{
		string RegionId { get; }
		string Name { get; }
	}

	public class Region : IRegion
	{
		public string RegionId { get; set; }
		public string Name { get; set; }
	}
}
