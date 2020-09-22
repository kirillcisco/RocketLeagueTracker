using Common.Models.Search;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common.Search
{
    public class RlTracker
    {
        private readonly string SearchUrl = @"https://api.tracker.gg/api/v2/rocket-league/standard/search?";
        public ObservableCollection<SearchData> Users = new ObservableCollection<SearchData>();

        public async Task<ObservableCollection<SearchData>> Search(string username, Platform type)
        {
            Users.Clear();

            var typeString = GetPlatformString(type);

            bool isId = long.TryParse(username, out long longPlaceholder);
            var uri = new Uri($"{SearchUrl}platform={typeString}&query={username}{(isId ? "" : "&autocomplete=true")}");

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error searching for Platform: {type} User: {username}");

                var responseText = await response.Content.ReadAsStringAsync();
                var foundUsers = JsonConvert.DeserializeObject<SearchResponse>(responseText);

                if (foundUsers.Data.Count == 0)
                {
                    uri = new Uri($"{SearchUrl}platform={typeString}&query={username}{(!isId ? "" : "&autocomplete=true")}");
                    response = await client.GetAsync(uri);

                    if (!response.IsSuccessStatusCode)
                        return Users;

                    responseText = await response.Content.ReadAsStringAsync();
                    foundUsers = JsonConvert.DeserializeObject<SearchResponse>(responseText);
                }

                foreach (var user in foundUsers.Data)
                {
                    Users.Add(user);

                    //Fire off a background task to pull the users info.  This will allow them to show in the UI, and then stats updated async
#pragma warning disable CS4014
                    Task.Run(async () =>
                    {
                        var fullInfo = await Get(long.Parse(user.PlatformUserIdentifier), GetPlatform(user.PlatformSlug));

                        user.QuickDetails = new QuickDetails()
                        {
                            ThreesMmr = fullInfo.Segments?.FirstOrDefault(x => x.Attributes.PlaylistId == 13)?.Stats?.Rating?.Value?.ToString(),
                            TwosMmr = fullInfo.Segments?.FirstOrDefault(x => x.Attributes.PlaylistId == 11)?.Stats?.Rating?.Value?.ToString()
                        };

                        user.Data = fullInfo;
                    });
#pragma warning restore CS4014

                }

                return Users;
            }
        }

        /// <summary>
        /// Get the stats for a single user
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<UserData> Get(long? userid, Platform type)
        {
            var plat = GetPlatformString(type);
            var uri = new Uri($"https://api.tracker.gg/api/v2/rocket-league/standard/profile/{plat}/{userid.ToString()}");

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error finding user for Platform: {type} User: {userid.ToString()}");

                var responseText = await response.Content.ReadAsStringAsync();
                var userResponse = JsonConvert.DeserializeObject<Response>(responseText);

                return userResponse.Data;
            }
        }

        /// <summary>
        /// Get the text represnetation for the Enum.  These are the values that RlTracker recognize.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetPlatformString(Platform type)
        {
            switch (type)
            {
                case Platform.Pc:
                    return "steam";
                case Platform.Xbox:
                    return "xbl";
                case Platform.Psn:
                    return "psn";
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// Get the Platform enum from the given string
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Platform GetPlatform(string type)
        {
            var low = type.ToLower();

            switch (low)
            {
                case "xbl":
                    return Platform.Xbox;
                case "xbox":
                    return Platform.Xbox;
                case "ps":
                    return Platform.Psn;
                case "psn":
                    return Platform.Psn;
                default:
                    return Platform.Pc;
            }
        }
    }
}

