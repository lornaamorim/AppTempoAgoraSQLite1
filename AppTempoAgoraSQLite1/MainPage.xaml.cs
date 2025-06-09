using System.Collections.ObjectModel;
using AppTempoAgoraSQLite1.Models;
using AppTempoAgoraSQLite1.Services;

namespace AppTempoAgoraSQLite1
{
    public partial class MainPage : ContentPage
    {

        ObservableCollection<Tempo> lista = new ObservableCollection<Tempo>();
        public object lstPrevisoes;
        public object txt_cidade;
        private object lbl_res;

        public MainPage()
        {
            InitializeComponent();
            lstPrevisoes.ItemsSource = lista;
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        protected async override void OnAppearing()
        {
            try
            {
                lista.Clear();

                List<Tempo> tmp = await App.Db.GetAll();

                tmp.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }


        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem selecinado = sender as MenuItem;
                Tempo t = selecinado.BindingContext as Tempo;

                bool confirm = await DisplayAlert(
                    "Tem Certeza?", $"Remover previsão para {t.Cidade}?", "Sim", "Não");

                if (confirm)
                {
                    await App.Db.Delete(t.Id);
                    lista.Remove(t);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        t.Cidade = txt_cidade.Text;
                        t.DataConsulta = DateTime.Now;

                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Por do Sol: {t.sunset} \n" +
                                         $"Temp Máx: {t.temp_max} \n" +
                                         $"Temp Min: {t.temp_min} \n";


                        string mapa = $"https://embed.windy.com/embed.html?" +
                                      $"type=map&location=coordinates&metricRain=mm&metricTemp=°C" +
                                      $"&metricWind=km/h&zoom=5&overlay=wind&product=ecmwf&level=surface" +
                                      $"&lat={t.lat.ToString().Replace(",", ".")}&lon={t.lon.ToString().Replace(",", ".")}";



                        await App.Db.Insert(t);

                        lista.Clear();
                        App.Db.GetAll().Result.ForEach(i => lista.Add(i));
                    }
                    else
                    {

                        lbl_res.Text = "Sem dados de Previsão";
                    }

                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private void lstPrevisoes_Refreshing(object sender, EventArgs e)
        {

        }

        private void lstPrevisoes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                Tempo t = e.SelectedItem as Tempo;


            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string q = e.NewTextValue;

                lista.Clear();

                List<Tempo> tmp = await App.Db.Search(q);

                tmp.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }

        }
    }


}
