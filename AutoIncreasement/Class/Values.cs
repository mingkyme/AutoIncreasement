using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoIncreasement.Class
{
    class Values : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _Variable = 0;
        public int Variable
        {
            get { return _Variable; }
            set { _Variable = value; NotifyPropertyChanged(nameof(Variable)); }
        }
        private int _Interval = 1;
        public int Interval
        {
            get { return _Interval; }
            set { _Interval = value; NotifyPropertyChanged(nameof(Interval)); }
        }
        public void NotifyPropertyChanged(String info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        
    }
}
