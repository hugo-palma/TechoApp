using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Techo_App.Models;
using Techo_App.Services;

namespace Techo_App.ViewModels
{
    class ChecklistViewModel : INotifyPropertyChanged
    {
        Evento evento;
        private ObservableCollection<Check> _coleccionCheck;
        public ObservableCollection<Check> coleccionCheck {
            get { return _coleccionCheck; }
            set
            {
                _coleccionCheck = value;
                OnPropertyChanged();
            }
        }
        ChecklistServices checklistServices;


        public ChecklistViewModel(Evento evento)
        {
            this.evento = evento;
            InitializeDataAsync(evento.idEventos);
        }
        private async Task InitializeDataAsync(int idEventos)
        {
            checklistServices = new ChecklistServices();
            
            coleccionCheck = new ObservableCollection<Check>();
            var checklist = await checklistServices.GetChecklistAsync(idEventos);
            foreach (var check in checklist)
            {
                coleccionCheck.Add(check);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
