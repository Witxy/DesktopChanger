using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.Diagnostics;

namespace DesktopChanger
{
    public partial class MainWindow : Window
    {
        public static string Style;
        public MainWindow()
        {
            PathWorker.WallpaperPathWorker();

            InitializeComponent();

            DateTime localDate1 = DateTime.Now;
            String minute = localDate1.ToString("mm", new CultureInfo("en-US"));
            String second = localDate1.ToString("ss", new CultureInfo("en-US"));

            int trueMinute = 0;
            int trueSeconds = 60 - Convert.ToInt16(second);

            DayChangeLogic.timer.Tick += new EventHandler(DayChangeLogic.timerTick);
            DayChangeLogic.timer.Interval = new TimeSpan(0, trueMinute, trueSeconds);
            DayChangeLogic.timer.Start();

            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("fire-2-24.ico");
            ni.Visible = true;
            System.Windows.Forms.ContextMenu myMenu1 = new System.Windows.Forms.ContextMenu();

            myMenu1.MenuItems.Add("Настройки", new EventHandler(FormShow));
            myMenu1.MenuItems.Add("About", new EventHandler(OpenGithub));
            myMenu1.MenuItems.Add("Выход", new EventHandler(FormExit));
            ni.ContextMenu = myMenu1;
            ni.DoubleClick += new EventHandler(FormShow);
        }
        
        public void FormShow(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        public void OpenGithub(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Witxy/DesktopChanger");
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

        private void ButtonDaySet_Click(object sender, RoutedEventArgs e)
        {
            int DayIndex = ComboBoxDayOfWeek.SelectedIndex;
            switch (DayIndex)
            {
                case 0: PathWorker.WallpaperPathChanger(TextBoxWallpaperPath.Text, DayIndex); break;
                case 1: PathWorker.WallpaperPathChanger(TextBoxWallpaperPath.Text, DayIndex); break;
                case 2: PathWorker.WallpaperPathChanger(TextBoxWallpaperPath.Text, DayIndex); break;
                case 3: PathWorker.WallpaperPathChanger(TextBoxWallpaperPath.Text, DayIndex); break;
                case 4: PathWorker.WallpaperPathChanger(TextBoxWallpaperPath.Text, DayIndex); break;
                case 5: PathWorker.WallpaperPathChanger(TextBoxWallpaperPath.Text, DayIndex); break;
                case 6: PathWorker.WallpaperPathChanger(TextBoxWallpaperPath.Text, DayIndex); break;
            }
            PathWorker.WallpaperPathWriter();
        }

        private void TxtMonday_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var SelectedTextBox = sender as TextBox;
            WallPath(SelectedTextBox);
        }

        private void ComboBox1_DropDownClosed(object sender, EventArgs e)
        {
            Style = this.ComboBoxWallpaperPositon.Text;
        }

        private void ComboBox1_Initialized(object sender, EventArgs e)
        {
            Style = this.ComboBoxWallpaperPositon.Text;
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            TextBoxWallpaperPath.Text = PathWorker.WallpaperPath[ComboBoxDayOfWeek.SelectedIndex];
        }
    }
}
