using ControleEPIApp.Controller;
using ControleEPIApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControleEPIApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListarEPI : ContentPage
    {
        public PageListarEPI()
        {
            InitializeComponent();
        }

        void NavegarEPI(Funcionario func)
        {
            PageUpDel upDel = new PageUpDel();
            upDel.funcionario = func;
            Navigation.PushAsync(upDel);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            lsvEPI.ItemsSource = MySQLCon.ListarEPI();
            lsvEPIVencida.ItemsSource = MySQLCon.ListarEPIVencida();
        }

        private void lsvEPI_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
                NavegarEPI(e.SelectedItem as Funcionario);
        }

        private void lsvEPIVencida_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
                NavegarEPI(e.SelectedItem as Funcionario);
        }
    }
}