using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lesson_2
{
    /// <summary>
    /// Interaction logic for TextBoxEmpty.xaml
    /// </summary>
    public partial class TextBoxEmpty : UserControl
    {
        #region Prop
        public string Text { 
            get { return textBox.Text; }
            set { textBox.Text = value.Trim(); }
        }

        public static Brush DefaultBrush { get; set; } = Brushes.Red;

        public Brush TextBoxBorderBrush
        {
            get { return border.BorderBrush; }
            set { border.BorderBrush = value; }
        }
        #endregion
        public TextBoxEmpty()
        {
            InitializeComponent();
            border.BorderBrush = DefaultBrush;
        }

        public void SetError(string errorText)
        {
            textBlockTooltip.Text = errorText;
            if(errorText == "")
            {
                //no error
                border.BorderThickness = new Thickness(0);
                toolTip.Visibility = Visibility.Hidden;
            }
            else
            {
                //there's error
                border.BorderThickness = new Thickness(1);
                toolTip.Visibility = Visibility.Visible;
            }
        }

        public void SetFocus()
        {
            textBox.Focus();
        }
    }
}
