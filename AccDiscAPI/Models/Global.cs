using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace AccDiscAPI.Models
{
    public static class Global
    {
        public static readonly RestClient client = new RestClient("https://discord.com");
        public static readonly HttpClient http_client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });

        /// <summary>
        /// Auth of discord.
        /// </summary>
        public static string Authorization = "";
        
        /// <summary>
        /// Cookie of an account.
        /// </summary>
        public static string Cookie = "";

        public static RestRequest AddHeader(RestRequest request)
        {
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", Authorization);
            request.AddHeader("Cookie", Cookie);
            return request;
        }

        public static Message ConvertJsonToMessage(JObject json)
        {
            List<Roll> roll_list = new List<Roll>();
            List<Attachment> attachments_list = new List<Attachment>();
            List<User> mentions_list = new List<User>();

            User author = new User()
            {
                id = (long)json["author"]["id"],
                discriminator = (int)json["author"]["discriminator"],
                public_flags = (int)json["author"]["public_flags"],
                username = (string)json["author"]["username"],
                avatar = (string)json["author"]["avatar"],
                avatar_decoration = (string)json["author"]["avatar_decoration"] == null ? "" : (string)json["author"]["avatar_decoration"],
            };

            Message refer_message = GetReferMessage(json);

            foreach (JObject att in json["attachments"])
            {
                Attachment attachment = new Attachment()
                {
                    id = (long)att["id"],
                    content_type = (string)att["content_type"],
                    filename = (string)att["filename"],
                    height = (int)att["height"],
                    width = (int)att["width"],
                    proxy_url = (string)att["proxy_url"],
                    size = (int)att["size"],
                    url = (string)att["url"],

                };
                attachments_list.Add(attachment);
            }

            foreach (JObject ment in json["mentions"])
            {
                User mention = new User()
                {
                    id = (long)ment["id"],
                    discriminator = (int)ment["discriminator"],
                    public_flags = (int)ment["public_flags"],
                    username = (string)ment["username"],
                    avatar = (string)ment["avatar"],
                    avatar_decoration = (string)ment["avatar_decoration"] == null ? "" : (string)ment["avatar_decoration"],

                };
                mentions_list.Add(mention);
            }

            foreach (JValue roll in json["mention_roles"])
            {
                Roll new_roll = new Roll()
                {
                    id = (long)roll
                };
                roll_list.Add(new_roll);
            }

            Message message = new Message()
            {
                id = (long)json["id"],
                attachments = attachments_list,
                author = author,
                channel_id = (long)json["channel_id"],
                content = (string)json["content"],
                edited_timestamp = (string)json["edited_timestamp"],
                flags = (int)json["flags"],
                mention_everyone = (bool)json["mention_everyone"],
                mention_roles = roll_list,
                mentions = mentions_list,
                pinned = (bool)json["pinned"],
                timestamp = (string)json["timestamp"],
                tts = (bool)json["tts"],
                type = (int)json["type"],

                referenced_message = refer_message
            };

            return message;
        }

        private static Message GetReferMessage(JObject json)
        {
            if (json["referenced_message"] == null || json["referenced_message"].ToString() == "") return new Message();

            List<Roll> refer_roll_list = new List<Roll>();
            List<Attachment> refer_attachments_list = new List<Attachment>();
            List<User> refer_mentions_list = new List<User>();

            User refer_author = new User()
            {
                id = (long)json["referenced_message"]["author"]["id"],
                discriminator = (int)json["referenced_message"]["author"]["discriminator"],
                public_flags = (int)json["referenced_message"]["author"]["public_flags"],
                username = (string)json["referenced_message"]["author"]["username"],
                avatar = (string)json["referenced_message"]["author"]["avatar"],
                avatar_decoration = (string)json["referenced_message"]["author"]["avatar_decoration"] == null ? "" : (string)json["referenced_message"]["author"]["avatar_decoration"],
            };

            foreach (JObject att in json["referenced_message"]["attachments"])
            {
                Attachment attachment = new Attachment()
                {
                    id = (long)att["id"],
                    content_type = (string)att["content_type"],
                    filename = (string)att["filename"],
                    height = (int)att["height"],
                    width = (int)att["width"],
                    proxy_url = (string)att["proxy_url"],
                    size = (int)att["size"],
                    url = (string)att["url"],

                };
                refer_attachments_list.Add(attachment);
            }

            foreach (JObject ment in json["referenced_message"]["mentions"])
            {
                User mention = new User()
                {
                    id = (long)ment["id"],
                    discriminator = (int)ment["discriminator"],
                    public_flags = (int)ment["public_flags"],
                    username = (string)ment["username"],
                    avatar = (string)ment["avatar"],
                    avatar_decoration = (string)ment["avatar_decoration"] == null ? "" : (string)ment["avatar_decoration"],

                };
                refer_mentions_list.Add(mention);
            }

            foreach (JValue roll in json["referenced_message"]["mention_roles"])
            {
                Roll new_roll = new Roll()
                {
                    id = (long)roll

                };
                refer_roll_list.Add(new_roll);
            }

            Message refer_message = new Message()
            {
                id = (long)json["referenced_message"]["id"],
                attachments = refer_attachments_list,
                author = refer_author,
                channel_id = (long)json["referenced_message"]["channel_id"],
                content = (string)json["referenced_message"]["content"],
                edited_timestamp = (string)json["referenced_message"]["edited_timestamp"],
                flags = (int)json["referenced_message"]["flags"],
                mention_everyone = (bool)json["referenced_message"]["mention_everyone"],
                mention_roles = refer_roll_list,
                mentions = refer_mentions_list,
                pinned = (bool)json["referenced_message"]["pinned"],
                timestamp = (string)json["referenced_message"]["timestamp"],
                tts = (bool)json["referenced_message"]["tts"],
                type = (int)json["referenced_message"]["type"]
            };

            return refer_message;
        }

        /// <summary>
        /// Search structure result
        /// </summary>
        public struct SearchResults
        {
            public List<Message> message_list;
            public int total_results;
        }
    }


}
