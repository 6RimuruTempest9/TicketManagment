using System;
using System.IO;
using System.Net;
using ThirdPartyEventEditor.CommonElements.Exceptions;

namespace ThirdPartyEventEditor.CommonElements.Converters
{
    public static class ImageConverter
    {
        public static string ConvertImageURLToBase64String(string url)
        {
			if (url == null)
			{
				throw new ArgumentNullException(nameof(url));
			}

			var bytes = GetImageBytesFromImageURL(url);

			return Convert.ToBase64String(bytes, 0, bytes.Length);
		}

        private static byte[] GetImageBytesFromImageURL(string url)
        {
			if (url == null)
            {
				throw new ArgumentNullException(nameof(url));
            }

			var bytes = default(byte[]);

			try
			{
				var request = WebRequest.Create(url);

				using (var response = request.GetResponse())
				using (var stream = response.GetResponseStream())
				using (var binaryReader = new BinaryReader(stream))
                {
					var length = (int)response.ContentLength;

					bytes = binaryReader.ReadBytes(length);
				}
			}
			catch (Exception ex)
			{
				throw new ConvertException("Error during try getting image from url.", ex);
			}

			return bytes;
		}
    }
}