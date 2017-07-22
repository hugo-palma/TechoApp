using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Techo_App.Models;

namespace Techo_App.ViewModels
{
    public class MessageViewModel : INotifyPropertyChanged
    {
        private ContentPage contentPage;
        private string _title;
        public string title
        {
            get { return _title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }
        private bool _isBusy;
        public bool isBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Mensaje> _listaMensajes;
        public ObservableCollection<Mensaje> listaMensajes
        {
            get { return _listaMensajes; }
            set {
                _listaMensajes = value;
                OnPropertyChanged();
            }
        }

        public MessageViewModel(ContentPage contentPage)
        {
            this.contentPage = contentPage;
            listaMensajes = new ObservableCollection<Mensaje>
            {
                new Mensaje { mensaje= "hola mundo", recibido = true, foto="photo.png"},
                new Mensaje { mensaje= "hola tu", recibido = false, foto="photo.png"}
            };
           /* StackLayout SL = new StackLayout();
            RelativeLayout relativeLayout = new RelativeLayout();
            ListView mensajesListView = new ListView();
            mensajesListView.ItemTemplate = new DataTemplate(typeof(MyDataTemplateSelector));
            mensajesListView.ItemsSource = listaMensajes;
            mensajesListView.HasUnevenRows = true;
            mensajesListView.SeparatorVisibility = SeparatorVisibility.None;
            /*relativeLayout.Children.Add(mensajesListView, Constraint.RelativeToParent((parent) =>
            {
                return (1 * parent.Height);
            }));
            Grid mensajesControls = new Grid();
            mensajesControls.BackgroundColor = Color.FromHex("EFEFF4");
            mensajesControls.VerticalOptions = LayoutOptions.FillAndExpand;
            mensajesControls.VerticalOptions = LayoutOptions.FillAndExpand;
            mensajesControls.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            mensajesControls.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            Entry entry = new Entry();
            entry.HeightRequest = 25;
            entry.Placeholder = "Mensaje";
            //entry.SetBinding(Entry.TextProperty, "OutgoingText");
            mensajesControls.Children.Add(entry, 0, 1);
            Button button = new Button();
            button.Text = "Enviar";
            //button.SetBinding(Button.CommandProperty, "SendCommand");
            mensajesControls.Children.Add(button, 0, 1);

            /*relativeLayout.Children.Add(mensajesControls, Constraint.RelativeToView(mensajesListView, (Parent, sibling) =>
            {
                return Parent.Height * 0.91;
            }),
            Constraint.RelativeToParent((Parent) =>
            {
                return Parent.Width;
            }),
            Constraint.RelativeToView(mensajesListView, (Parent, sibling) =>
            {
                return Parent.Height * 0.09;
            }));
            SL.Children.Add(mensajesListView);
            contentPage.Content = SL;
            */

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
