
using System.ComponentModel;

namespace ViewLayer.Components
{
	public partial class RoundedToggleButton : RoundedButtonControl
	{
		public RoundedToggleButton()
		{
			InitializeComponent();
		}

		private Color onBackColor = Color.FromArgb(50, 230, 80);  // 緑色 (ON:活性化)
		private Color offBackColor = Color.FromArgb(189, 189, 189); // 灰色 (OFF)

		private bool isChecked = false;
		/// <summary>
		/// チェック状態を取得または設定します
		/// </summary>
		/// 		[Browsable(true)]
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

				if (isChecked)
				{
					// 選択状態の場合、背景色をonBackColorに設定
					BackgroundColor = onBackColor;
					Text = onText;
				}
				else
				{
					// 未選択状態の場合、背景色を元のoffBackColorに設定
					BackgroundColor = offBackColor;
					Text = offText;
				}
			}
		}

		private string onText = "有効"; // ON時のテキスト
		/// <summary>
		/// ON状態の時に表示されるテキストを取得または設定します
		/// </summary>
		[Category("Appearance")]
		[Description("ON状態の時に表示されるテキストを設定します")]
		[DefaultValue("有効")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string OnText
		{
			get { return onText; }
			set
			{
				onText = value ?? string.Empty; // nullの場合は空文字列に変換
				Invalidate(); // 画面を再描画
			}
		}

		private string offText = "無効"; // OFF時のテキスト

		/// <summary>
		/// OFF状態の時に表示されるテキストを取得または設定します
		/// </summary>
		[Category("Appearance")]
		[Description("OFF状態の時に表示されるテキストを設定します")]
		[DefaultValue("無効")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string OffText
		{
			get { return offText; }
			set
			{
				offText = value ?? string.Empty; // nullの場合は空文字列に変換
				Invalidate(); // 画面を再描画
			}
		}

		private void RoundedToggleButton_Click(object sender, EventArgs e)
		{
			if (Enabled) // Enabledの場合のみ処理を実行
			{
				Checked = !Checked; // 状態を反転
			}
		}
	}
}
