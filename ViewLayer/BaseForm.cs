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
			// ���f���̃C�x���g���w��
			_model.Calculated += CalculateResult;
			_controller = new Controller(model);
		}

		private void button1_Click (object sender, EventArgs e)
		{
			// ���͒l�̎擾
			var numeric1 = _numeric1.Value;
			var numeric2 = _numeric2.Value;

			// �v�Z
			// var result = numeric1 + numeric2;
			// var result = Add(numeric1,numeric2);
			// var result = _controller.Add(numeric1, numeric2);
			_controller.Add(numeric1, numeric2);

			// �\��
			// _resultLabel.Text = result.ToString();
		}

		private void CalculateResult (object? sender, decimal e)
		{
			// �\��
			_resultLabel.Text = e.ToString();
		}

		// �r���[�ɒ��ڏ����͔̂�����
		private decimal Add (Decimal a, Decimal b)
		{
			return a + b;
		}
	}
}
