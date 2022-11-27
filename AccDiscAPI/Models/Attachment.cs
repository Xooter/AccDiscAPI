﻿using System;
using System.IO;
using static AccDiscAPI.Models.Global;

namespace AccDiscAPI.Models
{
    public class Attachment
    {
        public long id;
        public string content_type;
        public string filename;
        public int height;
        public int width;
        public string proxy_url;
        public int size;
        public string url;

        /// <summary>
        /// Save Guild avatar asynchronously
        /// </summary>
        public async void Save(string file_name = "", string path = "")
        {
            file_name = (file_name == "") ? $"attachment_{this.filename.Replace(" ", "_")}" : file_name;

            http_client.DefaultRequestHeaders.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            http_client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate");
            try
            {
                byte[] data = await http_client.GetByteArrayAsync($"{this.proxy_url}");

                File.WriteAllBytes(!string.IsNullOrEmpty(path) ? path + "/" : path + $"{file_name}.{proxy_url.Split('.')[proxy_url.Split('.').Length - 1]}", data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
