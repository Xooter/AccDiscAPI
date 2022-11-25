using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;

namespace AccDiscAPI.Models
{
    public class Message
    {
       
        public long id;
        public List<Attachment> attachments;
        public User author;
        public long channel_id;
        public string content;
        public string edited_timestamp;
        public int flags;
        public bool mention_everyone;
        public List<Roll> mention_roles;
        public List<User> mentions;
        public Message referenced_message = null;
        public bool pinned;
        public string timestamp;
        public bool tts;
        public int type;

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
