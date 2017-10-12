using System;
using System.Drawing;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer.Chart
{
	//*********************************************************************
	//
	// Chart Class
	//
	// Base class implementation for BarChart and PieChart
	//
	//*********************************************************************

	abstract public class Chart
	{
		private const int _colorLimit = 12;

		private Color[] _color = 
			{ 
				Color.Chocolate,
				Color.YellowGreen,
				Color.Olive,
				Color.DarkKhaki,
				Color.Sienna,
				Color.PaleGoldenrod,
				Color.Peru,
				Color.Tan,
				Color.Khaki,
				Color.DarkGoldenrod,
				Color.Maroon,
				Color.OliveDrab
			};

		// Represent collection of all data points for the chart
		private ChartItemsCollection _dataPoints = new ChartItemsCollection();  

		// The implementation of this method is provided by derived classes
		public abstract Bitmap Draw();	

		public ChartItemsCollection DataPoints
		{
			get{ return _dataPoints; }
			set{ _dataPoints = value; }
		}

		public void SetColor(int index, Color NewColor)
		{
			if (index < _colorLimit) 
			{
				_color[index] = NewColor;
			}
			else
			{
				throw new Exception("Color Limit is " + _colorLimit);
			}
		}

		public Color GetColor(int index)
		{
			if (index < _colorLimit) 
			{
				return _color[index];
			}
			else
			{
				throw new Exception("Color Limit is " + _colorLimit);
			}
		}
	}
}