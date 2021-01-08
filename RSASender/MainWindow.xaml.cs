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
        // creates a object of the rsa class
        private readonly RSAEncrypt _rsa;

        delegate void UpdateCipherText(string cipher);
        // Here i create a tcp server
        private readonly TcpServer _tcpServer;
        // Here i set the port it should be listening to
        const int PortNo = 13005;
        const string ServerIp = "127.0.0.1";

        public MainWindow()
        {
            InitializeComponent();
            // Here i create a tcp server
            _tcpServer = new TcpServer();
            // here i Start the tcp server
            Task.Run((() => _tcpServer.TcpServerStart(ServerIp, PortNo)));
            // here i subscribe to the event when message was received
            _tcpServer.MessageReceived += _tcpServer_MessageReceived;
            _rsa = new RSAEncrypt();
            _rsa.AssignNewKey();
        }

        /// <summary>
        /// This event triggers if message i TcpServer is received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _tcpServer_MessageReceived(object sender, string e)
        {
            Dispatcher.BeginInvoke(new UpdateCipherText(UpdateCipher), new object[] {e});
        }

        /// <summary>
        /// This method is used to update the UI from Tcp server task
        /// </summary>
        /// <param name="cipher"></param>
        private void UpdateCipher(string cipher)
        {
            ModulusText.Text = cipher;
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
                string message = Convert.ToBase64String(encryptedMessage);
                CipherBytesText.Text = message;
                TcpClient client = new TcpClient();
                client.Connect("127.0.0.1", message);
            }
        }
    }
}