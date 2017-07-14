using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Techo_App.RestClient
{
    public class RestClient<T>
    {

        private string WebServiceUrl;
        public RestClient(string opcion)
        {
            switch(opcion)
            {
                case "users":
                    WebServiceUrl = "http://www.palmapplicationsv.com/techoapp/public/api/register";
                    break;
                case "events":
                    WebServiceUrl =  "http://www.palmapplicationsv.com/techoapp/public/api/events";
                    break;
                case "login":
                    WebServiceUrl = "http://www.palmapplicationsv.com/techoapp/public/api/login";
                    break;
                case "userEvents":
                    WebServiceUrl = "http://www.palmapplicationsv.com/techoapp/public/api/userevents/add";
                    break;
            }
        }
        public RestClient(string opcion, string opcional)
        {
            switch (opcion)
            {
                case "eventsByAssistance":
                    WebServiceUrl = "http://www.palmapplicationsv.com/techoapp/public/api/eventsbyassistance/" + opcional;
                    break;
            }
        }
        public RestClient(string opcion, string opcional1, string opcional2)
        {
            switch (opcion)
            {
                case "attendants":
                    WebServiceUrl = "http://www.palmapplicationsv.com/techoapp/public/api/eventsattendees/" + opcional1 + "/" + opcional2;
                    break;
            }
        }
        public async Task<List<T>> GetAsync()
        {
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(WebServiceUrl);

            Debug.WriteLine(json);

            var taskModels = JsonConvert.DeserializeObject<List<T>>(json);

            return taskModels;
        }
        public async Task<string> PostAsync(T t)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(t);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(WebServiceUrl, httpContent);

            Debug.WriteLine(result.Content.ReadAsStringAsync().Result);

            var jsonResult = result.Content.ReadAsStringAsync().Result;

            return jsonResult;
        }
    }
}
