using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ComicShop
{
	[XmlRoot("uri")]
	public class XmlUri : IXmlSerializable
	{
		private string value;

		public XmlUri()
		{

		}

		public XmlUri(Uri uri)
		{
			this.Uri = uri;
		}

		public XmlUri(string value)
		{
			this.Uri = new Uri(value);
		}

		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.Value = reader.ReadElementContentAsString();
		}

		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteValue(this.Value);
		}

		public static implicit operator Uri(XmlUri o)
		{
			return o == null ? null : o.Uri;
		}

		public static implicit operator XmlUri(Uri o)
		{
			return o == null ? null : new XmlUri(o);
		}

		public static implicit operator XmlUri(string o)
		{
			return o == null ? null : new XmlUri(o);
		}


		/// <summary>
		/// Gets or sets the uri value.
		/// </summary>
		[XmlIgnore]
		public Uri Uri
		{
			get { return new Uri(this.value); }
			set { this.value = value?.ToString(); }
		}

		/// <summary>
		/// Gets or sets the raw string value
		/// </summary>
		[XmlIgnore]
		public string Value
		{
			get { return value; }
			set { this.value = value; }
		}

		public override string ToString()
		{
			return this.value;
		}
	}
}