using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;

namespace AccDiscAPI.Models
{
    public class ChannelBase
    {
        public long id;
        public string name;
        public long guild_id;
        public long? last_message_id;
        public long? parent_id;
        public bool nsfw;
        public int position;

        /// <summary>
        /// Get Invites of channel.
        /// </summary>
        /// <remarks>
        /// Need server permissions.
        /// </remarks>
        /// <returns>
        /// List of Invites.
        /// </returns>
        public List<Invite> GetInvites()
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{this.id}/invites", Method.Get);

            var response = Global.client.Execute(Global.AddHeader(request));

            if (response.Content.Contains("The resource is being rate limited.") ||
                response.Content.Contains("Missing Permissions") ||
                response.Content.Contains("You are being rate limited."))
                return new List<Invite>();

            JArray json = JArray.Parse(response.Content);

            List<Invite> result = new List<Invite>();

            foreach (var item in json)
            {
                User inviter = new User() {
                    username = (string)item["inviter"]["username"],
                    id = (long)item["inviter"]["id"],
                    avatar = (string)item["inviter"]["avatar"],
                    avatar_decoration = (string)item["inviter"]["avatar_decoration"],
                    discriminator = (int)item["inviter"]["discriminator"],
                    public_flags = (int)item["inviter"]["public_flags"]
                };

                result.Add(new Invite()
                {
                    code = (string)item["code"],
                    uses = (int)item["uses"],
                    expires_at = (string)item["expires_at"],
                    created_at = (string)item["created_at"],
                    inviter = inviter,
                    temporary = (bool)item["temporary"],
                    max_age = (int)item["max_age"],
                    max_uses = (int)item["max_uses"],

                });
            }

            return result;
        }

        /// <summary>
        /// Change name of channel.
        /// </summary>
        /// <remarks>
        /// Need server permissions.
        /// </remarks>
        public void ChangeName(string name)
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{this.id}", Method.Patch);

            request.AddJsonBody(new { name = name });
            Global.client.Execute(Global.AddHeader(request));

            this.name = name;
        }

       
    }
}
