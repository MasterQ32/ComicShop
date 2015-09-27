using System.Xml.Serialization;

namespace ComicShop
{
	/// <summary>
	/// A single comic strip or page.
	/// </summary>
	[XmlRoot("strip")]
	public class Strip
	{
		/// <summary>
		/// Gets or sets the url where the comic is located.
		/// </summary>
		[XmlElement("image")]
		public XmlUri Url { get; set; }

		/// <summary>
		/// Gets or sets the title of the comic.
		/// </summary>
		[XmlElement("title")]
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets some additional text that is displayed in addition to the comic itself.
		/// </summary>
		[XmlElement("flavour")]
		public string Flavour { get; set; }
	}
}