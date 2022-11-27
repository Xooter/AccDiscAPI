using RestSharp;
using System.Diagnostics;

namespace AccDiscAPI.Models
{
    public class Invite
    {
        public string code;
        public int uses;
        public string expires_at;
        public string created_at;
        public User inviter;
        public bool temporary;
        public int max_age;
        public int max_uses;

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
