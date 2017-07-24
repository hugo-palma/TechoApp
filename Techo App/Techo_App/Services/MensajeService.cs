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
    class MensajeService
    {
        public async Task<Object> PostNuevoMensajeAsync(int fromId, int[] toId, string texto)
        {
            MensajePost mensajePost  = new MensajePost();
            RestClient<MensajePost> restClient = new RestClient<MensajePost>("nuevoMensaje");
            mensajePost.idFrom = fromId;
            mensajePost.idTo = toId[0];
            mensajePost.message = texto;
            string jsonResult = await restClient.PostAsync(mensajePost);
            string status = (string)JObject.Parse(jsonResult)["status"];
            if (status == "sent")
            {
                return "sent";
            }
            return "unsuccessful";

        }
    }
}
