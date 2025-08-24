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

		private Color backgroundColor = Color.LightSkyBlue; // デフォルト背景色
		/// <summary>
		/// ボタンの背景色を取得または設定します。
		/// </summary>
		[Browsable(true)]
		[Category("Appearance")]
		[Description("ボタンの背景色を設定します。RGB値で指定可能です。")]
		[DefaultValue(typeof(Color), "LightBlue")]
		public Color BackgroundColor
		{
			get { return backgroundColor; }
			set
			{
				backgroundColor = value;
				Invalidate(); // 再描画
			}
		}

		public RoundedButtonControl()
		{
			Size = new Size(100, 40);
		}

		/// <summary>
		/// コントロールの描画処理を行います。
		/// </summary>
		/// <param name="e">描画イベントの引数</param>
		/// <remarks>
		/// 角丸矩形の背景と枠線を、設定された色で描画します。
		/// アンチエイリアシングを有効にして滑らかな描画を行います。
		/// </remarks>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

			// 枠線の太さを考慮して描画領域を調整
			int penWidth = 1;
			Rectangle rect = new Rectangle(
				penWidth / 2,
				penWidth / 2,
				ClientRectangle.Width - penWidth,
				ClientRectangle.Height - penWidth
			);

			using (GraphicsPath path = GetRoundedRectanglePath(rect, cornerRadius))
			{
				// 背景色で塗りつぶし
				using (SolidBrush brush = new SolidBrush(backgroundColor))
				{
					e.Graphics.FillPath(brush, path);
				}

				// 枠線を描画
				using (Pen pen = new Pen(Color.Black, penWidth))
				{
					e.Graphics.DrawPath(pen, path);
				}
			}
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
	}
}
