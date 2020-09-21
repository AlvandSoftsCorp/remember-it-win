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
    /// Interaction logic for CtrlExtButton.xaml
    /// </summary>
    public partial class CtrlExtButton : UserControl
    {
        //protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        //{
        //    base.OnRenderSizeChanged(sizeInfo);
        //    try
        //    {
        //        if (sizeInfo.WidthChanged) grdContainer.Width = sizeInfo.NewSize.Width - 10;
        //        if (sizeInfo.HeightChanged) grdContainer.Height = sizeInfo.NewSize.Height - 10;
        //    }
        //    catch
        //    { 
        //        //
        //    }
        //}

        public event RoutedEventHandler OnExtButtonClicked = null;


        public CtrlExtButton()
        {
            InitializeComponent();
        }

        public ImageSource Image_Source
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


        public string Title
        {
            get
            {
                return lblTitle.Content.ToString();
            }
            set
            {
                lblTitle.Content = value;
            }
        }
        
        
        public string Detail
        {
            get
            {
                return lblDetail.Content.ToString();
            }
            set
            {
                lblDetail.Content = value;
            }
        }

        private void btnButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (this.OnExtButtonClicked != null) this.OnExtButtonClicked(this, e);
        }
    }
}
