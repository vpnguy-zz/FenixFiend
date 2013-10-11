using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FenixFiend
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            //Post example shamelessly ripped from MSDN
            WebRequest request = WebRequest.Create("http://www.fenixlight.com/service/en/Code/Inquiry");
            request.Method = "POST";
            string postData = "Serial=" + GenerateFenix(comboBox1.Text);
            //MessageBox.Show(postData); //For debug, feel with it
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            responseFromServer = CleanStr(responseFromServer);
            label2.Text = StrSplit(responseFromServer, "Serial Numbertd            td                ", "td");
            label4.Text = StrSplit(responseFromServer, "Modeltd            td                ", "td");
            label7.Text = StrSplit(responseFromServer, "LEDtd            td                ", "td");
            label5.Text = StrSplit(responseFromServer, "Reflectortd            td                ", "td");
            label11.Text = StrSplit(responseFromServer, "Colortd            td                ", "td");
            label9.Text = StrSplit(responseFromServer, "Sales Country or Regiontd            td                ", "td");
            reader.Close();
            dataStream.Close();
            response.Close();
        }
        private static string GenerateFenix(string Type)
        {
            Random random = new Random();
            switch (Type)
            {
                case "E11 Black":
                    return "F4797R0" + random.Next(1000,2999);
                case "E11 Silver":
                    return "F4EDH40" + random.Next(1000, 2999);
                case "LD01 Black":
                    return "F3CSCL00" + random.Next(100, 999);
                case "LD01 Stainless Steel":
                    return "42SW2T0" + random.Next(3000, 5999);

            }
            return null;
        }
        private static string CleanStr(string Input)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            Input = rgx.Replace(Input, "");
            Input = Input.Replace(System.Environment.NewLine, "");
            return Input;
        }
        private static string StrSplit( string strSource, string strStart, string strEnd)
        {
            int stPos = 0;
            int stEnd = 0;
            int lengthStr = strStart.Length;
            string strResult = null;
            strResult = string.Empty;
            stPos = strSource.IndexOf(strStart, 0);
            stEnd = strSource.IndexOf(strEnd, stPos + lengthStr);
            if (stPos != -1 && stEnd != -1)
            {
                strResult = strSource.Substring(stPos + lengthStr, stEnd - (stPos + lengthStr));
            }
            return strResult;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FenixFiend is a tool for informational use only.\nFenixFiend attempts to generate the 11 digit serial numbers for fenix flashlights.\n\nNot intended for illicit uses");
        }
    }
}
