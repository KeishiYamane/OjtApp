using ControlLayer;
using ModelLayer;
using System.ComponentModel.Design.Serialization;

namespace ViewLayer
{
	public partial class BaseForm : Form
	{
		private readonly Model _model;
		private readonly Controller _controller;

		public BaseForm (Model model)
		{
			InitializeComponent();
			this._model = model;
			_controller = new Controller(model);
		}
	}
}
