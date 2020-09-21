using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows.Controls;
using System;
using System.Windows.Interop;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Alvas.Audio;
using System.Diagnostics;

namespace RememberIt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Force Closing of Alvas Audio window
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        const UInt32 WM_CLOSE = 0x0010;
        BackgroundWorker bgAlvasRegWindowCloser = new BackgroundWorker();

        void bgAlvasRegWindowCloser_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                System.Threading.Thread.Sleep(100);
                IntPtr windowPtr = FindWindowByCaption(IntPtr.Zero, "* Unregistered version * - This screen is shown only in a unregistered version");
                if (windowPtr == IntPtr.Zero) continue;
                SendMessage(windowPtr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                break;
            }
        }
        #endregion Force Closing of Alvas Audio window

        private Stack<System.Windows.Controls.UserControl> ActivityStack = new Stack<System.Windows.Controls.UserControl>();
        private System.Windows.Controls.UserControl CurrentActivity = null;
        CtrlCardBrowser ctrlCardBrowser = new CtrlCardBrowser();
        CtrlHome ctrlHome = new CtrlHome();
        CtrlContent ctrlContent = new CtrlContent();
        CtrlCard ctrlCard = null;


        private void EscapePressed()
        {
            PopLastScreen();
        }

        public MainWindow()
        {
            InitializeComponent();

            //FrmTest t = new FrmTest();
            //t.ShowDialog();
            //return;

            List<string> def_cards = Globals.GetListOfDefectiveCards();
            if (def_cards.Count != 0)
            {
                MessageBox.Show("There are some defective cards detected. Please check them up.");

                frmDefectiveCards f = new frmDefectiveCards();
                f.ShowDialog();
            }

            Init();
        }



        private void Init()
        {
            Globals.Root = Globals.GetExecutablePath();
        
            StopAlAu();
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            System.Drawing.Rectangle rect = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            this.Width = 300;
            this.Height = rect.Height;
            this.Top = 0;
            this.Left = rect.Width - this.Width;


            this.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(MainWindow_PreviewKeyDown);

            ctrlHome.OnReq_SetDefaultGroup += new EventHandler(ctrlHome_OnReq_SetDefaultGroup);
            ctrlHome.wndMainWindow = this;


            PresentContent(ctrlHome);
            //PresentContent(ctrlContent);
            //WndTest t = new WndTest();
            //t.ShowDialog();
        }



        void ctrlHome_OnReq_SetDefaultGroup(object sender, EventArgs e)
        {
            frmSelectCardGroup scg = new frmSelectCardGroup();
            scg.ShowDialog();
            if (scg.IsCanceled == true) return;
            Globals.SetDefaultGroup(scg.SelectedGroupPath);
        }


        private void StopAlAu()
        {
            // Get rid of Alvas Audio registration window
            bgAlvasRegWindowCloser.DoWork += new DoWorkEventHandler(bgAlvasRegWindowCloser_DoWork);
            bgAlvasRegWindowCloser.RunWorkerAsync();
            RecorderEx rex = new RecorderEx();
            rex.Format = AudioCompressionManager.GetPcmFormat(1, 16, 44100);
            rex.StartRecord();
            rex.StopRecord();
            // Get rid of Alvas Audio registration window
        }

        void MainWindow_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                EscapePressed();
            }
        }

        public void PresentContent(System.Windows.Controls.UserControl AScreen)
        {
            if (CurrentActivity != null)
            {
                this.ActivityStack.Push(CurrentActivity);
            }
            CurrentActivity = AScreen;
            this.ContentHolder.Content = CurrentActivity;
        }

        public void PopLastScreen()
        {
            // If user does not want to delete current working card just do nothing.
            if (CurrentActivity is CtrlCard)
            {
                CtrlCard card = ((CtrlCard)CurrentActivity);
                if (card.ExternalAskToDiscardIsInProcess == true)
                {
                    card.ExternalAskToDiscard();
                    if (card.AllowClose == false) return;
                }
                else
                {
                    if (card.SaveIsPressed == true)
                    {

                    }
                    else // Discard is pressed
                    {

                    }
                }
            }

            if (this.ActivityStack.Count > 0)
            {
                CurrentActivity = this.ActivityStack.Pop();
                this.ContentHolder.Content = CurrentActivity;
            }
        }




        // Fade out
        //private void Window_Closing(object sender, CancelEventArgs e)
        //{
        //    Closing -= Window_Closing;
        //    e.Cancel = true;
        //    var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(1));
        //    anim.Completed += (s, _) => this.Close();
        //    this.BeginAnimation(UIElement.OpacityProperty, anim);
        //}
    }


}