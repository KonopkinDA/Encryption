using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace WFApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fName = openFileDialog1.FileName;
                saveFileDialog1.Filter = "txt files |*.txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fDestination = saveFileDialog1.FileName;
                    if (comboBox1.SelectedIndex == 0)
                    {
                        EncryptDES(fName, fDestination, textBox1.Text.ToString());
                    }
                    else
                    {
                        EncryptAES(fName, fDestination, textBox1.Text.ToString());
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "txt files |*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fName = openFileDialog1.FileName;
                saveFileDialog1.Filter = "txt files |*.txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fDestination = saveFileDialog1.FileName;
                    if (comboBox1.SelectedIndex == 0)
                    {
                        DecryptDES(fName, fDestination, textBox1.Text.ToString());
                    }
                    else
                    {
                        DecryptAES(fName, fDestination, textBox1.Text.ToString());
                    }
                }
            }
        }

        public static void EncryptDES(string name, string destination, string key)
        {
            FileStream fsOpen = new FileStream(name, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypt = new FileStream(destination, FileMode.Create, FileAccess.Write);
            DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();
            try
            {
                desProvider.Key = ASCIIEncoding.ASCII.GetBytes(key);
                desProvider.IV = ASCIIEncoding.ASCII.GetBytes(key);
                ICryptoTransform cryptoTransform = desProvider.CreateEncryptor();
                CryptoStream cryptoStream = new CryptoStream(fsEncrypt, cryptoTransform, CryptoStreamMode.Write);
                byte[] cryptoInput = new byte[fsOpen.Length - 0];
                fsOpen.Read(cryptoInput, 0, cryptoInput.Length);
                cryptoStream.Write(cryptoInput, 0, cryptoInput.Length);
                cryptoStream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            fsOpen.Close();
            fsEncrypt.Close();
        }

        public static void DecryptDES(string name, string destination, string key)
        {
            FileStream fsOpen = new FileStream(name, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypt = new FileStream(destination, FileMode.Create, FileAccess.Write);
            DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();
            try
            {
                desProvider.Key = ASCIIEncoding.ASCII.GetBytes(key);
                desProvider.IV = ASCIIEncoding.ASCII.GetBytes(key);
                ICryptoTransform cryptoTransform = desProvider.CreateDecryptor();
                CryptoStream cryptoStream = new CryptoStream(fsEncrypt, cryptoTransform, CryptoStreamMode.Write);
                byte[] cryptoInput = new byte[fsOpen.Length - 0];
                fsOpen.Read(cryptoInput, 0, cryptoInput.Length);
                cryptoStream.Write(cryptoInput, 0, cryptoInput.Length);
                cryptoStream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            fsOpen.Close();
            fsEncrypt.Close();
        }

        public static void EncryptAES(string name, string destination, string key)
        {
            FileStream fsOpen = new FileStream(name, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypt = new FileStream(destination, FileMode.Create, FileAccess.Write);

            try
            {

                byte[] IV = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                AesManaged aesM = new AesManaged();
                aesM.Key = Encoding.UTF8.GetBytes(key);
                aesM.IV = IV;

                CryptoStream cryptoStream = new CryptoStream(fsEncrypt, aesM.CreateEncryptor(), CryptoStreamMode.Write);
                byte[] cryptoInput = new byte[fsOpen.Length - 0];
                fsOpen.Read(cryptoInput, 0, cryptoInput.Length);
                cryptoStream.Write(cryptoInput, 0, cryptoInput.Length);
                cryptoStream.FlushFinalBlock();
                cryptoStream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            fsOpen.Close();
            fsEncrypt.Close();
        }

        public static void DecryptAES(string name, string destination, string key)
        {
            FileStream fsOpen = new FileStream(name, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypt = new FileStream(destination, FileMode.Create, FileAccess.Write);

            try
            {

                byte[] IV = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                AesManaged aesM = new AesManaged();
                aesM.Key = Encoding.UTF8.GetBytes(key);
                aesM.IV = IV;

                CryptoStream cryptoStream = new CryptoStream(fsEncrypt, aesM.CreateDecryptor(), CryptoStreamMode.Write);
                byte[] cryptoInput = new byte[fsOpen.Length - 0];
                fsOpen.Read(cryptoInput, 0, cryptoInput.Length);
                cryptoStream.Write(cryptoInput, 0, cryptoInput.Length);
                cryptoStream.FlushFinalBlock();
                cryptoStream.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            fsOpen.Close();
            fsEncrypt.Close();

        }


    }
}
