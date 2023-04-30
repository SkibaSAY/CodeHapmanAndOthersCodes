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
using CodesLibrary;
using Newtonsoft.Json;

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
            var dict = HaphmanCode.BuildCode(text);

            codesText.Text = String.Join("\n",dict.ToArray());

            var codedText = HaphmanCode.Coding(text, dict);
            encryptText.Text = codedText;
            var decodText = HaphmanCode.Decoding(codedText, dict);
            decryptText.Text = decodText;
        }

        private void RunHemming_Click(object sender, RoutedEventArgs e)
        {
            var sd = new OpenFileDialog();
            if (sd.ShowDialog() == false) return;
            var text = File.ReadAllText(sd.FileName);
            inputText.Text = text;
            var hemmingCode = new HemmingCode();

            var codedText = "";
            try
            {
                codedText = hemmingCode.Coding(text);
                encryptText.Text = codedText;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //тут всё верно. Делаем вид, что входная строка - это уже код и исправляем
            var decodeText = hemmingCode.Decoding(text);
            decryptText.Text = decodeText;
        }

        private void RunAriph_Click(object sender, RoutedEventArgs e)
        {
            var sd = new OpenFileDialog();
            if (sd.ShowDialog() == false) return;
            var text = File.ReadAllText(sd.FileName);
            inputText.Text = text;
            var ariphCode = new ArifmeticalCode();

            var codedText = "";
            try
            {
                codedText = ariphCode.Coding(text);
                encryptText.Text = codedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            var decodeText = ariphCode.Decoding(codedText);
            decryptText.Text = decodeText;
        }

        private void FanoShennon_Click(object sender, RoutedEventArgs e)
        {
            var sd = new OpenFileDialog();
            if (sd.ShowDialog() == false) return;
            var text = File.ReadAllText(sd.FileName);
            inputText.Text = text;
            var fanoShennonCode = new PhanoShenonCode();

            var codedText = "";
            var resources = "";
            try
            {
                fanoShennonCode.Code(text,out codedText ,out resources);
                encryptText.Text = codedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           var dictionary = JsonConvert.DeserializeObject<Dictionary<char, string>>(resources);
            foreach(var pair in dictionary)
            {
                codesText.Text += pair.Key + "-" +pair.Value + "\n";
            }

            var decodeText = "";
            fanoShennonCode.Decode(codedText, out decodeText, resources);
            decryptText.Text = decodeText;
        }
    }
}
