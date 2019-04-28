using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calcea.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calcea.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SchemasPage : ContentPage
	{
		public FrontSBody Body;
		public FrontSBody PerfectBody;

		private float GX;
		private float GY;

		public SchemasPage()
		{
			InitializeComponent();

			Body = new FrontSBody
			{
				Size = 172,
				FootToFootDistance = 16,
				RightFootToG = 5
			};

			PerfectBody = new FrontSBody()
			{
				Size = 172,
				FootToFootDistance = 16,
				RightFootToG = 8
			};
		}

		private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
		{
			SKImageInfo info = e.Info;
			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();

			SKPaint paint = new SKPaint
			{
				Color = SKColors.Black,
				TextSize = 40
			};

			float fontSpacing = paint.FontSpacing;
			float x = 20;               // left margin
			float y = fontSpacing;      // first baseline
			float indent = 100;

			canvas.DrawText("SKCanvasView Height and Width:", x, y, paint);
			y += fontSpacing;
			canvas.DrawText(String.Format("{0:F2} x {1:F2}",
					CanvasView.Width, CanvasView.Height),
				x + indent, y, paint);
			y += fontSpacing * 2;
			canvas.DrawText("SKCanvasView CanvasSize:", x, y, paint);
			y += fontSpacing;
			canvas.DrawText(CanvasView.CanvasSize.ToString(), x + indent, y, paint);
			y += fontSpacing * 2;
			canvas.DrawText("SKImageInfo Size:", x, y, paint);
			y += fontSpacing;
			canvas.DrawText(info.Size.ToString(), x + indent, y, paint);
		}

		private void OnPainting2(object sender, SKPaintSurfaceEventArgs args)
		{
			SKImageInfo info = args.Info;
			SKSurface surface = args.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();

			canvas.DrawColor(SKColor.Parse("#cecece"));

			GX = 0.5f * info.Width;
			GY = ((172.0f - 88.7f) * info.Height / Body.Size);

			PaintGrid(canvas, info);
			PaintPerfectBody(canvas, info);
			PaintUserBody(canvas, info);
		}

		private void PaintGrid(SKCanvas canvas, SKImageInfo info)
		{
			SKPath gridPath = new SKPath();

			// Draw the grid
			gridPath.MoveTo(GX, 0);
			gridPath.LineTo(GX, info.Height);
			gridPath.MoveTo(0, GY);
			gridPath.LineTo(info.Width, GY);

			// Create two SKPaint objects
			SKPaint gridPaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = SKColors.Black,
				StrokeWidth = 2
			};

			canvas.DrawPath(gridPath, gridPaint);
		}

		private void PaintUserBody(SKCanvas canvas, SKImageInfo info)
		{
			// Create the paths
			SKPath bodyPath = new SKPath();

			float rightFootX = -Body.RightFootToG;
			float rightFootY = 0.516f * Body.Size;

			float leftFootX = Body.FootToFootDistance - Body.RightFootToG;
			float leftFootY = 0.516f * Body.Size;

			// Draw the body
			// Fomat : bodyPath.LineTo(([RELATIVE X COORDINATE] + [DEMI X AXE]) * info.Width / [X AXE SIZE], ([RELATIVE Y COORDINATE] + [DEMI Y AXE]) * info.Height / [Y AXE SIZE]);
			bodyPath.MoveTo(GX, GY);
			bodyPath.LineTo(ConvertToRelativeX(rightFootX, info), ConvertToRelativeY(rightFootY, info));
			bodyPath.MoveTo(GX, GY);
			bodyPath.LineTo(ConvertToRelativeX(leftFootX, info), ConvertToRelativeY(leftFootY, info));
			bodyPath.MoveTo(GX, GY);
			bodyPath.LineTo(GX, 0);

			// Create two SKPaint objects
			SKPaint bodyPaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = SKColors.Brown,
				StrokeWidth = 30
			};

			canvas.DrawPath(bodyPath, bodyPaint);
		}

		private void PaintPerfectBody(SKCanvas canvas, SKImageInfo info)
		{
			// Create the paths
			SKPath bodyPath = new SKPath();

			float rightFootX = -PerfectBody.RightFootToG;
			float rightFootY = 0.516f * PerfectBody.Size;

			float leftFootX = PerfectBody.FootToFootDistance - PerfectBody.RightFootToG;
			float leftFootY = 0.516f * PerfectBody.Size;

			// Draw the body
			// Fomat : bodyPath.LineTo(([RELATIVE X COORDINATE] + [DEMI X AXE]) * info.Width / [X AXE SIZE], ([RELATIVE Y COORDINATE] + [DEMI Y AXE]) * info.Height / [Y AXE SIZE]);
			bodyPath.MoveTo(GX, GY);
			bodyPath.LineTo(ConvertToRelativeX(rightFootX, info), ConvertToRelativeY(rightFootY, info));
			bodyPath.MoveTo(GX, GY);
			bodyPath.LineTo(ConvertToRelativeX(leftFootX, info), ConvertToRelativeY(leftFootY, info));
			bodyPath.MoveTo(GX, GY);
			bodyPath.LineTo(GX, 0);

			// Create two SKPaint objects
			SKPaint bodyPaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = SKColors.DarkGreen,
				StrokeWidth = 30
			};

			canvas.DrawPath(bodyPath, bodyPaint);
		}

		private float ConvertToRelativeX(float original, SKImageInfo info)
		{
			return (original + 20.0f) * info.Width / 40.0f;
		}

		private float ConvertToRelativeY(float original, SKImageInfo info)
		{
			return (original + Body.Size / 2.0f) * info.Height / Body.Size;
		}
	}
} // geoffrey