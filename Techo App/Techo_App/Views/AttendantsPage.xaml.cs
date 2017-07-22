﻿
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Techo_App.ViewModels;
using Techo_App.Models;

namespace Techo_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendantsPage : ContentPage
    {
        public AttendantsPage(Evento evento)
        {
            InitializeComponent();
            AttendantsViewModel attendantsViewModel = new AttendantsViewModel(Navigation, evento);
            BindingContext = attendantsViewModel;
        }
    }
}