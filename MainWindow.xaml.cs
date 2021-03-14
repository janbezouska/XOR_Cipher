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
            DeCipher(false); //spustí se metoda DeCipher s informací, že se jedná o šifrování
        }

        private void Decipher_Click(object sender, RoutedEventArgs e)
        {
            DeCipher(true); //spustí se metoda DeCipher s informací, že se jedná o dešifrování
        }
        public void DeCipher(bool decipher)
        {
            string strContent = "";     //zde se uloží text, který budeme (de)šifrovat
            string strKey = tbKey.Text; //klíč
            string strOutput = "";      //zde se uloží text, který bude výsledkem (de)šifrování
            bool err = false;           //informace o tom, zade se objevila určitá chyba


            if(!decipher)   //pokud šifrujeme, uložíme text z RichTextBoxu pro šifrování
                strContent = new TextRange(rtbInput.Document.ContentStart, rtbInput.Document.ContentEnd).Text.Trim();
            else            //pokud dešifrujeme, uložíme text z RichTextBoxu pro dešifrování
                strContent = new TextRange(rtbOutput.Document.ContentStart, rtbOutput.Document.ContentEnd).Text.Replace("\r\n", "");


            if (strContent == "")               //pokud je text prázdný, vypíšeme chybu
            {
                tbErr.Foreground = Brushes.Red; //červený text
                tbErr.Clear();                  //smazání všeho na TextBoxu
                if (!decipher)                  //vypsání chyby podle toho, zda šifrujeme, nebo dešifrujeme
                    tbErr.AppendText("NOTHING TO CIPHER");
                else
                    tbErr.AppendText("NOTHING TO DECIPHER");
                err = true;                     //uložení informace o chybě
            }
            if (strKey == "")                       //pokud je klíč prázdný, vypíšeme chybu
            {
                if (err)                            //pokud nasatla předchozí chyba, připíšeme tuto pod ní
                    tbErr.AppendText("\nNO KEY");
                else                                //pokud nenastala předchozí chyba, vypíšeme tuto
                {
                    tbErr.Foreground = Brushes.Red; //červený text
                    tbErr.Clear();                  //smazání všeho na TextBoxu
                    tbErr.AppendText("NO KEY");     //vypsání chyby
                }
            }
            else if (!err)      //pokud nenastala žádná chyba, (de)šifrujeme
            {
                tbErr.Clear(); 

                for (int i = 0; i < strContent.Length; i++)     //procházíme jednotlivé znaky textu
                    strOutput += (char)(strContent[i] ^ strKey[i % strKey.Length]); //provedeme XOR operaci na jednotlivé
                                                                                    //znaky textu a klíče, viz readme
                if(!decipher)
                {              //pokud jsme šifrovali, vypíšeme výsledek do RichTextBoxu dešifrování
                    rtbOutput.Document.Blocks.Clear();
                    rtbOutput.AppendText(strOutput);
                }
                else
                {              //pokud jsme dešifrovali, vypíšeme výsledek do RichTextBoxu šifrování
                    rtbInput.Document.Blocks.Clear();
                    rtbInput.AppendText(strOutput);
                }
            }
        }
    }

}
