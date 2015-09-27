using System.Collections.Generic;

using System.IO;
using System.Xml.Serialization;

namespace ComicShop
{
	[XmlRoot("comic")]
	public class Comic
	{
		private static readonly XmlSerializer serializer = new XmlSerializer(typeof(Comic));

		/// <summary>
		/// Gets or sets the name of the comic.
		/// </summary>
		[XmlElement("name")]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the original site of the comic.
		/// </summary>
		[XmlElement("source")]
		public XmlUri Source { get; set; }

		/// <summary>
		/// Gets or sets the list of issues.
		/// </summary>
		[XmlElement("issue")]
		public List<Issue> Issues { get; set; } = new List<Issue>();

		#region Static Saving / Loading

		public static Issue Load(Stream stream)
		{
			return serializer.Deserialize(stream) as Issue;
		}

		public void Save(Stream stream)
		{
			serializer.Serialize(stream, this);
		}

		#endregion
	}
}
