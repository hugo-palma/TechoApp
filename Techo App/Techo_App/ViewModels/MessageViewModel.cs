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
using Techo_App.Services;

namespace Techo_App.ViewModels
{
    public class MessageViewModel : INotifyPropertyChanged
    {
        private int idGrupo;
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

        public MessageViewModel(ContentPage contentPage, int idGrupo)
        {
            this.contentPage = contentPage;
            this.idGrupo = idGrupo;
            listaMensajes = new ObservableCollection<Mensaje>
            {
                //new Mensaje { mensaje= "hola mundo", recibido = true, foto="photo.png"},
                //new Mensaje { mensaje= "hola tu", recibido = false, foto="photo.png"}
            };

        }
        private string outgoingText;

        public string OutGoingText
        {
            get { return outgoingText; }
            set { outgoingText = value; OnPropertyChanged(); }
        }
        public Command enviarCommand
        {
            get
            {
                return new Command(async () =>
                {
                    MensajeService mensajeService = new MensajeService();
                    SesionService sesionService = new SesionService();
                    int[] ids = new int[1];
                    ids[0] = idGrupo;
                    int sesionId = await sesionService.GetSesionIdUserDbAsync();
                    await mensajeService.PostNuevoMensajeAsync(sesionId, ids, OutGoingText);
                    listaMensajes.Add(new Mensaje { mensaje = OutGoingText, recibido = false, fechaCreacion = DateTime.Now.ToString() });
                    OutGoingText = null;
                });

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
