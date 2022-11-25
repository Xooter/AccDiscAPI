using AccDiscAPI.Models.Channel;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;

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
    }
}
