using Google.Apis.Gmail.v1.Data;
using System.Collections.Generic;
using System.Text;

namespace GmailServiceInfraestructure
{
    /// <summary>
    /// Gmail query search data information.
    /// </summary>
    public class GmailSearch
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public GmailSearch()
        {
            Label = new List<GmailSystemLabels>();
        }

        /// <summary>
        /// Get or set the gmail message sender. Could be the email or the name.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Get or set label reference to search.
        /// </summary>
        public IList<GmailSystemLabels> Label { get; set; }

        /// <summary>
        /// Get or set the value to filter by read status of the request query.
        /// </summary>
        public bool Unread { get; set; }

        /// <summary>
        /// Converts the value of this instance to a gmail query.
        /// </summary>
        /// <returns>A string whose value is the same as this instance.</returns>
        public override string ToString()
        {
            var toString = new StringBuilder();
            if (!string.IsNullOrEmpty(From))
            {
                toString.Append($" from:{From}");
            }
            if (Unread)
            {
                toString.Append(" is:unread");
            }
            return toString.ToString();
        }
    }
}