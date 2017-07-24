using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Techo_App.Services;
using Techo_App.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
namespace Techo_App.ViewModels
{
    class AttendantsViewModel : INotifyPropertyChanged
    {
        private INavigation Navigation;
        private Evento evento;
        private ObservableCollection<Voluntario> _voluntariosCollection;
        public ObservableCollection<Voluntario> voluntariosCollection {
            get { return _voluntariosCollection; }
            set {
                _voluntariosCollection = value;
                OnPropertyChanged();
            } }
        public AttendantsViewModel(INavigation Navigation, Evento evento)
        {
            this.Navigation = Navigation;
            this.evento = evento;
            InitializeDataAsync();
        }
        private SesionService sesionService;
        private FriendsService friendsService;
        private async Task InitializeDataAsync()
        {
            sesionService = new SesionService();
            friendsService = new FriendsService();
            int idUsuario = await sesionService.GetSesionIdUserDbAsync();
            var listaTemp = await friendsService.GetUsersByEventAsync(idUsuario, evento.idEventos);

            voluntariosCollection = new ObservableCollection<Voluntario>();
            ListView listView = new ListView();
            /*listView.RowHeight = 130;
            ListViewBehaviorNoSelected lBNS = new ListViewBehaviorNoSelected();
            listView.Behaviors.Add(lBNS);
            listView.ItemTemplate = new DataTemplate(typeof(CustomAttendantsCell));*/
            foreach (var voluntario in listaTemp)
            {
                if(voluntario.amigos == 0)
                {
                    voluntario.textoBoton = "Solicitud Pendiente";
                }
                else if(voluntario.amigos == 1)
                {
                    voluntario.textoBoton = "Ver";
                }
                else if(voluntario.amigos == 2)
                {
                    voluntario.textoBoton = "Enviar Solicitud";
                }

                if(voluntario.foto == null)
                {
                    voluntario.foto = "photo.png";
                }
                else
                {
                    if (voluntario.foto.Contains("https:"))
                    {
                        
                    }
                    else
                    {
                        voluntario.foto = "http://www.palmapplicationsv.com/techoapp/public/" + voluntario.foto;
                    }
                }
                voluntariosCollection.Add(voluntario);
            }

        }

        public Command UsuarioPickedCommand
        {
            get
            {
                return new Command(async (voluntario) =>
                {
                    var voluntarioSeleccionado = voluntario as Voluntario;
                    Debug.WriteLine(voluntarioSeleccionado.nombre);
                    if(voluntarioSeleccionado.amigos == 2)
                    {
                        friendsService = new FriendsService();
                        var result = (string) await friendsService.PostFrienshipRequest(await sesionService.GetSesionIdUserDbAsync(), voluntarioSeleccionado.idUsuarios);
                        if (result == "successful")
                        {
                            int numero = voluntariosCollection.IndexOf(voluntarioSeleccionado);
                            voluntarioSeleccionado.textoBoton = "ya";
                            voluntariosCollection[numero] = voluntarioSeleccionado;
                            OnPropertyChanged();
                        }
                    }
                   
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
