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
			// モデルのイベントを購読
			_model.Calculated += CalculateResult;
			_controller = new Controller(model);
		}

		private void button1_Click (object sender, EventArgs e)
		{
			// 入力値の取得
			var numeric1 = _numeric1.Value;
			var numeric2 = _numeric2.Value;

			// 計算
			// var result = numeric1 + numeric2;
			// var result = Add(numeric1,numeric2);
			// var result = _controller.Add(numeric1, numeric2);
			_controller.Add(numeric1, numeric2);

			// 表示
			// _resultLabel.Text = result.ToString();
		}

		private void CalculateResult (object? sender, decimal e)
		{
			// 表示
			_resultLabel.Text = e.ToString();
		}

		// ビューに直接書くのは避ける
		private decimal Add (Decimal a, Decimal b)
		{
			return a + b;
		}
	}
}
