using System.ComponentModel;

namespace ViewLayer.Components
{
	public partial class RoundedToggleButtonEx : UserControl
	{
		public RoundedToggleButtonEx ()
		{
			InitializeComponent();
			Size = new Size(100, 40);
			this.DoubleBuffered = true;// チラつきを防ぐ
			this.Click += ToggleButton_Click;
		}

		private Color onBackColor = Color.FromArgb(50, 230, 80);  // 緑色 (ON:活性化)
		private Color offBackColor = Color.FromArgb(189, 189, 189); // 灰色 (OFF)
		private Color buttonColor = Color.White;
		
		private bool isChecked = false;
		/// <summary>
		/// チェック状態を取得または設定します
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Checked
		{
			get { return isChecked; }
			set
			{
				isChecked = value;
				Invalidate(); // 画面を再描画（OnPaintを呼び出す）
							  // CheckedChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		private string onText = "する"; // ON時のテキスト
		/// <summary>
		/// ON状態の時に表示されるテキストを取得または設定します
		/// </summary>
		[Category("Appearance")]
		[Description("ON状態の時に表示されるテキストを設定します")]
		[DefaultValue("する")]
		public string OnText
		{
			get { return onText; }
			set
			{
				onText = value ?? string.Empty; // nullの場合は空文字列に変換
				Invalidate(); // 画面を再描画
			}
		}

		private string offText = "しない"; // OFF時のテキスト
		/// <summary>
		/// OFF状態の時に表示されるテキストを取得または設定します
		/// </summary>
		[Category("Appearance")]
		[Description("OFF状態の時に表示されるテキストを設定します")]
		[DefaultValue("しない")]
		public string OffText
		{
			get { return offText; }
			set
			{
				offText = value ?? string.Empty; // nullの場合は空文字列に変換
				Invalidate(); // 画面を再描画
			}
		}

		private void ToggleButton_Click (object sender, EventArgs e)
		{
			if (Enabled) // Enabledの場合のみ処理を実行
			{
				Checked = !Checked; // 状態を反転
			}
		}

		protected override void OnPaint (PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			// 背景色の設定
			Color backColor = (Enabled, isChecked) switch
			{
				(true, true) => onBackColor,
				(true, false) => offBackColor,
				(false, false) => offBackColor
			};

			// 背景の角丸矩形を描画
			using (SolidBrush brush = new SolidBrush(backColor))
			{
				g.FillRoundedRectangle(brush, new Rectangle(0, 0, Width, Height), Height / 2);
			}

			// トグルボタン
			int buttonX = isChecked ? Width - Height : 0;
			int padding = 10; // 余白を追加(背景の境界線とのくっつき防止)
			using (SolidBrush brush = new SolidBrush(buttonColor))
			{
				// スイッチ（丸）を描画
				g.FillEllipse(brush, new Rectangle(buttonX + padding / 2, padding / 2, Height - padding, Height - padding));
			}

			// 文字の描画
			string text = isChecked ? onText : offText;

			// テキストが空文字列でない場合のみ描画
			if (!string.IsNullOrEmpty(text))
			{
				using (Font font = new Font("Meiryo", 9, FontStyle.Bold))
				using (SolidBrush textBrush = new SolidBrush(Color.Black))
				{
					SizeF textSize = g.MeasureString(text, font);
					float margin = 15; // 余白
					float textX = isChecked ? margin : (Width - textSize.Width - margin); // ONなら左寄せ（margin分の余白）、OFFなら右寄せ（Width - 文字幅 - margin）
					float textY = (Height - textSize.Height) / 2;
					g.DrawString(text, font, textBrush, textX, textY); // ボタンの中央に文字を描画
				}
			}
		}
	}
	public static class GraphicsExtensions
	{
		public static void FillRoundedRectangle (this Graphics g, Brush brush, Rectangle rect, int radius)
		{
			using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
			{
				path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
				path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
				path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
				path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
				path.CloseFigure();
				g.FillPath(brush, path);
			}
		}
	}
}
