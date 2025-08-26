using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ViewLayer.Components
{
	/// <summary>
	/// 角丸のラジオボタンコントロールクラス
	/// </summary>
	/// <remarks>
	/// RoundedButtonControlを継承し、ラジオボタンの機能を提供します。
	/// 同一親コントロール内で排他選択が可能です。
	/// 選択時の背景色をデザイナーから設定できます。
	/// </remarks>
	[DefaultEvent("Click")]
	public partial class RoundedRadioButton : RoundedButtonControl
	{
		private bool isChecked = false; // デフォルト値：false
		/// <summary>
		/// ラジオボタンの選択状態を取得または設定します。
		/// </summary>
		[Browsable(true)]
		[Category("Behavior")]
		[Description("ラジオボタンの選択状態を設定します。")]
		[DefaultValue(false)]
		public bool Checked
		{
			get { return isChecked; }
			set
			{
				if (isChecked != value)
				{
					isChecked = value;
					Invalidate(); // 再描画
				}
			}
		}

		private Color selectedColor = Color.FromArgb(37, 99, 235); // デフォルト値：RGB(37, 99, 235)
		/// <summary>
		/// ラジオボタンが選択された時の背景色を取得または設定します。
		/// </summary>
		[Browsable(true)]
		[Category("Appearance")]
		[Description("ラジオボタンが選択された時の背景色を設定します。")]
		[DefaultValue(typeof(Color), "37, 99, 235")]
		public Color SelectedColor
		{
			get { return selectedColor; }
			set
			{
				selectedColor = value;
				//if (isChecked)
				//{
				//	Invalidate(); // 選択状態の場合は再描画
				//}
			}
		}

		/// <summary>
		/// RoundedRadioButtonクラスの新しいインスタンスを初期化します。
		/// </summary>
		public RoundedRadioButton ()
		{
			InitializeComponent();
		}

		/// <summary>
		/// クリックイベントを処理し、ラジオボタンの排他選択機能を実装します。
		/// </summary>
		/// <param name="e">イベント引数</param>
		/// <remarks>
		/// 未選択状態の場合、同一親コントロール内の他のRoundedRadioButtonを
		/// 未選択状態にしてから、自身を選択状態にします。
		/// </remarks>
		protected override void OnClick (EventArgs e)
		{
			base.OnClick(e);

			if (!Checked)
			{
				foreach (var control in Parent.Controls)
				{
					if (control == this)
					{
						continue;
					}

					if (control is RoundedRadioButton radioButton)
					{
						radioButton.Checked = false;
					}
				}
				Checked = true;
			}
		}

		/// <summary>
		/// コントロールの描画処理を行います。
		/// </summary>
		/// <param name="e">描画イベントの引数</param>
		/// <remarks>
		/// 選択状態に応じて背景色を変更して描画します。
		/// 選択時はSelectedColorプロパティ、未選択時はBackgroundColorプロパティを使用します。
		/// </remarks>
		protected override void OnPaint (PaintEventArgs e)
		{

			// 基底クラスの描画処理を実行
			base.OnPaint(e);

			// 選択状態に応じて背景色を一時的に変更
			Color originalBackgroundColor = BackgroundColor;
			if (isChecked)
			{
				BackgroundColor = selectedColor;
			}
		}
	}
}
