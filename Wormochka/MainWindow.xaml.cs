using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using HaphmanCodeLibrary;

namespace Wormochka
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RunHaphman_Click(object sender, RoutedEventArgs e)
        {
            var sd = new OpenFileDialog();
            if (sd.ShowDialog() == false) return;
            var text = File.ReadAllText(sd.FileName);
            inputText.Text = text;
            var dict = HaphmanCode.BuildCode(sd.FileName);

            codesText.Text = String.Join("\n",dict.ToArray());

            var codedText = HaphmanCode.Coding(text, dict);
            encryptText.Text = codedText;
            var decodText = HaphmanCode.Decoding(codedText, dict);
            decryptText.Text = decodText;
        }
    }
}
