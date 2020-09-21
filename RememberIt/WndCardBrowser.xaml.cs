using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RememberIt
{
    /// <summary>
    /// Interaction logic for WndCardBrowser.xaml
    /// </summary>
    public partial class WndCardBrowser : Window
    {
        public WndCardBrowser()
        {
            InitializeComponent();
        }

        private string card_root_folder = "";

        public string CardRootFolder
        {
            set
            {
                this.card_root_folder = value;
            }
        }


        public void Init()
        {
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            System.Drawing.Rectangle rect = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            this.Width = 300;
            this.Height = rect.Height;
            this.Top = 0;
            this.Left = rect.Width - this.Width;
            
            this.ctrlCardBrowser.DisableOperations();
            this.ctrlCardBrowser.SetCardRootFolder(this.card_root_folder);
            this.ctrlCardBrowser.Init(CtrlCardBrowser.InitCondition.SingleSpecifiedCard);
        }
    }
}
