
using Xamarin.Forms;
using Techo_App.CustomCells;
using Techo_App.Models;

namespace Techo_App
{
    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public MyDataTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as Mensaje;
            if (messageVm == null)
                return null;
            return messageVm.recibido ? this.incomingDataTemplate : this.outgoingDataTemplate;
        }

        private readonly DataTemplate incomingDataTemplate;
        private readonly DataTemplate outgoingDataTemplate;

    }
}
