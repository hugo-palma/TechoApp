using System.Collections.Generic;
using System.Threading.Tasks;
using Techo_App.Models;
using Techo_App.RestClient;

namespace Techo_App.Services
{
    class ChecklistServices
    {
        public async Task<List<Check>> GetChecklistAsync(int idEvento)
        {
            RestClient<Check> restClient = new RestClient<Check>("checklist", idEvento.ToString());
            var checklist = await restClient.GetAsync();
            return checklist;
        }
    }
}
