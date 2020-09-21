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
    /// Interaction logic for CtrlRoundLabel.xaml
    /// </summary>
    public partial class CtrlRoundLabel : UserControl
    {
        public CtrlRoundLabel()
        {
            InitializeComponent();
        }

        public string LabelText
        {
            get
            {
                return lblText.Text.ToString();
            }
            set
            {
                lblText.Text= value;
            }

        }

    }
}
