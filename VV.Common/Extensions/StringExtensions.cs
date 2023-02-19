namespace VV.Common.Extensions;

public static class StringExtensions
{
	public static string Truncate(this string value, int maxLength)
	{
		if (String.IsNullOrWhiteSpace(value) || maxLength <= 0) return "";
		if (value.Length <= maxLength) return value;
		return value.Substring(0, maxLength) + "...";		
	}
}
