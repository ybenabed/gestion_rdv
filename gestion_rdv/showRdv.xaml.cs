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
using System.IO;

namespace gestion_rdv
{
    /// <summary>
    /// Interaction logic for showRdv.xaml
    /// </summary>
    public partial class showRdv : Window
    {
        private Doctor doctor { get; set; }
        private DateTime day { get; set; }
        private List<dataofRdv> appList { get; set; }
        public showRdv(List<dataofRdv> list, Doctor doc, DateTime d)
        {
            InitializeComponent();
            appList = list; day = d;
            doctor = doc;
            date.Content = d.Day + " " + translate(d.Month) + " " + d.Year;
            Grid addNewGrid = (Grid)appointementStack.Children[0];
            addNewGrid.Visibility = System.Windows.Visibility.Collapsed;
            appointementStack.Children.RemoveAt(0);
            foreach (dataofRdv rdv in list) appointementStack.Children.Add(buildAppointement(rdv));
            appointementStack.Children.Add(addNewGrid);
            appointementStack.Children.Add(buildPlus());
            Point point = App.mainscreen.PointToScreen(Mouse.GetPosition(App.mainscreen));
            double left = point.X + 2 - this.Width, top = point.Y + 2 - this.Height;
            if (left < 0) left = 0;
            if (top < 0) top = 0;
            if (left + this.Width > System.Windows.SystemParameters.PrimaryScreenWidth) left = System.Windows.SystemParameters.PrimaryScreenWidth - this.Width;
            if (top + this.Height > System.Windows.SystemParameters.PrimaryScreenHeight) top = System.Windows.SystemParameters.PrimaryScreenHeight - this.Height;
            this.Left = left;
            this.Top = top;
        }
        private string translate(int mounth)
        {
            switch (mounth)
            {
                case 1:
                    return "Janvier";
                case 2:
                    return "Fevrier";
                case 3:
                    return "Mars";
                case 4:
                    return "Avril";
                case 5:
                    return "Mai";
                case 6:
                    return "Juin";
                case 7:
                    return "Juillet";
                case 8:
                    return "Aout";
                case 9:
                    return "Septembre";
                case 10:
                    return "Octobre";
                case 11:
                    return "Novembre";
                case 12:
                    return "Decembre";
                default:
                    return "";
            }
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void closeButton_MouseEnter(object sender, MouseEventArgs e)
        {
            BrushConverter brushConverter = new BrushConverter();
            closeButton.Background = (Brush)brushConverter.ConvertFrom("#FFD13434");

        }

        private void closeButton_MouseLeave(object sender, MouseEventArgs e)
        {
            closeButton.Background = Brushes.Transparent;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            dataOfPatient pat = selectPatient.Show();
            addNewAppoCell(pat);
        }
        private void addNewAppoCell(dataOfPatient patient)
        {
            Grid grid = (Grid)appointementStack.Children[appointementStack.Children.Count - 2];
            grid.Tag = patient;
            Label label = (Label)((StackPanel)((StackPanel)grid.Children[1]).Children[0]).Children[0];
            label.Content = patient.nom + " " + patient.prenom + " à ";
            grid.Visibility = System.Windows.Visibility.Visible;
            appointementStack.Children[appointementStack.Children.Count - 1].Visibility = System.Windows.Visibility.Collapsed;
        }
        private Grid buildPlus()
        {
            Grid grid = new Grid
            {
                Margin = new Thickness(5),
            };
            grid.MouseEnter += Rectangle_MouseEnter;
            grid.MouseEnter += Rectangle_MouseLeave;
            //Grid's First Child 
            Rectangle rectangle = new Rectangle
            {
                Stroke = Brushes.Black,
                RadiusX = 10,
                RadiusY = 10,
            };
            //Grid's second Child
            Label label = new Label
            {
                Content = addNewLabel.Content,
                FontWeight = FontWeights.Bold,
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
            };
            TextBlock textb1 = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                Width = 50,
                Height = 40,
                FontSize = 30,
                Text = "+",
                Foreground = (Brush)(new BrushConverter()).ConvertFromString("#FF84F184"),
            };
            label.Content = textb1;
            //Adding childre 
            grid.Children.Add(rectangle);
            grid.Children.Add(label);
            //grid.Children.Add(popup);
            grid.MouseEnter += Rectangle_MouseEnter;
            grid.MouseLeave += Rectangle_MouseLeave;
            grid.MouseLeftButtonUp += add_Click;
            return grid;
        }
        private Grid buildAppointement(dataofRdv rdv)
        {
            dataOfPatient patient = Patient.getPatient(rdv.patKey);
            Grid grid = new Grid
            {
                Margin = new Thickness(5),
            };
            grid.MouseEnter += Rectangle_MouseEnter;
            grid.MouseEnter += Rectangle_MouseLeave;
            //Grid's First Child 
            Rectangle rectangle = new Rectangle
            {
                Stroke = Brushes.Black,
                RadiusX = 10,
                RadiusY = 10,
            };
            //Grid's second Child
            StackPanel stackpanel = new StackPanel();
            //stackpanel's First Child 
            Label label = new Label
            {
                Content = patient.nom + " " + patient.prenom + " à " + rdv.date.ToShortTimeString(),
                FontWeight = FontWeights.Bold,
            };
            //stackpanel's second Child
            StackPanel stack = new StackPanel
            {
                Orientation = Orientation.Horizontal,
            };
            //stack's Children 
            RadioButton radio0 = new RadioButton
            {
                GroupName = "etat" + rdv.key,
                Content = appointementCase.corresponding(0).toString,
                IsChecked = (appointementCase.corresponding(0).toInt == rdv.etat.toInt),
                Uid = "0"
            };
            RadioButton radio1 = new RadioButton
            {
                GroupName = "etat" + rdv.key,
                Content = appointementCase.corresponding(1).toString,
                IsChecked = (appointementCase.corresponding(1).toInt == rdv.etat.toInt),
                Uid = "1"
            };
            RadioButton radio2 = new RadioButton
            {
                GroupName = "etat" + rdv.key,
                Content = appointementCase.corresponding(2).toString,
                IsChecked = (appointementCase.corresponding(2).toInt == rdv.etat.toInt),
                Uid = "2"
            };
            radio0.Checked += RadioButton_Checked;
            radio1.Checked += RadioButton_Checked;
            radio2.Checked += RadioButton_Checked;
            radio0.Tag = radio1.Tag = radio2.Tag = rdv;
            radio0.Background = radio1.Background = radio2.Background = (Brush)(new BrushConverter()).ConvertFromString("#FFD54A42");
            //Add children to stack 
            stack.Children.Add(new Label());
            stack.Children.Add(radio0);
            stack.Children.Add(new Label());
            stack.Children.Add(radio1);
            stack.Children.Add(new Label());
            stack.Children.Add(radio2);
            //stackpanel's third child 
            StackPanel stack2 = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
            };
            //stack2's children 
            List<object> taglist = new List<object>();
            taglist.Add(rdv); taglist.Add(patient);
            Label label0 = new Label { Cursor = Cursors.Hand };
            TextBlock textb0 = new TextBlock
            {
                TextDecorations = TextDecorations.Underline,
                Text = "Imprimer",
                Foreground = Brushes.Purple,
            };
            label0.Content = textb0;
            label0.Tag = taglist;
            label0.MouseLeftButtonUp += label0_MouseLeftButtonUp;
            Label label1 = new Label { Cursor = Cursors.Hand };
            TextBlock textb1 = new TextBlock
            {
                TextDecorations = TextDecorations.Underline,
                Text = "Modifier",
                Foreground = Brushes.Purple,
            };
            label1.Content = textb1;
            Label label2 = new Label { Cursor = Cursors.Hand };
            TextBlock textb2 = new TextBlock
            {
                TextDecorations = TextDecorations.Underline,
                Text = "Supprimer",
                Foreground = Brushes.Purple,
            };
            label2.Content = textb2;
            label2.MouseLeftButtonUp += supprimer_Rdv;
            label2.Tag = rdv;
            stack2.Children.Add(label0);
            stack2.Children.Add(label1);
            stack2.Children.Add(label2);
            //Add children to stackpanel 
            stackpanel.Children.Add(label);
            stackpanel.Children.Add(stack);
            stackpanel.Children.Add(stack2);
            //Add children to grid 
            grid.Children.Add(rectangle);
            grid.Children.Add(stackpanel);
            return grid;
        }

        void label0_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            List<object> taglist = (List<object>)(sender as Label).Tag;
            dataofRdv rendezvous = (dataofRdv)taglist[0];
            dataOfPatient patient = (dataOfPatient)taglist[1];
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string appname = System.Windows.Application.ResourceAssembly.GetName().Name;
            string path = documents + @"\" + appname + @"\" + patient.nom+" " +
                patient.prenom + " " + patient.idpat;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            Microsoft.Win32.SaveFileDialog save = new Microsoft.Win32.SaveFileDialog();            
            save.Filter = "Fichiers Text|*.txt";
            string filename = "Rdv " + rendezvous.date;
            filename = filename.Replace("/", ""); filename = filename.Replace(":", "");
            filename = filename.Replace(" ", "");
            save.FileName = filename;
            save.InitialDirectory = path;
            if (save.ShowDialog() == true)
            {
                string sav = save.FileName;
                Rendez_Vous.imprimerRendezVous(patient, rendezvous, sav);
            }   
        }
        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle rec = (Rectangle)(sender as Grid).Children[0];
            rec.Fill = (Brush)(new BrushConverter()).ConvertFromString("#FFC1EEFF");
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle rec = (Rectangle)(sender as Grid).Children[0];
            rec.Fill = Brushes.Transparent;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        //The new Appointement treatment 
        private void increment_KeyDown(object sender, KeyEventArgs e)
        {
            string name = (sender as TextBox).Name;
            switch (name)
            {
                case "minute":
                    int min;
                    if (!int.TryParse(minute.Text, out min)) min = 0;
                    if (e.Key == Key.Up || e.Key == Key.Right) minute.Text = ((min + 1) % 60).ToString("00");
                    if (e.Key == Key.Down || e.Key == Key.Left)
                    {
                        min = (min - 1) % 60;
                        if (min < 0) min += 60;
                        minute.Text = min.ToString("00");
                    }
                    break;
                case "hour":
                    int hou;
                    if (!int.TryParse(hour.Text, out hou)) hou = 0;
                    if (e.Key == Key.Up || e.Key == Key.Right) hour.Text = ((hou + 1) % 24).ToString("00");
                    if (e.Key == Key.Down || e.Key == Key.Left)
                    {
                        hou = (hou - 1) % 24;
                        if (hou < 0) hou += 24;
                        hour.Text = hou.ToString("00");
                    }
                    if (e.Key == Key.Tab) minute.Focus();
                    break;
            }
        }
        private void time_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int value;
            if (!int.TryParse(e.Text, out value)) e.Handled = true;
        }
        private void time_LostFocus(object sender, RoutedEventArgs e)
        {
            string str = (sender as TextBox).Name;
            if ((sender as TextBox).Text == "") (sender as TextBox).Text = "00";
            int val = int.Parse((sender as TextBox).Text);
            if (val < 10) (sender as TextBox).Text = val.ToString("00");
            switch (str)
            {
                case "minute":
                    if (val > 59) (sender as TextBox).Text = "59";
                    break;
                case "hour":
                    if (val > 23) (sender as TextBox).Text = "23";
                    break;
            }
        }

        private void ajouterRDV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Grid grid = (Grid)appointementStack.Children[appointementStack.Children.Count - 2];
            dataOfPatient patient = (dataOfPatient)grid.Tag;
            int hr = int.Parse(hour.Text), mn = int.Parse(minute.Text);
            DateTime appDate = new DateTime(day.Year, day.Month, day.Day, hr, mn, 0);
            AlertBoxResult result = AlertBoxResult.Positive;
            foreach (dataofRdv datrdv in appList)
                if (datrdv.date.Equals(appDate))
                {
                    result = alertBox.Show("Il existe déja un rendez-vous à " + appDate.ToShortTimeString(),
                        AlertBoxType.ConfirmationYesNo, "Enregistrer quand meme", "Ne pas enregistrer", this, true);
                    break;
                }
            if (result == AlertBoxResult.Positive)
            {
                Console.WriteLine(appDate.ToString());
                dataofRdv datardv = new dataofRdv
                {
                    docKey = doctor.ID(),
                    patKey = patient.idpat,
                    date = appDate,
                    etat = appointementCase.Valide,
                };
                Rendez_Vous rdv = new Rendez_Vous(datardv);
                rdv.Ajouter_RDV();
                description.Text = ""; hour.Text = "00"; minute.Text = "00";
                appointementStack.Children[appointementStack.Children.Count - 1].Visibility = System.Windows.Visibility.Visible;
                appointementStack.Children[appointementStack.Children.Count - 2].Visibility = System.Windows.Visibility.Collapsed;
                bool insert = false;
                for (int i = 0; i < appList.Count; i++)
                    if (appList[i].date > appDate)
                    {
                        appList.Insert(i, datardv);
                        appointementStack.Children.Insert(i, buildAppointement(datardv));
                        insert = true;
                        break;
                    }
                if (!insert)
                {
                    appList.Add(datardv);
                    appointementStack.Children.Insert(appointementStack.Children.Count - 2, buildAppointement(datardv));
                }
                App.mainscreen.appointementAdded(appDate.Day);
            }
        }

        private void supprimer_Rdv(object sender, MouseButtonEventArgs e)
        {
            AlertBoxResult result = alertBox.Show("Voulez-vous vraimenet supprimer ce rendez-vous?", AlertBoxType.ConfirmationYesNo,
                "Supprimer", "Annuler", this, true);
            if (result == AlertBoxResult.Positive)
            {
                dataofRdv rendezvous = (dataofRdv)(sender as Label).Tag;
                int index = -1;
                for (int i = 0; i < appList.Count; i++)
                    if (appList[i].key == rendezvous.key)
                    {
                        index = i;
                        appList.RemoveAt(i);
                        break;
                    }
                Rendez_Vous r = new Rendez_Vous(rendezvous);
                r.deleteAppointement();
                appointementStack.Children.RemoveAt(index);
                App.mainscreen.appointementRemoved(rendezvous.date.Day);
                r.deleteAppointement();
            }
        }

        private void annulerAjout_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            appointementStack.Children[appointementStack.Children.Count - 1].Visibility = System.Windows.Visibility.Visible;
            appointementStack.Children[appointementStack.Children.Count - 2].Visibility = System.Windows.Visibility.Collapsed;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Cursor cursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            RadioButton radio = sender as RadioButton;
            dataofRdv datardv = (dataofRdv)radio.Tag;
            Rendez_Vous rendezvous = new Rendez_Vous(datardv);
            rendezvous.updateState(int.Parse(radio.Uid));
            datardv.etat = appointementCase.corresponding(int.Parse(radio.Uid));
            for (int i = 0; i < appList.Count; i++)
                if (appList[i].key == datardv.key)
                {
                    appList[i] = datardv;
                    break;
                }
            Mouse.OverrideCursor = cursor;
        }
    }
}
