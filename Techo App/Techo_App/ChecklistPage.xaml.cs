﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Techo_App.ViewModels;
using Techo_App.Models;

namespace Techo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChecklistPage : ContentPage
    {
        ChecklistViewModel checklistViewModel;
        public ChecklistPage(Evento evento)
        {
            InitializeComponent();
            checklistViewModel = new ChecklistViewModel(evento);
            BindingContext = checklistViewModel;
        }
    }
}