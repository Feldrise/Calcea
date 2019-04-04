using System;
using System.Collections.Generic;
using System.Text;

namespace Calcea.Models
{
	public enum MenuItemType
	{
		Browse,
		Schemas,
		Maps,
		About
	}
	public class HomeMenuItem
	{
		public MenuItemType Id { get; set; }

		public string Title { get; set; }
		public string ImageSrc { get; set; }
	}
}
