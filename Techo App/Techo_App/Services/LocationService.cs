using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techo_App.Models;
using Techo_App.RestClient;

namespace Techo_App.Services
{
    public class LocationService
    {
        public async Task<string> PostLoacationCheckInAsync(Location location)
        {
            RestClient<Location> restClient = new RestClient<Location>("evniarLocation");
            var result = await restClient.PostAsync(location);
            string status = (string)JObject.Parse(result)["status"];
            if (status == "added")
            {
                return "successful";
            }
            else
            {
                return "unsuccessful";
            }
        }
    }
}
