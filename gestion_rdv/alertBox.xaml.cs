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
    /// Interaction logic for alertBox.xaml
    /// </summary>
    public enum AlertBoxType
    {
        ConfirmationYesNo,
        Warning
    }
    public enum AlertBoxResult
    {
        Positive,
        Negative
    }
    public partial class alertBox : Window
    {
        private List<Page> list { get; set; }
        private int Source { get; set; }

        private static AlertBoxResult YesNoResult { get; set; }
        static alertBox alertbox { get; set; }

        public alertBox()
        {
            InitializeComponent();
            //this.Width = 300;
            list = new List<Page>();
            //list.Add(App.pagenavigation);
            //list.Add(App.pageordonnance);
        }

        public static AlertBoxResult Show(string message, AlertBoxType type, string pos, string neg, Window wind, bool owner)
        {
            if (type == AlertBoxType.Warning)
            {
                alertbox = new alertBox
                {
                    messageLabel = { Text = message },
                    type0 = { Visibility = Visibility.Visible },
                    type1 = { Visibility = Visibility.Hidden },
                };
                if (owner)
                {
                    alertbox.Owner = wind;
                    alertbox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                }
                else
                {
                    if (wind != null)
                    {
                        alertbox.Owner = wind;
                        Point point = alertbox.GetMousePos(wind);
                        double left = point.X + 2 - alertbox.Width, top = point.Y + 2 - alertbox.Height;
                        if (left < 0) left = 0;
                        if (top < 0) top = 0;
                        alertbox.Left = left;
                        alertbox.Top = top;
                    }
                    else alertbox.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
                alertbox.ShowDialog();
                return AlertBoxResult.Positive;
            }
            else
            {
                alertbox = new alertBox
                {
                    messageLabel = { Text = message },
                    type0 = { Visibility = Visibility.Hidden },
                    type1 = { Visibility = Visibility.Visible },
                    yesButton = { Content = pos },
                    noButton = { Content = neg },
                };
                if (owner)
                {
                    alertbox.Owner = wind;
                    alertbox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                }
                else
                {
                    if (wind != null)
                    {
                        alertbox.Owner = wind;
                        Point point = alertbox.GetMousePos(wind);
                        double left = point.X + 2 - alertbox.Width, top = point.Y + 2 - alertbox.Height;
                        if (left < 0) left = 0;
                        if (top < 0) top = 0;
                        alertbox.Left = left;
                        alertbox.Top = top;
                    }
                    else alertbox.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
                alertbox.ShowDialog();
                return YesNoResult;
            }
        }

        public static AlertBoxResult Show(string message, AlertBoxType type, string pos, string neg, Window wind, bool owner, Point point)
        {
            if (type == AlertBoxType.Warning)
            {
                alertbox = new alertBox
                {
                    messageLabel = { Text = message },
                    type0 = { Visibility = Visibility.Visible },
                    type1 = { Visibility = Visibility.Hidden },
                };
                if (owner)
                {
                    alertbox.Owner = wind;
                    alertbox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                }
                else
                {
                    if (wind != null)
                    {
                        alertbox.Owner = wind;
                        double left = point.X + 2 - alertbox.Width, top = point.Y + 2 - alertbox.Height;
                        if (left < 0) left = 0;
                        if (top < 0) top = 0;
                        alertbox.Left = left;
                        alertbox.Top = top;
                    }
                    else alertbox.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
                alertbox.ShowDialog();
                return AlertBoxResult.Positive;
            }
            else
            {
                alertbox = new alertBox
                {
                    messageLabel = { Text = message },
                    type0 = { Visibility = Visibility.Hidden },
                    type1 = { Visibility = Visibility.Visible },
                    yesButton = { Content = pos },
                    noButton = { Content = neg },
                };
                if (owner)
                {
                    alertbox.Owner = wind;
                    alertbox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                }
                else
                {
                    if (wind != null)
                    {
                        alertbox.Owner = wind;
                        double left = point.X + 2 - alertbox.Width, top = point.Y + 2 - alertbox.Height;
                        if (left < 0) left = 0;
                        if (top < 0) top = 0;
                        alertbox.Left = left;
                        alertbox.Top = top;
                    }
                    else alertbox.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
                alertbox.ShowDialog();
                return YesNoResult;
            }
        }

        private void type0_Click(object sender, RoutedEventArgs e) // OK CLICK
        {
            alertbox.Close();
            alertbox = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == yesButton)
            {
                YesNoResult = AlertBoxResult.Positive;
            }
            else
            {
                YesNoResult = AlertBoxResult.Negative;
            }
            alertbox.Close();
            alertbox = null;
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private Point GetMousePos(Window screen)
        {
            return screen.PointToScreen(Mouse.GetPosition(screen));
        }
    }
}
