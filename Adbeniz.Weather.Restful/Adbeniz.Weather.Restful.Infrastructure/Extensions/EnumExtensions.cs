using System;

namespace Adbeniz.Weather.Restful.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
		public static T FromInt<T>(int value)
		{
			return (T)Enum.GetValues(typeof(T)).GetValue(value);
		}

		public static T FromString<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value);
		}
    }
}
