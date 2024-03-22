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
    public partial class PageUpDel : ContentPage
    {
        public Funcionario funcionario;
        public PageUpDel()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = this.funcionario;
        }

        private void btnExcluir_Clicked(object sender, EventArgs e)
        {
            if (funcionario.id != 0)
            {
                MySQLCon.ExcluirFuncionario(funcionario);
                DisplayAlert("Exclusão", "Funcionário excluida com sucesso!", "OK");
                Navigation.PopAsync();
            }
        }

        private void btnAtualizar_Clicked(object sender, EventArgs e)
        {
                funcionario.data_vencimento = DateTime.Today.AddDays(Double.Parse(txtValidade.Text));
                MySQLCon.AtualizarFuncionario(funcionario);
                DisplayAlert("Edição", "Funcionário atualizada com sucesso!", "OK");
                Navigation.PopAsync();
        }
    }
}