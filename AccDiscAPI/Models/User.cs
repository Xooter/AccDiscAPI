using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using static AccDiscAPI.Models.Global;

namespace AccDiscAPI.Models
{
    public class User
    {
        public long id;
        public string avatar;
        public string avatar_decoration;
        public int? discriminator;
        public long? public_flags;
        public int? flags;
        public string username;
        public string bio;
        public string accent_color;
        public string banner;
        public string banner_color;

        public List<Accounts> connected_accounts;
        public List<UserGuild> mutual_guilds;

        /// <summary>
        /// Save user avatar asynchronously
        /// </summary>
        /// <param name="file_name">The name of the output file</param>
        /// <param name="path">The output path</param>
        public async void SaveAvatar(string file_name = "", string path = "")
        {
            file_name = (file_name == "") ? $"avatar_{this.username.Replace(" ", "_")}" : file_name;

            http_client.DefaultRequestHeaders.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            http_client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate");
            try
            {
                byte[] data = await http_client.GetByteArrayAsync($"https://cdn.discordapp.com/avatars/{this.id}/{this.avatar}.gif");
                
                File.WriteAllBytes(!string.IsNullOrEmpty(path) ? path + "/" : path + $"{file_name}.gif", data);
            }
            catch (Exception ex)
            {
                byte[] data = await http_client.GetByteArrayAsync($"https://cdn.discordapp.com/avatars/{this.id}/{this.avatar}.webp");

                File.WriteAllBytes(!string.IsNullOrEmpty(path) ? path + "/" : path + $"{file_name}.jpg", data);
            }
        }

        /// <summary>
        /// Add personal note to user
        /// </summary>
        public void AddNote(string note)
        {
            var request = new RestRequest($"https://discord.com/api/v9/users/@me/notes/{this.id}", Method.Put);

            request.AddJsonBody(new { note = note });
            Global.client.Execute(Global.AddHeader(request));
        }

        /// <summary>
        /// Change username nickname.
        /// </summary>
        /// <remarks>
        /// Need server permissions.
        /// </remarks>
        public void ChangeNick(long server_id, string nickname)
        {
            var request = new RestRequest($"https://discord.com/api/v9/guilds/{server_id}/members/{this.id}", Method.Patch);

            request.AddJsonBody(new { nick = nickname });
            Global.client.Execute(Global.AddHeader(request));
        }

        /// <summary>
        /// Mute or Deaf an user.
        /// </summary>
        /// <remarks>
        /// Need server permissions.
        /// </remarks>
        public void FullMute(long server_id, bool mute = true, bool deaf = true)
        {
            var request = new RestRequest($"https://discord.com/api/v9/guilds/{server_id}/members/{this.id}", Method.Patch);

            request.AddJsonBody(new { mute = mute, deaf = deaf });
            Global.client.Execute(Global.AddHeader(request));
        }

        /// <summary>
        /// Moves the user x number of times.
        /// </summary>
        /// <remarks>
        /// Need server permissions.
        /// </remarks>
        public void Annoy(long server_id, long channel_1, long channel_2, int movemments = 10, int sleep_time = 350)
        {
            for (int i = 0; i < movemments; i++)
            {
                System.Threading.Thread.Sleep(sleep_time);
                var request = new RestRequest($"https://discord.com/api/v9/guilds/{server_id}/members/{this.id}", Method.Patch);

                if (i % 2 == 0)
                    request.AddJsonBody(new { channel_id = channel_1 });
                else
                    request.AddJsonBody(new { channel_id = channel_2 });

                Global.client.Execute(Global.AddHeader(request));
            }
        }

        /// <summary>
        /// Moves the user to a channel.
        /// </summary>
        /// <remarks>
        /// Need server permissions.
        /// </remarks>
        public void MoveToChannel(long server_id, long channel_id)
        {
            var request = new RestRequest($"https://discord.com/api/v9/guilds/{server_id}/members/{this.id}", Method.Patch);

            request.AddJsonBody(new { channel_id = channel_id });
            Global.client.Execute(Global.AddHeader(request));
        }

        /// <summary>
        /// Disconect user.
        /// </summary>
        /// <remarks>
        /// Need server permissions.
        /// </remarks>
        public void DisconnectChannel(long server_id)
        {
            var request = new RestRequest($"https://discord.com/api/v9/guilds/{server_id}/members/{this.id}", Method.Patch);

            request.AddJsonBody(new { channel_id = "" });
            Global.client.Execute(Global.AddHeader(request));
        }

    }
}
