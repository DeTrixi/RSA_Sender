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
        private RSAEncrypt _rsa;
        public MainWindow()
        {
            InitializeComponent();
            _rsa = new RSAEncrypt();
            _rsa.AssignNewKey();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ModulusText.Text))
            {
                MessageBox.Show("You must enter Modulus");
            }
            else
            {
                _rsa._publicKey.Modulus = Convert.FromBase64String(ModulusText.Text);
                _rsa._publicKey.Exponent = Convert.FromBase64String(ExponentText.Text);
                byte[] encryptedMessege = _rsa.EncryptData(Encoding.UTF8.GetBytes(MessageText.Text));
                //byte[] encryptedMessege2 = _rsa.EncryptData(Convert.FromBase64String(MessageText.Text));
                CipherBytesText.Text = Convert.ToBase64String(encryptedMessege);
                //CipherBytesText.Text = Convert.ToBase64String(encryptedMessege2);
                //byte[] DecryptedMessage = _rsa.EncryptData(encryptedMessege);
                //DecryptedText.Text = Convert.ToBase64String(_rsa.DecryptData(encryptedMessege));
            }
        }
    }
}
