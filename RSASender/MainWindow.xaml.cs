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

namespace RSASender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RSAEncrypt _rsa;

        public MainWindow()
        {
            InitializeComponent();
            _rsa = new RSAEncrypt();
            _rsa.AssignNewKey();
        }

        /// <summary>
        /// Encrypts the Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ModulusText.Text))
            {
                MessageBox.Show("You must enter Modulus");
            }
            else
            {
                // updates this Pyblic key so that it get Receivers public key
                _rsa._publicKey.Modulus = Convert.FromBase64String(ModulusText.Text);
                _rsa._publicKey.Exponent = Convert.FromBase64String(ExponentText.Text);
                
                // Encrypts the message 
                byte[] encryptedMessage = _rsa.EncryptData(Encoding.UTF8.GetBytes(MessageText.Text));
                // Displays the text in the window
                CipherBytesText.Text = Convert.ToBase64String(encryptedMessage);
            }
        }
    }
}