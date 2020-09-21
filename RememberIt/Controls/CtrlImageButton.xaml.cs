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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RememberIt
{
    /// <summary>
    /// Interaction logic for CtrlImageButton.xaml
    /// </summary>
    public partial class CtrlImageButton : UserControl
    {
        public CtrlImageButton()
        {
            InitializeComponent();
        }

        public ImageSource SourceOfImage
        {
            get
            {
                return imgImage.Source;
            }
            set
            {
                imgImage.Source = value;
            }
        }

        public event RoutedEventHandler OnImageButtonClicked = null;


        private void btnButton_Click(object sender, RoutedEventArgs e)
        {
            if (OnImageButtonClicked == null) return;
            this.OnImageButtonClicked(this, null);
        }
    }
}
