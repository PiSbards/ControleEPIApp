﻿using ControleEPIApp.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControleEPIApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PagePrincipal());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
