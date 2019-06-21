using Microsoft.Win32;
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
using System.Globalization;
using System.Runtime.InteropServices;
using System.IO;


namespace DesktopChanger
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isZero = false;
        String FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+"\\Path.txt";
        bool isExisted = false;
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            
            try
            {
                StreamReader file = new StreamReader(FilePath);
                txtMonday.Text = file.ReadLine();
                txtTuesday.Text = file.ReadLine();
                txtWednesday.Text = file.ReadLine();
                txtThursday.Text = file.ReadLine();
                txtFriday.Text = file.ReadLine();
                txtSaturday.Text = file.ReadLine();
                txtSunday.Text = file.ReadLine();
                file.Close();
                isExisted = true;

                DateTime localDate1 = DateTime.Now;
                String minute = localDate1.ToString("mm", new CultureInfo("en-US"));
                String second = localDate1.ToString("ss", new CultureInfo("en-US"));

                int trueMinute = 59 - Convert.ToInt16(minute);
                int trueSeconds = 60 - Convert.ToInt16(second);

                timer.Tick += new EventHandler(timerTick);
                timer.Interval = new TimeSpan(0, trueMinute, trueSeconds);
                timer.Start();
            }
            catch(Exception ev)
            {

            }

            if(isExisted)
            {
                    this.Hide();
            }


            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("D://back//fire-2-24.ico");
            ni.Visible = true;
            System.Windows.Forms.ContextMenu myMenu1 = new System.Windows.Forms.ContextMenu();
           
            myMenu1.MenuItems.Add("Настройки", new EventHandler(FormShow));
            myMenu1.MenuItems.Add("Выход", new EventHandler(FormExit));
            ni.ContextMenu = myMenu1;
            
        }

        public void FormShow(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        public void FormExit(object sender, EventArgs e)
        {
            this.Close();
        }


        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

 

        [DllImport("User32", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uiAction, int uiParam, string pvParam, uint fWinIni);



        public static void WallPath(Control p)
        {
            var pP = p as TextBox;
            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.Filter = "Картинки(*.JPG;*.PNG)|*.JPG;*.PNG";
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = true;
            if (myDialog.ShowDialog() == true)
            {
                pP.Text = myDialog.FileName;
            }

        }

        public static void WallChange(String path)
        {
            SystemParametersInfo(0x0014, 0, path, 0x0001);
        }


        

        private void BtnMonday_Click(object sender, RoutedEventArgs e)
        {
            WallPath(txtMonday);
        }

        private void BtnTuesday_Click(object sender, RoutedEventArgs e)
        {
            WallPath(txtTuesday);
        }

        private void BtnWednesday_Click(object sender, RoutedEventArgs e)
        {
            WallPath(txtWednesday);
        }

        private void BtnThursday_Click(object sender, RoutedEventArgs e)
        {
            WallPath(txtThursday);
        }

        private void BtnFriday_Click(object sender, RoutedEventArgs e)
        {
            WallPath(txtFriday);
        }

        private void BtnSaturday_Click(object sender, RoutedEventArgs e)
        {
            WallPath(txtSaturday);
        }

        private void BtnSunday_Click(object sender, RoutedEventArgs e)
        {
            WallPath(txtSunday);
        }

        private void BtnReady_Click(object sender, RoutedEventArgs e)
        {

            if (txtMonday.Text != "" &&
                txtTuesday.Text != "" &&
                txtWednesday.Text != "" &&
                txtThursday.Text != "" &&
                txtFriday.Text != "" &&
                txtSaturday.Text != "" &&
                txtSunday.Text != "")
            {

                StreamWriter file = new StreamWriter(FilePath);
                file.Write(txtMonday.Text);
                file.Write(Environment.NewLine);
                file.Write(txtTuesday.Text);
                file.Write(Environment.NewLine);
                file.Write(txtWednesday.Text);
                file.Write(Environment.NewLine);
                file.Write(txtThursday.Text);
                file.Write(Environment.NewLine);
                file.Write(txtFriday.Text);
                file.Write(Environment.NewLine);
                file.Write(txtSaturday.Text);
                file.Write(Environment.NewLine);
                file.Write(txtSunday.Text);
                file.Close();

                if (!isExisted)
                {
  

                    timer.Tick += new EventHandler(timerTick);
                    timer.Interval = new TimeSpan(0, 0, 5);
                    timer.Start();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Пожалуйста, заполните все поля");
            }
        }
 
        String newDay = "";
        private void timerTick(object sender, EventArgs e)
        {
            if (!isZero)
            {
                timer.Interval = new TimeSpan(1, 0, 10);
                isZero = true;
            }
            DateTime localDate = DateTime.Now;
            String day = localDate.ToString("dddd", new CultureInfo("en-US"));
            if (newDay != day)
            {
                dayChange(day);
            }
            
        }

        public void dayChange(string day)
        {
            switch (day)
            {
                case "Monday":
                    WallChange(txtMonday.Text.ToString());
                    newDay = day;
                    break;
                case "Tuesday":
                    WallChange(txtTuesday.Text.ToString());
                    newDay = day;
                    break;
                case "Wednesday":
                    WallChange(txtWednesday.Text.ToString());
                    newDay = day;
                    break;
                case "Thursday":
                    WallChange(txtThursday.Text.ToString());
                    break;
                case "Friday":
                    WallChange(txtFriday.Text.ToString());
                    break;
                case "Saturday":
                    WallChange(txtSaturday.Text.ToString());
                    break;
                case "Sunday":
                    WallChange(txtTuesday.Text.ToString());
                    break;
                default:
                    break;
            }
        }


    }
}
