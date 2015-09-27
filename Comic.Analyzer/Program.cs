using HtmlAgilityPack;
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace ComicShop.Analyzer
{
	class Program
	{
		static void Main(string[] args)
		{

		}

		private static void ParseTrash()
		{
			var comic = new Comic()
			{
				Name = "Trash",
				Source = "http://www.mangareader.net/trash/"
			};

			using (var client = new WebClient())
			{
				for (int issueID = 1; issueID <= 26; issueID++)
				{
					Issue issue = new Issue()
					{
						Name = "Trash #" + issueID,
					};

					int stripCount = 0;

					// Gather issue information
					{
						string data = client.DownloadString(string.Format("http://www.mangareader.net/trash/{0}/", issueID));
						var doc = new HtmlDocument();
						doc.LoadHtml(data);

						var node = doc.DocumentNode.SelectSingleNode("//div[@id=\"selectpage\"]");
						issue.Cover = doc.DocumentNode.SelectSingleNode("//img[@id=\"img\"]")?.GetAttributeValue("src", null);

						var text = Regex.Match(node.InnerText, "of (?<num>\\d+)$");
						stripCount = int.Parse(text.Groups["num"].Value);
					}

					for (int stripID = 1; stripID <= stripCount; stripID++)
					{
						string url = string.Format("http://www.mangareader.net/trash/{0}/{1}", issueID, stripID);
						Console.WriteLine(url);
						string data = client.DownloadString(url);
						var doc = new HtmlDocument();
						doc.LoadHtml(data);

						var strip = new Strip();
						strip.Title = "Trash #" + issueID + " Page #" + stripID;

						strip.Url = doc.DocumentNode.SelectSingleNode("//img[@id=\"img\"]")?.GetAttributeValue("src", null);
						issue.Strips.Add(strip);
					}

					comic.Issues.Add(issue);
				}
			}
			using (var fs = File.Open("trash.xml", FileMode.Create, FileAccess.Write))
			{
				comic.Save(fs);
			}
		}

		static void ParsePomcomic()
		{
			var comic = new Comic()
			{
				Name = "Piece Of Me",
				Source = "http://www.pomcomic.com/"
			};
			var issue = new Issue()
			{
				Name = "Web Comic",
				Cover = "http://www.pomcomic.com/comics/292-bus-stop.jpg"
			};
			comic.Issues.Add(issue);

			string init = "http://www.pomcomic.com/comic-1";
			using (var client = new WebClient())
			{
				string next = init;
				while (next != null)
				{
					Console.WriteLine("Downloading {0}...", next);
					string data = client.DownloadString(next);
					var doc = new HtmlDocument();
					doc.LoadHtml(data);

					var strip = new Strip();

					strip.Title = doc.DocumentNode.SelectSingleNode("//h1[@id=\"comic-name\"]")?.InnerText;
					strip.Url = "http://www.pomcomic.com/" + doc.DocumentNode.SelectSingleNode("//div[@class=\"comic\"]/a/img")?.GetAttributeValue("src", null);

					if (strip.Title != null)
					{
						strip.Title = HttpUtility.HtmlDecode(strip.Title.Trim());
					}

					issue.Strips.Add(strip);

					var nextref = doc.DocumentNode.SelectSingleNode("//a[@id=\"next\"]")?.GetAttributeValue("href", null);

					next = null;
					if (nextref != null)
						next = "http://www.pomcomic.com/" + nextref;
				}
			}

			using (var fs = File.Open("pomcomic.xml", FileMode.Create, FileAccess.Write))
			{
				comic.Save(fs);
			}
		}
	}
}
