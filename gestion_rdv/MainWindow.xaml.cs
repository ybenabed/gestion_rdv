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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace gestion_rdv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Selection Variables 
        private Grid currentGrid { get; set; }
        #endregion

        #region Patient Rendez-vous Show Variables
        private List<dataofRdv> appList { get; set; }
        private dataOfPatient selectedPatient { get; set; }
        private bool alreadyBuilt { get;set; }
        #endregion

        #region PatientShow Variables
        private List<dataOfPatient> listPatients { get; set; }
        private List<int> existingItems { get; set; }
        #endregion


        #region RendezVous Variables
        private DateTime date { get; set; }
        private List<Border> list { get; set; }
        private Border currentSelected { get; set; }
        private Doctor doctor { get; set; }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            //Pour se connecter, parce qu'on va séparer les rendez-vous par docteur, on va créer un table 
            //pour le médcin, et si dans le futur il veut que ajouter d'autres utilisateurs, ça sera 
            //facielment extensible 
            Exception ex;
            doctor = new Doctor("username", "password");
            doctor.accountConnect(out ex);
            //----------------------------------------
            currentGrid = rendezvousGrid;
            currentGrid.Visibility = Visibility.Visible;
            patientGrid.Visibility = newPatientGrid.Visibility = Visibility.Hidden;
            //
            alreadyBuilt = false;
            existingItems = new List<int>();
            list = new List<Border>();
            date = DateTime.Now;
            foreach (Border border in list) rendezvousGrid.Children.Remove(border);
            if (rendezvousGrid.RowDefinitions.Count > 2)
                rendezvousGrid.RowDefinitions.RemoveRange(2, rendezvousGrid.RowDefinitions.Count - 2);
            list.Clear();
            if (yearCombo.Items.Count == 0) for (int year = 2018; year < 3000; year++) yearCombo.Items.Add(year.ToString());
            currentSelected = new Border();
            showMonth(new DateTime(date.Year, date.Month, 1));
            addTemplate.Tag = "special";
        }


        #region Selection Functions 
        private void buttonRdv_Click(object sender, RoutedEventArgs e)
        {
            if (currentGrid != rendezvousGrid)
            {
                currentGrid.Visibility = Visibility.Hidden;
                rendezvousGrid.Visibility = Visibility.Visible;
                currentGrid = rendezvousGrid;
                list = new List<Border>();
                date = DateTime.Now;
                foreach (Border border in list) rendezvousGrid.Children.Remove(border);
                if (rendezvousGrid.RowDefinitions.Count > 2)
                    rendezvousGrid.RowDefinitions.RemoveRange(2, rendezvousGrid.RowDefinitions.Count - 2);
                list.Clear();
                if (yearCombo.Items.Count == 0) for (int year = 2010; year < 3000; year++) yearCombo.Items.Add(year.ToString());
                currentSelected = new Border();
                showMonth(new DateTime(date.Year, date.Month, 1));
            }
        }

        private void buttonAddPatient_Click(object sender, RoutedEventArgs e)
        {
            if (currentGrid != newPatientGrid)
            {
                currentGrid.Visibility = Visibility.Hidden;
                newPatientGrid.Visibility = Visibility.Visible;
                currentGrid = newPatientGrid;
            }
        }

        private void buttonPatient_Click(object sender, RoutedEventArgs e)
        {
            if (currentGrid != patientGrid)
            {
                currentGrid.Visibility = Visibility.Hidden;
                patientGrid.Visibility = Visibility.Visible;
                currentGrid = patientGrid;
                loadPatients();
            }
        }
        #endregion 

        #region Rendez-vous Region
        private void showMonth(DateTime mounth)
        {
            date = new DateTime(mounth.Year, mounth.Month, 1);
            Mounth.Text = translate(date.Month);
            yearLabel.Content = date.Year.ToString();
            int lastDay = date.AddMonths(1).AddDays(-1).Day;
            int dayofWeek = (int)date.DayOfWeek, day = 1;
            Dictionary<DateTime, List<dataofRdv>> appointementMap = Rendez_Vous.appointementIn(date, doctor.ID());            
            for (int i = 2; day <= lastDay; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = new GridLength(1, GridUnitType.Star);
                rendezvousGrid.RowDefinitions.Add(rowDef);
                for (int j = dayofWeek; j < 7; j++)
                {
                    if (day > lastDay) break;
                    DateTime current = new DateTime(mounth.Year, mounth.Month, day);
                    List<dataofRdv> listt = new List<dataofRdv>();
                    bool have = appointementMap.TryGetValue(current, out listt);
                    //Create 
                    Border bord;
                    if (listt != null) bord = create("Day" + day, day, have, listt);
                    else bord = create("Day" + day, day, have, new List<dataofRdv>());
                    //If this day is today 
                    if (current == (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)))
                    {
                        bord.Background = (Brush)(new BrushConverter()).ConvertFromString("#FFBAE0EE");
                        StackPanel stackpanel = (StackPanel)bord.Child;
                        Grid grid = (Grid)stackpanel.Children[4];
                        grid.Background = (Brush)(new BrushConverter()).ConvertFromString("#FFBAE0EE");
                    }
                    rendezvousGrid.Children.Add(bord);
                    Grid.SetRow(bord, i);
                    Grid.SetColumn(bord, j);
                    list.Add(bord);
                    day++;
                }
                dayofWeek = 0;
            }
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
        private void ClickEvent(object sender, MouseButtonEventArgs e)
        {
            if (currentSelected != sender as Border) //One Click
            {
                deselection_process(sender as Border);
            }
            else //Double Click //Show Appointements
            {
                List<dataofRdv> listrdv = (List<dataofRdv>)(sender as Border).Tag;
                (new showRdv(listrdv, doctor, new DateTime(date.Year, date.Month, int.Parse((sender as Border).Uid)))).Show();
            }
        }
        private void deselection_process(Border newElement)
        {
            newElement.Focus();
            newElement.BorderThickness = new Thickness(4);
            currentSelected.BorderThickness = new Thickness(1);
            currentSelected = newElement;
        }
        private Border create(String Name, int day, bool visibility, List<dataofRdv> rdvlist)
        {
            //Creating the border 
            Border border = new Border();
            border.Tag = rdvlist;
            border.Style = cellModel.Style;
            border.BorderBrush = cellModel.BorderBrush;
            border.BorderThickness = cellModel.BorderThickness;
            border.Name = Name;
            StackPanel stackpanel = new StackPanel();
            //Creating stackpanel's first child 
            Grid grid = new Grid { Background = Grid1.Background };
            grid.Children.Add(
                new Label
                {
                    Content = day.ToString(),
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.Black,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new Thickness(-1),
                }
                );
            //Creating stackpanel's second child
            Border borderr = new Border
            {
                Background = Border1.Background,
                CornerRadius = Border1.CornerRadius,
                Margin = new Thickness(4)
            };
            borderr.Child = new TextBlock
            {
                Text = "Vous avez " + rdvlist.Count + " rendez-vous",
                Margin = new Thickness(5)
            };
            border.Margin = new Thickness(-1);
            if (!visibility) borderr.Visibility = Visibility.Collapsed;
            //Adding children
            stackpanel.Children.Add(grid);
            stackpanel.Children.Add(new Label());
            stackpanel.Children.Add(new Label { Margin = new Thickness(0, 0, 0, -6) });
            stackpanel.Children.Add(borderr);
            stackpanel.Children.Add(new Grid { Height = 150, Background = Brushes.Transparent, Margin = new Thickness(-6) });
            border.Child = stackpanel;
            border.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            border.MouseLeftButtonUp += ClickEvent;
            border.MouseEnter += Day_MouseEnter;
            border.MouseLeave += Day_MouseLeave;
            //border.KeyDown += day_keyDown;
            border.Uid = day.ToString();
            border.Focusable = true;
            return border;
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Clear the previous month 
            foreach (Border border in list) rendezvousGrid.Children.Remove(border);
            if (rendezvousGrid.RowDefinitions.Count > 2)
                rendezvousGrid.RowDefinitions.RemoveRange(2, rendezvousGrid.RowDefinitions.Count - 2);
            list.Clear();
            //Create the new one 
            if ((sender as Image).Uid == "1") showMonth(date.AddMonths(1));
            else showMonth(date.AddMonths(-1));
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (yearCombo.SelectedIndex > -1)
            {
                //Show the new Year
                yearLabel.Content = yearCombo.SelectedItem.ToString();
                //Clear the previous month 
                foreach (Border border in list) rendezvousGrid.Children.Remove(border);
                if (rendezvousGrid.RowDefinitions.Count > 2)
                    rendezvousGrid.RowDefinitions.RemoveRange(2, rendezvousGrid.RowDefinitions.Count - 2);
                list.Clear();
                //Create the new month
                showMonth(new DateTime(int.Parse(yearCombo.SelectedItem.ToString()), date.Month, 1));
                yearCombo.SelectedIndex = -1;
            }
        }

        private void Day_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).BorderThickness = new Thickness(4);
        }
        private void Day_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender as Border != currentSelected) (sender as Border).BorderThickness = new Thickness(1);
        }

        public void appointementAdded(int appDay)
        {
            List<dataofRdv> appList = (List<dataofRdv>)list[appDay - 1].Tag;
            //Setting the  visibility 
            ((Border)((StackPanel)list[appDay - 1].Child).Children[3]).Visibility = Visibility.Visible;
            TextBlock textblock = (TextBlock)((Border)((StackPanel)list[appDay - 1].Child).Children[3]).Child;
            textblock.Text = "Vous avez " + appList.Count + " rendez-vous";
        }

        public void appointementRemoved(int appDay)
        {
            List<dataofRdv> appList = (List<dataofRdv>)list[appDay - 1].Tag;
            //Setting the  visibility 
            if (appList.Count == 0) ((Border)((StackPanel)list[appDay - 1].Child).Children[3]).Visibility = Visibility.Hidden;
            else
            {
                TextBlock textblock = (TextBlock)((Border)((StackPanel)list[appDay - 1].Child).Children[3]).Child;
                textblock.Text = "Vous avez " + appList.Count + " rendez-vous";
            }
        }
        #endregion

        #region PatientShow

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
                c2.ClipboardContentBinding.StringFormat = "dd.MM.yyyy";
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
            //nbrTotal = listPatients.Count; nbrHomme = nbrTotal - nbrFemme;
        }
        private void patientsDataGrid_KeyUp(object sender, KeyEventArgs e)
        {
            patientsDataGrid.SelectedIndex = -1;
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
        #endregion

        #region Patient Rendez-vous Show
        private void add_Click(object sender, RoutedEventArgs e)
        {
            addNewAppoCell(selectedPatient);
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
            string dayOfWeek = rdv.date.ToString("dddd", new System.Globalization.CultureInfo("fr-FR"));
            dayOfWeek = char.ToUpper(dayOfWeek[0]) + dayOfWeek.Substring(1);
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
                Content = dayOfWeek + ", le " + rdv.date.ToShortDateString() + " à " 
                    + rdv.date.ToShortTimeString(),
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
            string path = documents + @"\" + appname + @"\" + patient.nom + " " +
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
            DateTime day = appointemenetDate.SelectedDate.Value.Date;
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

        private void _MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (appList != null)
            {
                appList = new List<dataofRdv>();
                for (int i = 0; i < appointementStack.Children.Count; i++)
                {
                    Grid tmp = (Grid)appointementStack.Children[i];
                    if (tmp.Tag != "special") appointementStack.Children.RemoveAt(i);
                }
            }
            selectedPatient = (dataOfPatient)patientsDataGrid.SelectedItem;
            if (selectedPatient.num != null) loadAppointements(selectedPatient); 
        }

        private void loadAppointements(dataOfPatient patient)
        {
            List<dataofRdv> list = Rendez_Vous.getPatientAppointement(patient.idpat, doctor.ID());
            appList = list;
            patientName.Content = patient.nom.ToUpper() + " " + patient.prenom.ToUpper();
            Grid addNewGrid = (Grid)appointementStack.Children[0];
            addNewGrid.Visibility = System.Windows.Visibility.Collapsed;
            appointementStack.Children.RemoveAt(0);
            foreach (dataofRdv rdv in list) appointementStack.Children.Add(buildAppointement(rdv));
            appointementStack.Children.Add(addNewGrid);
            appointementStack.Children.Add(buildPlus());
            alreadyBuilt = true;
        }
        #endregion

        #region AddPatient
        private void nvPatientSexe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nvPatientSexe_GotFocus(sender, new RoutedEventArgs());
        }

        private void nvPatientSexe_GotFocus(object sender, RoutedEventArgs e)
        {
            nvPatientSexeWarning.Visibility = Visibility.Hidden;
        }

        private void createNewPatient_Click(object sender, RoutedEventArgs e)
        {
            if (verifyNvPatientInsertion())
            {
                Patient patient = new Patient(nvPatientName.Text, nvPatientPrenom.Text, nvPatientnumTel.Text,
                    DateTime.Parse(nvPatientdate.Text), nvPatientSexe.SelectedIndex);
                patient.Insert_Nv_Patient();
                clearPatientInsertion();
            }
        }

        private bool verifyNvPatientInsertion()
        {
            nvPatientName.Text = nvPatientName.Text.Trim(); nvPatientnumTel.Text = nvPatientnumTel.Text.Trim();
            nvPatientPrenom.Text = nvPatientPrenom.Text.Trim();
            if (nvPatientName.Text == "")
            {
                nvPatientNameWarning.Visibility = Visibility.Visible;
                return false;
            }
            if (nvPatientPrenom.Text == "")
            {
                nvPatientPrenomWarning.Visibility = Visibility.Visible;
                return false;
            }
            if (nvPatientSexe.Text == "")
            {
                nvPatientSexeWarning.Visibility = Visibility.Visible;
                return false;
            }

            if (nvPatientnumTel.Text == "")
            {
                nvPatientNumWarning.Visibility = Visibility.Visible;
                return false;
            }
            if (nvPatientdate.Text == "")
            {
                nvPatientDateWarning.Visibility = Visibility.Visible;
                return false;
            }
            return true;
        }

        private void clearPatientInsertion()
        {
            nvPatientdate.Text = ""; nvPatientName.Text = ""; nvPatientnumTel.Text = "";
            nvPatientPrenom.Text = ""; nvPatientSexe.Text = ""; nvPatientSexe.SelectedIndex = -1;
        }

        private void nvPatientnumTel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                int.Parse(e.Text);
            }
            catch (Exception ex)
            {
                e.Handled = true;
            }
        }

        private void nvPatientInput_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            switch (textbox.Name)
            {
                case "nvPatientName":
                    nvPatientNameWarning.Visibility = Visibility.Hidden;
                    break;
                case "nvPatientPrenom":
                    nvPatientPrenomWarning.Visibility = Visibility.Hidden;
                    break;
                case "nvPatientnumTel":
                    nvPatientNumWarning.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void nvPatientdate_GotFocus(object sender, RoutedEventArgs e)
        {
            nvPatientDateWarning.Visibility = Visibility.Hidden;
        }

        private void newPatientGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) createNewPatient_Click(sender, new RoutedEventArgs());
        }

        private void nvPatientInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            nvPatientInput_GotFocus(sender, new RoutedEventArgs());
        }

        private void nvPatientdate_KeyUp(object sender, KeyEventArgs e)
        {
            nvPatientdate_GotFocus(sender, new RoutedEventArgs());
        }
        #endregion 

    }
}
