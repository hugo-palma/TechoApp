using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techo_App
{
    using Xamarin.Forms;
    public class ListViewBehaviorNoSelected : Behavior<ListView>
    {
        protected override void OnAttachedTo(ListView listview)
        {
            listview.ItemSelected += OnItemSelected;
            base.OnAttachedTo(listview);
        }
        protected override void OnDetachingFrom(ListView listview)
        {
            listview.ItemSelected -= OnItemSelected;
            base.OnDetachingFrom(listview);
        }
        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
