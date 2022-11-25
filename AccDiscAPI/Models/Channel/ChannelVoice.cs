using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccDiscAPI.Models.Channel
{
    public class ChannelVoice : ChannelBase
    {
       
        public int bitrate;
        public int user_limit;
        public string rtc_region;

        /// <summary>
        /// Change bitrate of the channel.
        /// </summary>
        /// <remarks>
        /// Need server permissions.
        /// </remarks>
        public void ChangeBitrate(int bitrate)
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{this.id}", Method.Patch);

            request.AddJsonBody(new { bitrate = bitrate });
            Global.client.Execute(Global.AddHeader(request));

            this.bitrate = bitrate;
        }

        /// <summary>
        /// Change the user limit per voice channel.
        /// </summary>
        /// <remarks>
        /// Need server permissions.
        /// </remarks>
        public void ChangeUserLimit(int user_limit=0)
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{this.id}", Method.Patch);

            request.AddJsonBody(new { user_limit = user_limit });
            Global.client.Execute(Global.AddHeader(request));

            this.user_limit = user_limit;
        }
    }
}
