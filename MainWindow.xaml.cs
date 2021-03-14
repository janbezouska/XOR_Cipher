using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace XOR_Cipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Cipher_Click(object sender, RoutedEventArgs e)
        {
            DeCipher(false);
        }

        private void Decipher_Click(object sender, RoutedEventArgs e)
        {
            DeCipher(true);
        }
        public void DeCipher(bool decipher)
        {
            string strContent = "";
            string strKey = tbKey.Text;
            string strOutput = "";
            //List<int> backupList = new List<int>();
            bool err = false;


            if(!decipher)
                strContent = new TextRange(rtbInput.Document.ContentStart, rtbInput.Document.ContentEnd).Text.Trim();
            else
                strContent = new TextRange(rtbOutput.Document.ContentStart, rtbOutput.Document.ContentEnd).Text.Replace("\r\n", "");


            if (strContent == "")
            {
                tbErr.Foreground = Brushes.Red;
                tbErr.Clear();
                if (!decipher)
                    tbErr.AppendText("NOTHING TO CIPHER");
                else
                    tbErr.AppendText("NOTHING TO DECIPHER");
                err = true;
            }
            if (strKey == "")
            {
                if (err)
                    tbErr.AppendText("\nNO KEY");
                else
                {
                    tbErr.Foreground = Brushes.Red;
                    tbErr.Clear();
                    tbErr.AppendText("NO KEY");
                }
            }
            else if (!err)
            {
                tbErr.Clear();

                for (int i = 0; i < strContent.Length; i++)
                    strOutput += (char)(strContent[i] ^ strKey[i % strKey.Length]);

                if(!decipher)
                {
                    rtbOutput.Document.Blocks.Clear();
                    rtbOutput.AppendText(strOutput);
                }
                else
                {
                    rtbInput.Document.Blocks.Clear();
                    rtbInput.AppendText(strOutput);
                }
            }
        }
    }

}
