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

namespace CheckersUI
{
    /// <summary>
    /// Interaction logic for DrawMenu.xaml
    /// </summary>
    public partial class DrawMenu : UserControl
    {
        public event Action<Option> OptionSelected;
        public static bool IsAsked { get; set; } = false;

        public DrawMenu()
        {
            InitializeComponent();

            IsAsked = true;
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Yes);
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.No);
        }
    }
}
