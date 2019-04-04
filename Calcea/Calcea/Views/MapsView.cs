using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace Calcea.Views
{
	public class MapsView : Map
	{
		public List<Position> RouteCoordinates { get; set; }

		public MapsView()
		{
		}
	}
}
