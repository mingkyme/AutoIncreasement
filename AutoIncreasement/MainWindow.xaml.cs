using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
        private Class.Values values;
        private static MainWindow main;
        public MainWindow()
        {
            if(main == null)
            {
                main = this;
            }
            values = new Class.Values();
            InitializeComponent();
            DataContext = values;
            XAML_Before_Text.Text = "${INDEX} 번째 손님을 환영합니다.\n\n";
        }

        private void XAML_Text_Changed(object sender, TextChangedEventArgs e)
        {
            string beforeText = XAML_Before_Text.Text;
            string afterText = beforeText.Replace("${INDEX}", values.Variable.ToString());
            XAML_After_Text.Text = afterText;
        }
        // 붙혀넣기 하고 나면 값 증가
        private void Increasement()
        {
            values.Variable += values.Interval;
        }
        private void Copy()
        {
            Clipboard.SetText(XAML_After_Text.Text);
            Increasement();
        }


        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        const int WH_KEYBOARD_LL = 13; // Номер глобального LowLevel-хука на клавиатуру
        const int WM_KEYDOWN = 0x100; // Сообщения нажатия клавиши
        const int VK_V = 0x0056; // Сообщения нажатия клавиши

        private LowLevelKeyboardProc _proc = HookProc;

        private static IntPtr hhook = IntPtr.Zero;

        public void SetHook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, hInstance, 0);
        }

        public static void UnHook()
        {
            UnhookWindowsHookEx(hhook);
        }

        public static IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (vkCode == VK_V && Keyboard.IsKeyDown(Key.LeftCtrl))
                {
                    main.Copy();
                }
                // return (IntPtr)1;
                return CallNextHookEx(hhook, code, (int)wParam, lParam);
            }
            else
                return CallNextHookEx(hhook, code, (int)wParam, lParam);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetHook();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            UnHook();
        }
    }

}
