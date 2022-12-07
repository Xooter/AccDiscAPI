using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Diagnostics;

namespace AccDiscAPI.Models.Channel
{
    public class ChannelText : ChannelBase
    {

        public string topic { get; set; }
        public int? rate_limit_per_user { get; set; }

        /// <summary>
        /// Get last X message of the channel.
        /// </summary>
        /// <remarks>
        /// Max 100 message.
        /// </remarks>
        /// <returns>
        /// List of Message
        /// </returns>
        public List<Message> GetChannelMessages(int message_count = 100)
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{this.id}/messages?limit={message_count}", Method.Get);

            var response = Global.client.Execute(Global.AddHeader(request));

            JArray message_array = JArray.Parse(response.Content.Replace("\"", "'"));

            List<Message> message_list = new List<Message>();

            foreach (JObject json in message_array)
            {
                message_list.Add(Global.ConvertJsonToMessage(json));
            }
            return message_list;
        }

        /// <summary>
        /// Change topic of channel.
        /// </summary>
        /// <remarks>
        /// Need server permissions.
        /// </remarks>
        public void ChangeTopic(string topic)
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{this.id}", Method.Patch);

            request.AddJsonBody(new { topic = topic });
            Global.client.Execute(Global.AddHeader(request));

            this.topic = topic;
        }

        /// <summary>
        /// Change the user wait-time in channel.
        /// </summary>
        /// <remarks>
        /// Need server permissions.
        /// </remarks>
        public void ChangeRate(int rate = 0)
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{this.id}", Method.Patch);

            request.AddJsonBody(new { rate_limit_per_user = rate });
            Global.client.Execute(Global.AddHeader(request));

            this.rate_limit_per_user = rate;
        }

        /// <summary>
        /// Get all pins of channel
        /// </summary>
        public List<Message> GetPins()
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{this.id}/pins", Method.Get);

            var response = Global.client.Execute(Global.AddHeader(request));

            if (response.Content.Contains("Missing Access"))
            {
                Debug.WriteLine("Missing Access");
                return null;
            }

            JArray json = JArray.Parse(response.Content);

            List<Message> message_list = new List<Message>();

            if (json.Count == 0) return new List<Message>();

            foreach (JObject arr_json in json)
            {
                message_list.Add(Global.ConvertJsonToMessage(arr_json));
            }

            return message_list;
        }
    }
}
