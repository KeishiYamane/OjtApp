
namespace ModelLayer
{
	public class Model
	{
		public EventHandler<decimal> Calculated = delegate { };

		//public decimal Add (decimal numeric1, decimal numeric2)
		//{
		//	return numeric1 + numeric2;
		//}
		public void Add (decimal numeric1, decimal numeric2)
		{
			var result = numeric1 + numeric2;
			Calculated(this, result);
		}
	}
}
