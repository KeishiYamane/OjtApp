using ModelLayer;

namespace ControlLayer
{
	public class Controller
	{
		private Model _model;

		public Controller (Model model)
		{
			_model = model;
		}

		//public decimal Add (decimal numeric1, decimal numeric2)
		//{
		//	return _model.Add(numeric1, numeric2);
		//}

		public void Add (decimal numeric1, decimal numeric2)
		{
			_model.Add(numeric1, numeric2);
		}
	}
}
