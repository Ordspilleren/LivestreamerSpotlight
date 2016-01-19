using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;

namespace LivestreamerSpotlight
{
    class Services
    {
        public static async Task<string> TwitchTV(string streamName, string quality)
        {
            if (quality == "source")
            {
                quality = "chunked";
            }

            var httpClient = new HttpClient();
            dynamic accessToken = JsonConvert.DeserializeObject(await httpClient.GetStringAsync("http://api.twitch.tv/api/channels/" + streamName.ToLower() + "/access_token"));
            string playlistUrl = "http://usher.twitch.tv/api/channel/hls/{0}.m3u8?player=twitchweb&token={1}&sig={2}&allow_audio_only=true&allow_source=true&type=any&p=1337";
            string playlistFile = await httpClient.GetStringAsync(string.Format(playlistUrl, streamName.ToLower(), accessToken.token, accessToken.sig));

            string streamUrl = "";
            using (StringReader sr = new StringReader(playlistFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("fmt=" + quality))
                    {
                        streamUrl = line;
                    }
                }
            }

            return streamUrl;
        }
    }
}
