using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;


namespace DesktopChanger
{
    public partial class MainWindow : Window
    {
        bool isExisted = false;
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                PathWorker.WallpaperPathWorker();
                isExisted = true;

                DateTime localDate1 = DateTime.Now;
                String minute = localDate1.ToString("mm", new CultureInfo("en-US"));
                String second = localDate1.ToString("ss", new CultureInfo("en-US"));

                int trueMinute = 0;
                int trueSeconds = 60 - Convert.ToInt16(second);

                DayChangeLogic.timer.Tick += new EventHandler(DayChangeLogic.timerTick);
                DayChangeLogic.timer.Interval = new TimeSpan(0, trueMinute, trueSeconds);
                DayChangeLogic.timer.Start();
            }
            catch (Exception ev)
            {

            }

            if (isExisted)
            {
                this.Hide();
            }

            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("fire-2-24.ico");
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
             PathWorker.WallpaperPathChanger(txtMonday.Text, 0);
             PathWorker.WallpaperPathChanger(txtSaturday.Text, 1);
             PathWorker.WallpaperPathChanger(txtWednesday.Text, 2);
             PathWorker.WallpaperPathChanger(txtThursday.Text, 3);
             PathWorker.WallpaperPathChanger(txtFriday.Text, 4);
             PathWorker.WallpaperPathChanger(txtSaturday.Text, 5);
             PathWorker.WallpaperPathChanger(txtSunday.Text, 6);
             PathWorker.WallpaperPathWriter();   

             if (!isExisted)
             {
                DayChangeLogic.timer.Tick += new EventHandler(DayChangeLogic.timerTick);
                DayChangeLogic.timer.Interval = new TimeSpan(0, 0, 5);
                DayChangeLogic.timer.Start();
             }         
        }
    }
}
