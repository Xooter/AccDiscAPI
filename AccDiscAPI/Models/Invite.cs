using RestSharp;
using System.Diagnostics;

namespace AccDiscAPI.Models
{
    public class Invite
    {
        public string code { get; set; }
        public int uses { get; set; }
        public string expires_at { get; set; }
        public string created_at { get; set; }
        public User inviter { get; set; }
        public bool temporary { get; set; }
        public int max_age { get; set; }
        public int max_uses { get; set; }

        /// <summary>
        /// Delete invitation in the channel
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            var request = new RestRequest($"https://discord.com/api/v9/invites/{this.code}", Method.Delete);

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
