using AccDiscAPI.Models.Channel;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static AccDiscAPI.Models.Global;

namespace AccDiscAPI.Models
{
    public class Guild
    {
        public long id;
        public string name;
        public long? afk_channel_id;
        public int? afk_timeout;
        public string discovery_splash;
        //todo:public List<Emoji> emojis
        public int? explicit_content_filter;
        public List<string> features;
        public string icon;
        public int? max_members;
        public bool nsfw;
        public int? nsfw_level;
        public long owner_id;
        public string preferred_locale;
        public int? premium_tier;
        public long? public_updates_channel_id;
        public string region;
        public List<Roll> roles;
        public long? rules_channel_id;
        public int verification_level;

        public List<ChannelText> TextChannel;
        public List<ChannelVoice> VoiceChannel;


        /// <summary>
        /// Count the number of member of each roll.
        /// </summary>
        /// <remarks>
        /// Need server permissions.
        /// </remarks>
        public void GetRolesCount()
        {
            var request = new RestRequest($"https://discord.com/api/v9/guilds/{this.id}/roles/member-counts", Method.Get);

            var response = Global.client.Execute(Global.AddHeader(request));

            JObject json = JObject.Parse(response.Content);

            foreach (var item in roles)
            {
                int member_count = (int)json[item.id.ToString()];

                item.members = member_count;
            }
        }

        /// <summary>
        /// Save Guild avatar asynchronously
        /// </summary>
        public async void SaveAvatar(string file_name = "", string path = "")
        {
            file_name = (file_name == "") ? $"avatar_{this.name.Replace(" ","_")}" : file_name;

            http_client.DefaultRequestHeaders.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            http_client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate");
            try
            {
                byte[] data = await http_client.GetByteArrayAsync($"https://cdn.discordapp.com/icons/{this.id}/{this.icon}.gif");

                File.WriteAllBytes(!string.IsNullOrEmpty(path) ? path + "/" : path + $"{file_name}.gif", data);
            }
            catch (Exception ex)
            {
                byte[] data = await http_client.GetByteArrayAsync($"https://cdn.discordapp.com/icons/{this.id}/{this.icon}.webp");

                File.WriteAllBytes(!string.IsNullOrEmpty(path) ? path + "/" : path + $"{file_name}.jpg", data);
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Search Messages in the guild
        /// </summary>
        /// <returns></returns>
        public SearchResults? SearchMessage(string content)
        {
            var request = new RestRequest($"https://discord.com/api/v9/guilds/{this.id}/messages/search?content={content}", Method.Get);

            var response = Global.client.Execute(Global.AddHeader(request));

            if (response.Content.Contains("Missing Access"))
            {
                Debug.WriteLine("Missing Access");
                return null;
            }

            JObject json = JObject.Parse(response.Content);
            JArray message_r = (JArray)json["messages"];

            List<Message> message_list = new List<Message>();

            if (message_r.Count == 0) return null;

            foreach (var arr_json in message_r)
            {
                JObject new_json = JObject.Parse(arr_json[0].ToString());
                message_list.Add(Global.ConvertJsonToMessage(new_json));
            }

            SearchResults result = new SearchResults()
            {
                message_list = message_list,
                total_results = (int)json["total_results"]
            };

            return result;
        }

        /// <summary>
        /// Exit to the guild
        /// </summary>
        public void Exit() 
        {
            var request = new RestRequest($"https://discord.com/api/v9/users/@me/guilds/{this.id}", Method.Delete);

            Global.client.Execute(Global.AddHeader(request));
        }
    }
}
