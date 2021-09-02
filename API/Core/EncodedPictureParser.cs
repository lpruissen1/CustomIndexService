namespace Core
{
	public static class EncodedPictureParser
	{
		public static ParsedResponse Parse(string encodedString)
		{
			var splitString = encodedString.Split(',');
			var mimeType = splitString[0].Split(':')[1].Split(';')[0];
			var content = splitString[1];

			return new ParsedResponse { MimeType = mimeType, Content = content };
		}
	}
}
