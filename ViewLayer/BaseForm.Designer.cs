namespace ViewLayer
{
	partial class BaseForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			label1 = new Label();
			label2 = new Label();
			_resultLabel = new Label();
			button1 = new Button();
			_numeric1 = new NumericUpDown();
			_numeric2 = new NumericUpDown();
			((System.ComponentModel.ISupportInitialize)_numeric1).BeginInit();
			((System.ComponentModel.ISupportInitialize)_numeric2).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(118, 15);
			label1.Name = "label1";
			label1.Size = new Size(15, 15);
			label1.TabIndex = 2;
			label1.Text = "+";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(268, 15);
			label2.Name = "label2";
			label2.Size = new Size(15, 15);
			label2.TabIndex = 3;
			label2.Text = "=";
			// 
			// _resultLabel
			// 
			_resultLabel.AutoSize = true;
			_resultLabel.Location = new Point(310, 15);
			_resultLabel.Name = "_resultLabel";
			_resultLabel.Size = new Size(76, 15);
			_resultLabel.TabIndex = 4;
			_resultLabel.Text = "                       ";
			// 
			// button1
			// 
			button1.Location = new Point(410, 12);
			button1.Name = "button1";
			button1.Size = new Size(75, 23);
			button1.TabIndex = 5;
			button1.Text = "計算";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// _numeric1
			// 
			_numeric1.Location = new Point(12, 12);
			_numeric1.Name = "_numeric1";
			_numeric1.Size = new Size(100, 23);
			_numeric1.TabIndex = 6;
			// 
			// _numeric2
			// 
			_numeric2.Location = new Point(139, 12);
			_numeric2.Name = "_numeric2";
			_numeric2.Size = new Size(100, 23);
			_numeric2.TabIndex = 7;
			// 
			// BaseForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(_numeric2);
			Controls.Add(_numeric1);
			Controls.Add(button1);
			Controls.Add(_resultLabel);
			Controls.Add(label2);
			Controls.Add(label1);
			Name = "BaseForm";
			Text = "Form1";
			((System.ComponentModel.ISupportInitialize)_numeric1).EndInit();
			((System.ComponentModel.ISupportInitialize)_numeric2).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private Label label1;
		private Label label2;
		private Label _resultLabel;
		private Button button1;
		private NumericUpDown _numeric1;
		private NumericUpDown _numeric2;
	}
}
