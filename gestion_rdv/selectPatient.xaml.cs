using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace gestion_rdv
{
    /// <summary>
    /// Interaction logic for selectPatient.xaml
    /// </summary>
    public partial class selectPatient : Window
    {
        private List<dataOfPatient> listPatients { get; set; }
        private List<int> existingItems { get; set; }
        private static dataOfPatient patient { get; set; }
        private static selectPatient selectpatient { get; set; }
        public selectPatient()
        {
            InitializeComponent();
            patientsDataGrid.MaxHeight = this.MaxHeight - 110;
            patientsDataGrid.MaxWidth = this.MaxWidth - 15;
            existingItems = new List<int>();
            loadPatients();
        }

        public static dataOfPatient Show()
        {
            selectpatient = new selectPatient();
            selectpatient.ShowDialog();
            return patient;
        }

        private void loadPatients()
        {
            listPatients = Patient.getListOfPatients();
            if (patientsDataGrid.Columns.Count == 0)
            {
                DataGridTextColumn c0 = new DataGridTextColumn(); c0.Binding = new Binding("nom"); c0.Header = "NOM"; c0.MinWidth = 160;
                DataGridTextColumn c1 = new DataGridTextColumn(); c1.Binding = new Binding("prenom"); c1.Header = "PRENOM"; c1.MinWidth = 160;
                DataGridTextColumn c2 = new DataGridTextColumn(); c2.Binding = new Binding("date"); c2.Header = "DATE DE NAISSANCE";
                DataGridTextColumn c3 = new DataGridTextColumn(); c3.Binding = new Binding("num"); c3.Header = "TELEPHONE"; c3.MinWidth = 120;
                DataGridTextColumn c4 = new DataGridTextColumn(); c4.Binding = new Binding("sexe"); c4.Header = "SEXE";              
                c2.ClipboardContentBinding.StringFormat = "d";
                patientsDataGrid.Columns.Add(c0); patientsDataGrid.Columns.Add(c1); patientsDataGrid.Columns.Add(c2);
                patientsDataGrid.Columns.Add(c3); patientsDataGrid.Columns.Add(c4);
            }
            foreach (dataOfPatient pat in listPatients)
            {
                if (!existingItems.Contains(pat.idpat))
                {
                    patientsDataGrid.Items.Add(pat);
                    existingItems.Add(pat.idpat);
                }
            }
        }
        private void nomPatGrid_KeyUp(object sender, KeyEventArgs e)
        {
            List<dataOfPatient> list = new List<dataOfPatient>();
            foreach (dataOfPatient patient in listPatients)
            {
                if (patient.nom.ToLower().StartsWith(nomPatGrid.Text.ToLower().Trim()) &&
                    patient.prenom.ToLower().StartsWith(prenomPatGrid.Text.ToLower().Trim()))
                    list.Add(patient);
            }
            patientsDataGrid.Items.Clear();
            foreach (dataOfPatient patient in list) patientsDataGrid.Items.Add(patient);
        }

        private void patientsDataGrid_KeyUp(object sender, KeyEventArgs e)
        {
            patientsDataGrid.SelectedIndex = -1;
        }

        private void _MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            patient = (dataOfPatient)patientsDataGrid.SelectedItem;
            this.Close();
        }

        private void ouvrirButton_Click(object sender, RoutedEventArgs e)
        {
            if (patientsDataGrid.SelectedItem != null)
                _MouseDoubleClick(sender, new MouseButtonEventArgs(Mouse.PrimaryDevice, new TimeSpan(DateTime.Now.Ticks).Seconds,
                MouseButton.Middle));
        }

    }
}
