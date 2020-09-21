using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace RememberIt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            List<string> def_cards = Globals.GetListOfDefectiveCards();
            if (def_cards.Count != 0)
            {
                MessageBox.Show("There are some defective cards detected. Please check them up.");
                
                frmDefectiveCards f = new frmDefectiveCards();
                f.ShowDialog();
            }
        }
    }
}
