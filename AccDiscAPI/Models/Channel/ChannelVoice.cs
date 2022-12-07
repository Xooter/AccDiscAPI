using RestSharp;

namespace AccDiscAPI.Models.Channel
{
    public class ChannelVoice : ChannelBase
    {

        public int bitrate { get; set; }
        public int user_limit { get; set; }
        public string rtc_region { get; set; }

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
        public void ChangeUserLimit(int user_limit = 0)
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{this.id}", Method.Patch);

            request.AddJsonBody(new { user_limit = user_limit });
            Global.client.Execute(Global.AddHeader(request));

            this.user_limit = user_limit;
        }
    }
}
