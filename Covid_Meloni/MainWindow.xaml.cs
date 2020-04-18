using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Covid_Meloni
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource ct;
        public MainWindow()
        {
            InitializeComponent();

            btn_interrompi.IsEnabled = false;
            txt_dati.IsReadOnly = true;
        }

        private void btn_carica_Click(object sender, RoutedEventArgs e)
        {
            ct = new CancellationTokenSource();
            btn_carica.IsEnabled = false;
            btn_interrompi.IsEnabled = true;
            txt_dati.Text = "";
            lst_lista.Items.Clear();
            Task.Factory.StartNew(() => CaricaDati());
        }

        private void CaricaDati()
        {
            string path = @"dati.xml";
            XDocument xmlDoc = XDocument.Load(path);
            XElement xmldati = xmlDoc.Element("root");
            var xmldato = xmldati.Elements("row");
            Thread.Sleep(300);

            foreach (var item in xmldato )
            {
                XElement xmlData = item.Element("data");
                XElement xmlStato = item.Element("stato");
                XElement xmlRicoveratiConSintomi = item.Element("ricoverati_con_sintomi");
                XElement xmlTerapiaIntensiva = item.Element("terapia_intensiva");
                XElement xmlTotaleOspedalizzati = item.Element("totale_ospedalizzati");
                XElement xmlIsolamentoDomiciliare = item.Element("isolamento_domiciliare");
                XElement xmlTotalePositivi = item.Element("totale_positivi");
                XElement xmlVariazioneTotalePositivi = item.Element("variazione_totale_positivi");
                XElement xmlNuoviPositivi = item.Element("nuovi_positivi");
                XElement xmlDimessiGuariti = item.Element("dimessi_guariti");
                XElement xmlDeceduti = item.Element("deceduti");
                XElement xmlTotaleCasi = item.Element("totale_casi");
                XElement xmlTamponi = item.Element("tamponi");

                Dato d = new Dato();
                {
                    d.Data = Convert.ToDateTime(xmlData.Value);
                    d.Stato = xmlStato.Value;
                    d.RicoveratiConSintomi = Convert.ToInt32(xmlRicoveratiConSintomi.Value);
                    d.TerapiaIntensiva = Convert.ToInt32(xmlTerapiaIntensiva.Value);
                    d.TotaleOspedalizzati = Convert.ToInt32(xmlTotaleOspedalizzati.Value);
                    d.IsolamentoDomiciliare = Convert.ToInt32(xmlIsolamentoDomiciliare.Value);
                    d.TotalePositivi = Convert.ToInt32(xmlTotalePositivi.Value);
                    d.VariazioneTotalePositivi = Convert.ToInt32(xmlVariazioneTotalePositivi.Value);
                    d.NuoviPositivi = Convert.ToInt32(xmlNuoviPositivi.Value);
                    d.DimessiGuariti = Convert.ToInt32(xmlDimessiGuariti.Value);
                    d.Deceduti = Convert.ToInt32(xmlDeceduti.Value);
                    d.TotaleCasi = Convert.ToInt32(xmlTotaleCasi.Value);
                    d.Tamponi = Convert.ToInt32(xmlTamponi.Value);
                }
                Dispatcher.Invoke(() => lst_lista.Items.Add(d));

                if (ct.Token.IsCancellationRequested)
                {
                    break;
                }
                Thread.Sleep(300);
            }

            Dispatcher.Invoke(() =>
            {
                btn_carica.IsEnabled = true;
                btn_interrompi.IsEnabled = false;
                ct = null;
            });
        }

        private void btn_interrompi_Click(object sender, RoutedEventArgs e)
        {
            ct.Cancel();
        }

        private void lst_lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dato d = (Dato)lst_lista.SelectedItem;
            if (d != null)
            {
                txt_dati.Text = Convert.ToString(d.Data);
                txt_dati.Text += "\n";
                txt_dati.Text += "\nStato:  " + d.Stato;
                txt_dati.Text += "\nRicoverati con sintomi:  " + Convert.ToInt32(d.RicoveratiConSintomi);
                txt_dati.Text += "\nTerapia intensiva:  " + Convert.ToInt32(d.TerapiaIntensiva);
                txt_dati.Text += "\nTotale ospedalizzati:  " + Convert.ToInt32(d.TotaleOspedalizzati);
                txt_dati.Text += "\nIsolamento domiciliare:  " + Convert.ToInt32(d.IsolamentoDomiciliare);
                txt_dati.Text += "\nTotale positivi:  " + Convert.ToInt32(d.TotalePositivi);
                txt_dati.Text += "\nVariazione totale positivi:  " + Convert.ToInt32(d.VariazioneTotalePositivi);
                txt_dati.Text += "\nNuovi positivi:  " + Convert.ToInt32(d.NuoviPositivi);
                txt_dati.Text += "\nDimessi guariti:  " + Convert.ToInt32(d.DimessiGuariti);
                txt_dati.Text += "\nDeceduti:  " + Convert.ToInt32(d.Deceduti);
                txt_dati.Text += "\nTotale casi:  " + Convert.ToInt32(d.TotaleCasi);
                txt_dati.Text += "\nTamponi:  " + Convert.ToInt32(d.Tamponi);
            }
        }
    }
}
