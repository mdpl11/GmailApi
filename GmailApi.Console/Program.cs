using GmailServiceInfraestructure;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;

namespace GmailQuickstart
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            GmailSearch();
            GmailSearchDetails();
            GetUpcomingEvents();
            Console.Read();
        }

        private static void GmailSearch()
        {
            var gmailSearch = new GmailSearch
            {
                Unread = true,
                Label = new List<GmailSystemLabels>(new[] { GmailSystemLabels.Important })
            };
            List<Message> messages = GmailApi.Instance.ListMessages(gmailSearch);
        }

        private static void GmailSearchDetails()
        {
            var gmailSearch = new GmailSearch
            {
                Unread = true,
                Label = new List<GmailSystemLabels>(new[] { GmailSystemLabels.Trash, GmailSystemLabels.Important }),
            };

            List<Message> messages = GmailApi.Instance.ListMessagesDetails(gmailSearch);
        }

        private static void GetUpcomingEvents()
        {
            Events events = CalendarApi.Instance.GetEvents();

            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }
        }
    }
}