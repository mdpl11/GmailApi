using System;
using System.Collections.Generic;
using System.Linq;

namespace Google.Apis.Gmail.v1.Data
{
    /// <summary>
    /// Gmail api helper.
    /// </summary>
    public static class GmailApiHelper
    {
        /// <summary>
        /// Get label ids string.
        /// </summary>
        /// <param name="labels">Label enum list.</param>
        /// <returns>Return a label id <see cref="IEnumerable{string}"/>.</returns>
        public static IEnumerable<string> GetLabel(IEnumerable<GmailSystemLabels> labels)
        {
            if (labels == null)
            {
                throw new ArgumentNullException(nameof(labels));
            }
            foreach (var label in labels.Distinct())
            {
                yield return GetLabel(label);
            }
        }

        /// <summary>
        /// Get the label id.
        /// </summary>
        /// <param name="label">Label enum.</param>
        /// <returns>Return the label <see cref="string"/> id.</returns>
        public static string GetLabel(GmailSystemLabels label)
        {
            switch (label)
            {
                case GmailSystemLabels.Chat:
                    return "CHAT";

                case GmailSystemLabels.Draft:
                    return "DRAFT";

                case GmailSystemLabels.Forums:
                    return "CATEGORY_FORUMS";

                case GmailSystemLabels.Important:
                    return "IMPORTANT";

                case GmailSystemLabels.Inbox:
                    return "INBOX";

                case GmailSystemLabels.Personal:
                    return "CATEGORY_PERSONAL";

                case GmailSystemLabels.Promotions:
                    return "CATEGORY_PROMOTIONS";

                case GmailSystemLabels.Sent:
                    return "SENT";

                case GmailSystemLabels.Social:
                    return "CATEGORY_SOCIAL";

                case GmailSystemLabels.Spam:
                    return "SPAM";

                case GmailSystemLabels.Starred:
                    return "STARRED";

                case GmailSystemLabels.Trash:
                    return "TRASH";

                case GmailSystemLabels.Unread:
                    return "UNREAD";

                case GmailSystemLabels.Updates:
                    return "CATEGORY_UPDATES";

                default:
                    return string.Empty;
            }
        }
    }
}