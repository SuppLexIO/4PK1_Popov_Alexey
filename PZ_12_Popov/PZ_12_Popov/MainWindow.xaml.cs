using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
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
using System.Xml.Serialization;

namespace PZ_12_Popov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    [Serializable]
    public class DataObject
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                var fileStream = new FileStream("data.xml", FileMode.Open);
                fileStream.Close();
            }
            catch (Exception)
            {
                ButtonDes.Visibility = Visibility.Collapsed;
                throw;
            }
        }
        private void SerializeButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)CheckInt.IsChecked && (bool)CheckString.IsChecked) { }
            else return;

            //Раньше я бы передавал массив, но так куда лучше
            var data = new DataObject
            {
                Name = NameTextBox.Text,
                Age = Convert.ToInt32(AgeTextBox.Text),
            };

            try
            {
                var serializer = new XmlSerializer(typeof(DataObject));
                var fileStream = new FileStream("data.xml", FileMode.Create);
                serializer.Serialize(fileStream, data);
                fileStream.Close();

                MessageBox.Show("Сериализация выполнена");

                ButtonDes.Visibility = Visibility.Visible;

            }
            catch (Exception)
            {
                MessageBox.Show("Сериализация не выполена, возможно нет свободного места?");
                throw;
                
            }
            
        }

        private void DeserializeButton_Click(object sender, RoutedEventArgs e)
        {
            var serializer = new XmlSerializer(typeof(DataObject));
            try
            {
                var fileStream = new FileStream("data.xml", FileMode.Open);
                var data = (DataObject)serializer.Deserialize(fileStream);
                fileStream.Close();

                NameTextBox.Text = data.Name;
                AgeTextBox.Text = data.Age.ToString();
                MessageBox.Show("Десериализация выполнена");
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при десериализации, возможно Вы не выполнили сериализацию и файл не был создан");
                throw;
            }
        }

        private void ProverkaInta(object sender, TextChangedEventArgs e)
        {
            if (AgeTextBox.Text.Length > 0)
            {
                try
                {
                    int WOOOOW = Convert.ToInt32(AgeTextBox.Text);
                }
                catch (Exception)
                {
                    AgeTextBox.Text = AgeTextBox.Text.Remove(AgeTextBox.Text.Length - 1);
                }
                if (AgeTextBox.Text.Length > 0)
                {
                    CheckInt.IsChecked = true;
                }
            }
            else 
            { 
                AgeTextBox.Text = "";
                CheckInt.IsChecked = false;
            }
        }
    }

   
}
    

