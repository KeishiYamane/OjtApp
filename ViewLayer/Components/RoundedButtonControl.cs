using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace ViewLayer.Components
{
	[DefaultEvent("Click")]
	public partial class RoundedButtonControl : UserControl
	{
		private int cornerRadius = 8; // デフォルト値
		/// <summary>
		/// ボタンの角丸の半径を取得または設定します。
		/// </summary>
		[Browsable(true)]
		[Category("Appearance")]
		[Description("ボタンの角丸の半径を設定します。")]
		[DefaultValue(8)]
		public int CornerRadius
		{
			get { return cornerRadius; }
			set
			{
				if (value < 0) value = 0;
				cornerRadius = value;
				Invalidate(); // 再描画
			}
		}

		private Color backgroundColor = Color.FromArgb(189, 189, 189); // デフォルト背景色
		/// <summary>
		/// ボタンの背景色を取得または設定します。
		/// </summary>
		[Browsable(true)]
		[Category("Appearance")]
		[Description("ボタンの背景色を設定します。RGB値で指定可能です。")]
		[DefaultValue(typeof(Color), "189, 189, 189")]
		public Color BackgroundColor
		{
			get { return backgroundColor; }
			set
			{
				if (backgroundColor != value)
				{
					backgroundColor = value;
					Invalidate(); // 再描画
				}
			}
		}

		private string text = ""; // 効かない UserControlは作成時に自動的にbase.Textプロパティにコントロール名（roundedButtonControl1）を設定
		/// <summary>
		/// ボタンに表示するテキストを取得または設定します。
		/// </summary>
		[Browsable(true)]
		[Category("Appearance")]
		[Description("ボタンに表示するテキストを設定します。")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			get { return text; }
			set
			{
				text = value ?? string.Empty;
				base.Text = text; // ベースクラスのTextプロパティも更新
				Invalidate(); // 再描画
			}
		}

		private Font font = new Font("Yu Gothic UI", 9F, FontStyle.Regular); // デフォルトフォント
		/// <summary>
		/// ボタンのテキストに使用するフォントを取得または設定します。
		/// </summary>
		[Browsable(true)]
		[Category("Appearance")]
		[Description("ボタンのテキストに使用するフォントを設定します。")]
		public override Font Font
		{
			get { return font; }
			set
			{
				font = value ?? new Font("Yu Gothic UI", 9F, FontStyle.Regular);
				Invalidate(); // 再描画
			}
		}

		private Color foreColor = Color.Black; // デフォルト文字色
		/// <summary>
		/// ボタンのテキストの色を取得または設定します。
		/// </summary>
		[Browsable(true)]
		[Category("Appearance")]
		[Description("ボタンのテキストの色を設定します。")]
		[DefaultValue(typeof(Color), "Black")]
		public override Color ForeColor
		{
			get { return foreColor; }
			set
			{
				foreColor = value;
				Invalidate(); // 再描画
			}
		}



		private int gradationOffset = 40; // デフォルトオフセット値
		/// <summary>
		/// グラデーション終点の基準色からのオフセット値を取得または設定します。
		/// </summary>
		[Browsable(true)]
		[Category("RoundButtonBase")]
		[Description("グラデーション終点の基準色からのオフセット値を設定します。")]
		[DefaultValue(40)]
		public virtual int GradationOffset
		{
			get { return gradationOffset; }
			set
			{
				if (gradationOffset != value)
				{
					gradationOffset = value;
					Invalidate(); // 再描画
				}
			}
		}

		private Color shadowColor = Color.FromArgb(100, 0, 0, 0); // デフォルト影色（半透明の黒）
		/// <summary>
		/// ボタンの影の色を取得または設定します。
		/// </summary>
		[Browsable(true)]
		[Category("Appearance")]
		[Description("ボタンの影の色を設定します。アルファ値で透明度を調整できます。")]
		[DefaultValue(typeof(Color), "100, 0, 0, 0")]
		public Color ShadowColor
		{
			get { return shadowColor; }
			set
			{
				if (shadowColor != value)
				{
					shadowColor = value;
					Invalidate(); // 再描画
				}
			}
		}

		private int shadowSize = 3; // デフォルト影サイズ
		/// <summary>
		/// ボタンの影のサイズを取得または設定します。
		/// </summary>
		[Browsable(true)]
		[Category("Appearance")]
		[Description("ボタンの影のサイズを設定します。0で影なし。")]
		[DefaultValue(3)]
		public int ShadowSize
		{
			get { return shadowSize; }
			set
			{
				if (value < 0) value = 0;
				if (shadowSize != value)
				{
					shadowSize = value;
					Invalidate(); // 再描画
				}
			}
		}

		private int originalShadowSize; // マウスダウン時の元の影サイズを保持
		private bool isPressed = false; // ボタンが押されているかどうかのフラグ

		public RoundedButtonControl()
		{
			InitializeComponent();
			Size = new Size(100, 40);
			Cursor = Cursors.Hand; // 手の形のカーソルを設定
								   // 描画の最適化
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			BackColor = Color.Transparent;
		}

		/// <summary>
		/// コントロールの描画処理を行います。
		/// </summary>
		/// <param name="e">描画イベントの引数</param>
		/// <remarks>
		/// 影→枠線→内側の面→テキストの順で描画します。
		/// アンチエイリアシングを有効にして滑らかな描画を行います。
		/// </remarks>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

			// 影を描画
			if (shadowSize > 0)
			{
				Rectangle shadowRect = new Rectangle(
					shadowSize,
					shadowSize,
					ClientRectangle.Width - shadowSize,
					ClientRectangle.Height - shadowSize
				);

				using (GraphicsPath shadowPath = GetRoundedRectanglePath(shadowRect, cornerRadius))
				using (SolidBrush shadowBrush = new SolidBrush(shadowColor))
				{
					e.Graphics.FillPath(shadowBrush, shadowPath);
				}
			}

			// メインボタンの描画領域（影の分を差し引く）
			int penWidth = 1;
			Rectangle buttonRect = new Rectangle(
				penWidth / 2,
				penWidth / 2,
				ClientRectangle.Width - shadowSize - penWidth,
				ClientRectangle.Height - shadowSize - penWidth
			);

			using (GraphicsPath path = GetRoundedRectanglePath(buttonRect, cornerRadius))
			{
				// グラデーション描画（縦方向固定、下から上に明るくなる）
				Color endColor = CalculateGradationEndColor(backgroundColor, gradationOffset);
				using (LinearGradientBrush gradientBrush = new LinearGradientBrush(buttonRect, endColor, backgroundColor, LinearGradientMode.Vertical))
				{
					e.Graphics.FillPath(gradientBrush, path);
				}

				// 枠線を描画
				using (Pen pen = new Pen(Color.Black, penWidth))
				{
					e.Graphics.DrawPath(pen, path);
				}
			}

			// テキストを描画
			if (!string.IsNullOrEmpty(text))
			{
				DrawText(e.Graphics, buttonRect);
			}
		}

		/// <summary>
		/// 指定されたGraphicsオブジェクトと矩形領域にテキストを中央配置で描画します。
		/// </summary>
		/// <param name="graphics">描画に使用するGraphicsオブジェクト</param>
		/// <param name="rect">テキストを描画する矩形領域</param>
		/// <remarks>
		/// テキストは常に矩形の中央に配置されます。
		/// ForeColorプロパティとFontプロパティを使用してテキストを描画します。
		/// テキストが矩形領域を超える場合は、適切にクリッピングされます。
		/// </remarks>
		private void DrawText(Graphics graphics, Rectangle rect)
		{
			if (string.IsNullOrEmpty(text) || font == null)
				return;

			using (SolidBrush textBrush = new SolidBrush(foreColor))
			{
				// StringFormatを設定してテキストを中央配置
				using (StringFormat stringFormat = new StringFormat())
				{
					stringFormat.Alignment = StringAlignment.Center;
					stringFormat.LineAlignment = StringAlignment.Center;

					// テキストを描画
					graphics.DrawString(text, font, textBrush, rect, stringFormat);
				}
			}
		}

		/// <summary>
		/// 基準色とオフセット値からグラデーション終点色を計算します。
		/// </summary>
		/// <param name="baseColor">基準となる色</param>
		/// <param name="offset">明度のオフセット値（正の値で明るく、負の値で暗くなります）</param>
		/// <returns>計算されたグラデーション終点色</returns>
		/// <remarks>
		/// RGB各成分にオフセット値を加算し、0-255の範囲内に制限します。
		/// オフセット値が正の場合は色が明るくなり、負の場合は暗くなります。
		/// </remarks>
		private Color CalculateGradationEndColor(Color baseColor, int offset)
		{
			// RGB各成分にオフセットを適用し、0-255の範囲内に制限
			int red = Math.Max(0, Math.Min(255, baseColor.R + offset));
			int green = Math.Max(0, Math.Min(255, baseColor.G + offset));
			int blue = Math.Max(0, Math.Min(255, baseColor.B + offset));

			return Color.FromArgb(red, green, blue);
		}

		/// <summary>
		/// 指定された矩形と半径を使用して、角丸矩形のGraphicsPathを生成します。
		/// </summary>
		/// <param name="rect">角丸矩形の基となる矩形領域</param>
		/// <param name="radius">角丸の半径（ピクセル単位）</param>
		/// <returns>角丸矩形を表すGraphicsPathオブジェクト</returns>
		/// <remarks>
		/// 半径から計算される直径が矩形の幅または高さを超える場合は、
		/// 矩形のサイズに合わせて直径を制限します。
		/// 戻り値のGraphicsPathは呼び出し元でDisposeする必要があります。
		/// </remarks>
		private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
		{
			GraphicsPath path = new GraphicsPath();

			// 直径
			int diameter = radius * 2;

			if (diameter > rect.Width) diameter = rect.Width;
			if (diameter > rect.Height) diameter = rect.Height;

			path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
			path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
			path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
			path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
			path.CloseFigure();

			return path;
		}

		private void RoundedButtonControl_MouseUp(object sender, MouseEventArgs e)
		{
			if (isPressed)
			{
				Location = new Point(Location.X - originalShadowSize, Location.Y - originalShadowSize); // クリック後に位置を戻す
				shadowSize = originalShadowSize; // 影を復元
				isPressed = false;
				Invalidate(); // 再描画
			}
		}

		private void RoundedButtonControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (!isPressed)
			{
				originalShadowSize = shadowSize; // 元の影サイズを保存
				Location = new Point(Location.X + shadowSize, Location.Y + shadowSize); // 影のサイズ分だけ位置をずらす
				shadowSize = 0; // 影を消す
				isPressed = true;
				Invalidate(); // 再描画
			}
		}
	}
}
