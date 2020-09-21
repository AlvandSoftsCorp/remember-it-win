using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace RememberIt
{
    public interface IActivity 
    {
        void Reflect_OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e);
    }
}
