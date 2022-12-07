using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Diagnostics;

namespace AccDiscAPI.Models
{
    public class ChannelBase
    {
        public long id { get; set; }
        public string name { get; set; }
        public long guild_id { get; set; }
        public long? last_message_id { get; set; }
        public long? parent_id { get; set; }
        public bool nsfw { get; set; }
        public int position { get; set; }

        /// <summary>
        /// Send channel message.
        /// </summary>
        /// <returns>The message sent.</returns>
        public Message SendMessage(string message = "", bool tts = false)
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{this.id}/messages", Method.Post);

            request.AddJsonBody(new { content = message, tts = tts });
            var response = Global.client.Execute(Global.AddHeader(request));
            JObject json = JObject.Parse(response.Content.Replace("\"", "'"));

            Message msg_callback = Global.ConvertJsonToMessage(json);
            return msg_callback;
        }

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
                User inviter = new User()
                {
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

        /// <summary>
        /// Create invitation of channel
        /// </summary>
        /// <param name="max_age">Expiration</param>
        /// <param name="max_uses">Maximum number of uses </param>
        /// <param name="temporary">When temporary members go offline, they are automatically kicked unless assigned a role</param>
        /// <returns></returns>
        public Invite CreateInvitation(int max_age = 0, int max_uses = 0, bool temporary = false)
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{this.id}/invites", Method.Post);

            //max_age=604800
            //max_uses = 100
            request.AddJsonBody(new
            {
                max_age = (max_age < 0 ? 0 : max_age),
                max_uses = (max_uses < 0 ? 0 : max_uses),
                temporary = temporary
            });

            var response = Global.client.Execute(Global.AddHeader(request));
            JObject json = JObject.Parse(response.Content.Replace("\"", "'"));

            if (response.Content.Contains("Missing Access"))
            {
                Debug.WriteLine("Missing Access");
                return null;
            }

            User inviter = new User()
            {
                username = (string)json["inviter"]["username"],
                id = (long)json["inviter"]["id"],
                avatar = (string)json["inviter"]["avatar"],
                avatar_decoration = (string)json["inviter"]["avatar_decoration"],
                discriminator = (int)json["inviter"]["discriminator"],
                public_flags = (int)json["inviter"]["public_flags"]
            };

            Invite invite = new Invite()
            {
                code = (string)json["code"],
                uses = (int)json["uses"],
                expires_at = (string)json["expires_at"],
                created_at = (string)json["created_at"],
                inviter = inviter,
                temporary = (bool)json["temporary"],
                max_age = (int)json["max_age"],
                max_uses = (int)json["max_uses"],

            };

            return invite;
        }

        /// <summary>
        /// Delete channel
        /// </summary>
        public bool Delete()
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{this.id}", Method.Delete);

            var response = Global.client.Execute(Global.AddHeader(request));

            if (response.Content.Contains("Missing Access"))
            {
                Debug.WriteLine("Missing Access");
                return false;
            }
            return true;
        }
    }
}
