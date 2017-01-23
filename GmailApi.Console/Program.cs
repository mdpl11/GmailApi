using GmailServiceInfraestructure;
using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;

namespace GmailQuickstart
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var gmailSearch = new GmailSearch
            {
                Unread = true,
                Label = new List<GmailSystemLabels>(new[] { GmailSystemLabels.Important })
            };
            var messages = GmailApi.Instance.ListMessages(gmailSearch);
            Console.Read();
        }
    }
}