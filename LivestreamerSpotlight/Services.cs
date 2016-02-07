using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;

namespace LivestreamerSpotlight
{
    class Service
    {
        public async Task<string> TwitchTV(string _streamName, string _quality)
        {
            if (_quality == "source")
            {
                _quality = "chunked";
            }

            try
            {
                var httpClient = new HttpClient();
                dynamic accessToken = JsonConvert.DeserializeObject(await httpClient.GetStringAsync("http://api.twitch.tv/api/channels/" + _streamName.ToLower() + "/access_token"));
                string playlistUrl = "http://usher.twitch.tv/api/channel/hls/{0}.m3u8?player=twitchweb&token={1}&sig={2}&allow_audio_only=true&allow_source=true&type=any&p=1337";
                string playlistFile = await httpClient.GetStringAsync(string.Format(playlistUrl, _streamName.ToLower(), accessToken.token, accessToken.sig));

                string streamUrl = "";
                using (StringReader sr = new StringReader(playlistFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("fmt=" + _quality))
                        {
                            streamUrl = line;
                        }
                    }
                }

                return streamUrl;
            }

            catch (Exception ex)
            {
                // !!Returning null here is probably not a good solution, but it works for now!!
                return null;
                throw ex;
            }
        }
    }
}
