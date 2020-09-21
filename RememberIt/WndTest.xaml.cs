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
    /// Interaction logic for WndTest.xaml
    /// </summary>
    public partial class WndTest : Window
    {
        public WndTest()
        {
            InitializeComponent();

            ctrlQuestion.BgColor = new SolidColorBrush(Color.FromRgb(195, 210, 230));
            ctrlAnswer.BgColor = new SolidColorBrush(Color.FromRgb(170, 250, 175));
            ctrlReminder.BgColor = new SolidColorBrush(Color.FromRgb(225, 195, 220));
        }
    }
}
