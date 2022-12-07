using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;

namespace AccDiscAPI.Models
{
    public class Message
    {

        public long id { get; set; }
        public List<Attachment> attachments { get; set; }
        public User author { get; set; }
        public long channel_id { get; set; }
        public string content { get; set; }
        public string edited_timestamp { get; set; }
        public int flags { get; set; }
        public bool mention_everyone { get; set; }
        public List<Roll> mention_roles { get; set; }
        public List<User> mentions { get; set; }
        public Message referenced_message { get; set; }
        public bool pinned { get; set; }
        public string timestamp { get; set; }
        public bool tts { get; set; }
        public int type { get; set; }

        /// <summary>
        /// Delete this message.
        /// </summary>
        /// <remarks>
        /// Need server permissions if the message is not your.
        /// </remarks>
        /// <returns>True if the message was deleted successfully.</returns>
        public bool DeleteMessage()
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{this.channel_id}/messages/{this.id}", Method.Delete);
            var response = Global.client.Execute(Global.AddHeader(request));

            return (response.Content.Length > 0) ? false : true;
        }

        /// <summary>
        /// Edit this message.
        /// </summary>
        /// <remarks>
        /// Need server permissions if the message is not your.
        /// </remarks>
        /// <returns>A new Message edited</returns>
        public Message EditMessage(string message)
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{this.channel_id}/messages/{this.id}", Method.Patch);

            request.AddJsonBody(new { content = message });

            var response = Global.client.Execute(Global.AddHeader(request));
            JObject json = JObject.Parse(response.Content.Replace("\"", "'"));

            Message msg_callback = Global.ConvertJsonToMessage(json);
            return msg_callback;
        }

    }
}
