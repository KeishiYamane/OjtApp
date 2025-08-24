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

		private void button1_Click (object sender, EventArgs e)
		{
			// “ü—Í’l‚ÌŽæ“¾
			var numeric1 = _numeric1.Value;
			var numeric2 = _numeric2.Value;

			// ŒvŽZ
			var result = numeric1 + numeric2;

			// •\Ž¦
			_resultLabel.Text = result.ToString();
		}
	}
}
