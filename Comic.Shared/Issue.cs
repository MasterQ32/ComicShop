using System.Collections.Generic;
using System.Xml.Serialization;

namespace ComicShop
{
	public class Issue 
    {
		/// <summary>
		/// Gets or sets the name of the issue.
		/// </summary>
		[XmlElement("name")]
		public string Name { get; set; }


		/// <summary>
		/// Gets or sets the cover image of the issue.
		/// </summary>
		[XmlElement("cover")]
		public XmlUri Cover { get; set; }

		/// <summary>
		/// Gets or sets a list of strips that build the issue.
		/// </summary>
		[XmlElement("strip")]
		public List<Strip> Strips { get; set; } = new List<Strip>();
	}
}
