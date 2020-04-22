using System;

namespace Sport_A_Lyzer.Helpers
{
	public static class LocalTimeProvider
	{

		public static DateTime GetLocalTime( DateTime time, string timeZoneId = "FLE Standard Time" )
		{
			return TimeZoneInfo.ConvertTimeBySystemTimeZoneId( time, timeZoneId );
		}
	}
}
