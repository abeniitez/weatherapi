using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Adbeniz.Weather.Restful.Infrastructure.Extensions
{
    public static class ObjectEssential
    {
		public static string ToXml(this object source, XmlSerializer serializer = null, bool omitStandardNamespaces = false)
		{
			XmlSerializerNamespaces ns = null;
			if (omitStandardNamespaces)
			{
				ns = new XmlSerializerNamespaces();
				ns.Add("", "");
			}
			using (var textWriter = new StringWriter())
			{
				var settings = new XmlWriterSettings() { Indent = true };
				using (var xmlWriter = XmlWriter.Create(textWriter, settings))
				{
					( serializer ?? new XmlSerializer(source.GetType()) ).Serialize(xmlWriter, source, ns);
				}

				return textWriter.ToString();
			}
		}

		public static string ToJson(this object source)
		{
			return JsonConvert.SerializeObject(source);
		}

		public static T FromXml<T>(String xml)
		{
			T returnedXmlClass = default(T);

			try
			{
				using (TextReader reader = new StringReader(xml))
				{
					try
					{
						returnedXmlClass =
							(T)new XmlSerializer(typeof(T)).Deserialize(reader);
					}
					catch (InvalidOperationException)
					{
						// String passed is not XML, simply return defaultXmlClass
					}
				}
			}
			catch (Exception ex)
			{
			}

			return returnedXmlClass ;
		}

		public static string ConvertToCelcius(double kelvin)
		{
			string celcius = (kelvin - 273.15).ToString();
			celcius = celcius.Substring(0,celcius.IndexOf(",")+2);
			return $"{celcius}º";
		}
    }
}
