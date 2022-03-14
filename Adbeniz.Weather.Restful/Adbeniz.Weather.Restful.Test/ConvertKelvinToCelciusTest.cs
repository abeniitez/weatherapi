using System;
using Adbeniz.Weather.Restful.Infrastructure.Extensions;
using Xunit;

namespace Adbeniz.Weather.Restful.Test
{
    public class ConvertKelvinToCelciusTest
    {
		[Theory]
		[InlineData(270,"-3,1º")]
		[InlineData(285.69,"12,5º")]
		[InlineData(285.6934,"12,5º")]
		[InlineData(285.69346465,"12,5º")]
		public void ConvertKelvinToCelciusShouldBeInString(double kelvin, string expected)
		{
			string celcius = ObjectEssential.ConvertToCelcius(kelvin);
			Assert.Equal(expected, celcius);
		}
    }
}
