using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace GmailServiceInfraestructure
{
    /// <summary>
    /// Gmail client service.
    /// </summary>
    public sealed class GmailApi
    {
        private const string ApplicationName = "Gmail API .NET";
        private const string userId = "me";
        private static volatile GmailApi instance;
        private static object syncRoot = new object();

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        private string[] Scopes = { GmailService.Scope.GmailReadonly };

        private GmailService service;

        private GmailApi()
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-quickstart.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            // Create Gmail API service.
            service = new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        /// <summary>
        /// Get an instance of the gmail api service.
        /// </summary>
        public static GmailApi Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GmailApi();
                    }
                }
                return instance;
            }
        }

        // <summary>
        /// Retrieve a Message by ID.
        /// </summary>
        /// <param name="messageId">ID of Message to retrieve.</param>
        public Message GetMessage(string messageId)
        {
            if (string.IsNullOrEmpty(messageId))
            {
                throw new ArgumentException("Argument cannot be null or empty.", nameof(messageId));
            }
            var resource = service.Users.Messages.Get(userId, messageId);
            resource.Format = UsersResource.MessagesResource.GetRequest.FormatEnum.Full;
            return resource.Execute();
        }

        /// <summary>
        /// List all Messages of the user's mailbox matching the query.
        /// </summary>
        /// <param name="query">String used to filter Messages returned.</param>
        public List<Message> ListMessages(GmailSearch query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            List<Message> result = new List<Message>();
            UsersResource.MessagesResource.ListRequest request = service.Users.Messages.List(userId);
            if (query.Label != null && query.Label.Any())
            {
                request.LabelIds = new Repeatable<string>(GmailApiHelper.GetLabel(query.Label));
            }
            request.Q = query.ToString();
            do
            {
                ListMessagesResponse response = request.Execute();
                if(response.Messages != null)
                {
                    result.AddRange(response.Messages);
                }
                if (response.NextPageToken != null)
                {
                    request.PageToken = response.NextPageToken;
                }
            }
            while (!string.IsNullOrEmpty(request.PageToken));
            return result;
        }

        /// <summary>
        /// List all detailed messages of the user's mailbox matching the query.
        /// </summary>
        /// <param name="query">String used to filter Messages returned.</param>
        public List<Message> ListMessagesDetails(GmailSearch query)
        {
            List<Message> result = ListMessages(query);
            var returnList = new List<Message>();
            foreach (var message in result)
            {
                returnList.Add(GetMessage(message.Id));
            }
            return returnList;
        }
    }
}
