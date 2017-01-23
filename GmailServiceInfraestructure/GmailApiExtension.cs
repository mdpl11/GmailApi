using System;
using System.Text;

namespace Google.Apis.Gmail.v1.Data
{
    /// <summary>
    /// Gmail api extensions.
    /// </summary>
    public static class GmailApiExtension
    {
        /// <summary>
        /// Get the message data wondering about mimetype.
        /// </summary>
        /// <param name="body">A single MIME message part.</param>
        /// <returns>Return an string with the label message</returns>
        public static string GetMessageData(this MessagePart body)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }
            if (body.MimeType == "text/html")
            {
                byte[] data = Convert.FromBase64String(body.Body.Data.Replace("-", "+").Replace("_", "/"));
                return Encoding.UTF8.GetString(data);
            }
            return body.Body?.Data ?? string.Empty;
        }
    }
}