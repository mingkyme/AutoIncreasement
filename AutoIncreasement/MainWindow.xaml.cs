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

namespace AutoIncreasement
{
    public partial class MainWindow : Window
    {
        private static int _Variable = 0;
        public static int Variable
        {
            get { return _Variable; }
            set { _Variable = value; }
        }
        private static int _Interval = 1;
        public static int Interval
        {
            get { return _Interval; }
            set { _Interval = value; }
        }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void XAML_Before_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            string beforeText = XAML_Before_Text.Text;
            string afterText = beforeText.Replace("${INDEX}", Variable.ToString());
            Increasement();
            XAML_After_Text.Text = afterText;
        }
        private void Increasement()
        {
            Variable = Variable + Interval;
        }
    }

}
