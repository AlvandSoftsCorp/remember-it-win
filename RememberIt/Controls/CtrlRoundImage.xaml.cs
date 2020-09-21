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
    /// Interaction logic for CtrlRoundImage.xaml
    /// </summary>
    public partial class CtrlRoundImage : UserControl
    {
        public CtrlRoundImage()
        {
            InitializeComponent();
        }


        public ImageSource PictureImageSource
        {
            get
            {
                return Receiver.Source;
            }
            set
            {
                Receiver.Source = value;
            }

        }
    }
}
