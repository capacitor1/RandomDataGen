using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomDataGenerater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int cnt;
        internal static string GetLongTimeStamp(DateTime dateTime)
        {
            return dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString("F0");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Stream fileStream = new FileStream(GetLongTimeStamp(DateTime.Now) + "_Random.dat", FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fileStream);
            bw.Seek(0, SeekOrigin.Begin);
            if (int.TryParse(textBox1.Text, out int val))
            {
                cnt = val;
            }
            else
            {
                cnt = 1;
            }
            for (int i = 0; i < cnt; i++) 
            {
                var characters = "ABCDEF0123456789";
                var Charsarr = new char[2097152];
                var random = new Random();

                for (int j = 0; j < Charsarr.Length; j++)
                {
                    Charsarr[j] = characters[random.Next(characters.Length)];
                }

                var resultString = new String(Charsarr);



                byte[] returnBytes = new byte[resultString.Length / 2];
                for (int a = 0; a < returnBytes.Length; a++)
                {
                    returnBytes[a] = Convert.ToByte(resultString.Substring(a * 2, 2), 16);
                }
                bw.Write(returnBytes);
                bw.Flush();
            }
            
            bw.Close();
        }
    }
}
