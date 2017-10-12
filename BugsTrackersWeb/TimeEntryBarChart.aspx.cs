using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using BugsTrackers.BusinessLogicLayer.Chart;

namespace BugsTrackers.Web
{
	//*********************************************************************
	//	TimeEntryBarChart Page
	//
	//	This page is used to generate bar graph image dynamically for Time Entry Page
	//
	//*********************************************************************
	
	public partial class TimeEntryBarChart : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Set return type to png image format
			Response.ContentType = "image/png";

			string xValues, yValues;

			// Get input parameters from query string
			xValues = Request.QueryString["xValues"];
			yValues = Request.QueryString["yValues"];

			if (xValues != null && yValues != null)
			{
				Bitmap bmp;
				MemoryStream memStream = new MemoryStream();
				BarGraph bar = new BarGraph(Color.White);
				
				// Set bar color for 7 days
				for (int i =0; i<7; i++) bar.SetColor(i, Color.Sienna);

				// Graph settings
				bar.VerticalTickCount = 2;
				bar.ShowLegend = false;
				bar.ShowData = true;
				bar.Height = 119;
				bar.Width = 195;
				bar.TopBuffer = 5;
				bar.BottomBuffer = 15;
				bar.FontColor = Color.Gray;

				bar.CollectDataPoints(xValues.Split("|".ToCharArray()), yValues.Split("|".ToCharArray()));
				bmp = bar.Draw();

				// Render BitMap Stream Back To Client
				bmp.Save(memStream, ImageFormat.Png);
				memStream.WriteTo(Response.OutputStream);
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
