using System.Collections.Generic;
using AccDiscAPI.Models;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AccDiscAPI.Models.Channel;

namespace AccDiscAPI
{
    public class AccDisc
    {
        /// <summary>
        /// Get last X message of any channel.
        /// </summary>
        /// <remarks>
        /// Max 100 message.
        /// </remarks>
        /// <returns>
        /// List of Messages.
        /// </returns>
        public List<Message> GetChannelMessages(long channel_id, int message_count = 100)
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{channel_id}/messages?limit={message_count}", Method.Get);

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
        /// Get user by the id.
        /// </summary>
        /// <returns>
        /// User class.
        /// </returns>
        public User GetUserInfo(long user_id)
        {
            var request = new RestRequest($"https://discord.com/api/v9/users/{user_id}/profile", Method.Get);

            var response = Global.client.Execute(Global.AddHeader(request));

            JObject json = JObject.Parse(response.Content.Replace("\"", "'"));

            List<Accounts> acc = JsonConvert.DeserializeObject<List<Accounts>>(json["connected_accounts"].ToString());
            List<UserGuild> mg = JsonConvert.DeserializeObject<List<UserGuild>>(json["mutual_guilds"].ToString());

            foreach (UserGuild item in mg)
            {
                var item_request = new RestRequest($"https://discord.com/api/v9/users/{user_id}/profile?guild_id={item.id}", Method.Get);

                var item_response = Global.client.Execute(Global.AddHeader(item_request));

                JObject item_json = JObject.Parse(item_response.Content.Replace("\"", "'"));
                if (item_json["guild_member"]["roles"].ToString() != "")
                {
                    List<string> roles = JsonConvert.DeserializeObject<List<string>>(item_json["guild_member"]["roles"].ToString());
                    item.rolls = roles;
                }

                item.joined_at = item_json["guild_member"]["joined_at"].ToString();
                item.nick = item_json["guild_member"]["nick"].ToString();
            }


            User result = new User()
            {
                id = (long)json["user"]["id"],
                discriminator = (int)json["user"]["discriminator"],
                public_flags = (int)json["user"]["public_flags"],
                flags = (int)json["user"]["flags"],
                username = (string)json["user"]["username"],
                bio = (string)json["user"]["bio"],
                avatar = (string)json["user"]["avatar"],
                avatar_decoration = (string)json["user"]["avatar_decoration"] == null ? "" : (string)json["user"]["avatar_decoration"],
                accent_color = (string)json["user"]["accent_color"] == null ? "" : (string)json["user"]["accent_color"],
                banner = (string)json["user"]["banner"] == null ? "" : (string)json["user"]["banner"],
                banner_color = (string)json["user"]["banner_color"] == null ? "" : (string)json["user"]["banner_color"],

                connected_accounts = acc,
                mutual_guilds = mg
            };

            return result;
        }

        /// <summary>
        /// Get Guild by id.
        /// </summary>
        /// <returns>
        /// Guild class.
        /// </returns>
        public Guild GetGuildInfo(long guild_id)
        {
            var request = new RestRequest($"https://discord.com/api/v9/guilds/{guild_id}", Method.Get);

            var response = Global.client.Execute(Global.AddHeader(request));

            JObject json = JObject.Parse(response.Content.Replace("\"", "'"));

            List<Roll> roles = new List<Roll>();
            foreach (JObject item in json["roles"])
            {
                roles.Add(new Roll()
                {
                    id = (long)item["id"],
                    name = (string)item["name"],
                    color = (int)item["color"],
                    description = (string)item["description"],
                    flags = (int)item["flags"],
                    hoist = (bool)item["hoist"],
                    managed = (bool)item["managed"],
                    mentionable = (bool)item["mentionable"],
                    position = (int)item["position"],
                    permissions = (long)item["permissions"],
                });
            }

            List<string> features = JsonConvert.DeserializeObject<List<string>>(json["features"].ToString());

            var channels_request = new RestRequest($"https://discord.com/api/v9/guilds/{guild_id}/channels", Method.Get);

            var channels_response = Global.client.Execute(Global.AddHeader(channels_request));
            JArray channels_json = JArray.Parse(channels_response.Content);

            List<ChannelText> TextChannel = new List<ChannelText>();
            List<ChannelVoice> VoiceChannel = new List<ChannelVoice>();

            foreach (JObject channel in channels_json)
            {
                //type 0 = text
                //type 2 = voice

                //type 4 = titulo
                //type 13
                //type 15 = foro
                
                if ((int)channel["type"] == 0)
                {
                    TextChannel.Add(new ChannelText()
                    {
                        id = (long)channel["id"],
                        name = (string)channel["name"],
                        guild_id = (long)channel["guild_id"],
                        last_message_id = (long?)channel["last_message_id"],
                        parent_id = (long?)channel["parent_id"],
                        nsfw = (bool)channel["nsfw"],
                        position = (int)channel["position"],
                        rate_limit_per_user = (int?)channel["rate_limit_per_user"],
                        topic = (string)channel["topic"]
                    });
                }
                else if ((int)channel["type"] == 2)
                {
                    VoiceChannel.Add(new ChannelVoice()
                    {
                        id = (long)channel["id"],
                        name = (string)channel["name"],
                        guild_id = (long)channel["guild_id"],
                        last_message_id = (long?)channel["last_message_id"],
                        parent_id = (long?)channel["parent_id"],
                        nsfw = (bool)channel["nsfw"],
                        position = (int)channel["position"],
                        bitrate = (int)channel["bitrate"],
                        user_limit = (int)channel["user_limit"],
                        rtc_region = (string)channel["rtc_region"]
                    });
                }
            }

            Guild result = new Guild()
            {
                id = (long)json["id"],
                name = (string)json["name"],
                afk_channel_id = (long?)json["afk_channel_id"],
                afk_timeout = (int?)json["afk_timeout"],
                discovery_splash = (string)json["discovery_splash"],
                explicit_content_filter = (int?)json["explicit_content_filter"],
                features = features,
                icon = (string)json["icon"],
                max_members = (int)json["max_members"],
                nsfw = (bool)json["nsfw"],
                nsfw_level = (int?)json["nsfw_level"],
                owner_id = (long)json["owner_id"],
                preferred_locale = (string)json["preferred_locale"],
                premium_tier = (int?)json["premium_tier"],
                public_updates_channel_id = (long?)json["public_updates_channel_id"],
                region = (string)json["region"],
                roles = roles,
                rules_channel_id = (long?)json["rules_channel_id"],
                verification_level = (int)json["verification_level"],
                TextChannel = TextChannel,
                VoiceChannel = VoiceChannel,
            };
            
            return result;
        }

        /// <summary>
        /// Add personal note by user id.
        /// </summary>
        public void AddNote(long user_id, string note)
        {
            var request = new RestRequest($"https://discord.com/api/v9/users/@me/notes/{user_id}", Method.Put);

            request.AddJsonBody(new { note = note });
            Global.client.Execute(Global.AddHeader(request));
        }

        /// <summary>
        /// Edit this message.
        /// </summary>
        /// <remarks>
        /// Need server permissions if the message is not your.
        /// </remarks>
        /// <returns>A new Message edited.</returns>
        public Message EditMessage(long message_id, long channel_id, string message)
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{channel_id}/messages/{message_id}", Method.Patch);

            request.AddJsonBody(new { content = message });

            var response = Global.client.Execute(Global.AddHeader(request));
            JObject json = JObject.Parse(response.Content.Replace("\"", "'"));

            Message msg_callback = Global.ConvertJsonToMessage(json);
            return msg_callback;
        }

        /// <summary>
        /// Delete any message.
        /// </summary>
        /// <remarks>
        /// Need server permissions if the message is not your.
        /// </remarks>
        /// <returns>True if the message was deleted successfully.</returns>
        public bool DeleteMessage(long message_id, long channel_id)
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{channel_id}/messages/{message_id}", Method.Delete);
            var response = Global.client.Execute(Global.AddHeader(request));

            return (response.Content.Length > 0) ? false : true;
        }

        /// <summary>
        /// Send channel message.
        /// </summary>
        /// <returns>The message sent.</returns>
        public Message SendMessage(long channel_id, string message = "", bool tts = false)
        {
            var request = new RestRequest($"https://discord.com/api/v9/channels/{channel_id}/messages", Method.Post);

            request.AddJsonBody(new { content = message, tts = tts });
            var response = Global.client.Execute(Global.AddHeader(request));
            JObject json = JObject.Parse(response.Content.Replace("\"", "'"));

            Message msg_callback = Global.ConvertJsonToMessage(json);
            return msg_callback;
        }

        /// <summary>
        /// Change username nickname by id.
        /// </summary>
        /// <remarks>
        /// Need server permissions.
        /// </remarks>
        public void ChangeNick(long user_id, long server_id, string nickname)
        {
            var request = new RestRequest($"https://discord.com/api/v9/guilds/{server_id}/members/{user_id}", Method.Patch);

            request.AddJsonBody(new { nick = nickname });
            Global.client.Execute(Global.AddHeader(request));
        }

        /// <summary>
        /// Mute or Deaf an user by id.
        /// </summary>
        /// <remarks>
        /// Need server permissions.
        /// </remarks>
        public void FullMute(long user_id, long server_id, bool mute = true, bool deaf = true)
        {
            var request = new RestRequest($"https://discord.com/api/v9/guilds/{server_id}/members/{user_id}", Method.Patch);

            request.AddJsonBody(new { mute = mute, deaf = deaf });
            Global.client.Execute(Global.AddHeader(request));
        }

        /// <summary>
        /// Moves the user to a channel.
        /// </summary>
        /// <remarks>
        /// Need server permissions.
        /// </remarks>
        public void MoveToChannel(long user_id, long server_id, long channel_id)
        {
            var request = new RestRequest($"https://discord.com/api/v9/guilds/{server_id}/members/{user_id}", Method.Patch);

            request.AddJsonBody(new { channel_id = channel_id });
            Global.client.Execute(Global.AddHeader(request));
        }

        /// <summary>
        /// Moves the user x number of times.
        /// </summary>
        /// <remarks>
        /// Need server permissions.
        /// </remarks>
        public void Annoy(long channel_1, long channel_2, long server_id, long client_id , int movemments = 10, int sleep_time = 350)
        {
            for (int i = 0; i < movemments; i++)
            {
                System.Threading.Thread.Sleep(sleep_time);
                var request = new RestRequest($"https://discord.com/api/v9/guilds/{server_id}/members/{client_id}", Method.Patch);

                if (i % 2 == 0)
                    request.AddJsonBody(new { channel_id = channel_1 });
                else
                    request.AddJsonBody(new { channel_id = channel_2 });

                Global.client.Execute(Global.AddHeader(request));
            }
        }
    }
}
