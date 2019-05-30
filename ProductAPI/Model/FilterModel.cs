namespace ProductAPI.Model
{
	/// <summary>
	/// Properties to filter on, so that we can version the api without having to worry about parameters
	/// </summary>
	public class FilterModel
	{
		public string Description { get; set; }
		public string Model { get; set; }
		public string Brand { get; set; }
		public int Limit { get; set; }
	}
}
