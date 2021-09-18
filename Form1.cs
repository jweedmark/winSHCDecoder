using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

/*  Originally ported from https://github.com/obrassard/shc-extractor
 *  by jweedmark
 *  Sep 15 2021
 *  SHC - SmartHealth Card QR Code Decoder
 */

namespace winQRCodeDecoder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string ParseSHC(string fileData)
        {
            string NumericToData = "";
            var path = Regex.Matches(fileData.Replace("shc:/", ""), "(..?)");
            foreach (Match i in path){ NumericToData += Convert.ToChar(SHCDecode(i.Value)[0]); }
            string[] JWSData = NumericToData.Split('.');

            string HeaderDecoded = ToBase64Decode(JWSData[0] + "==");
            string PayloadDecoded = ParseJWSPayload(JWSData[1]);
            string VerificationDecoded = JWSData[2];

            return PayloadDecoded;
        }
        private static sbyte[] SHCDecode(string s)
        {
            sbyte[] shcByte = new sbyte[s.Length / 2];
            for (int i = 0, j = 0; i < s.Length; i += 2, j++) {
                shcByte[j] = (sbyte)(Convert.ToInt32(s.Substring(i, 2), 10) + 45);
            }
            return shcByte;
        }
        private static string ParseJWSPayload(string jwsData)
        {
            string tmpData = jwsData.Trim().Replace("-", "+").Replace("_", "/");
            if (tmpData.Length % 4 > 0)
                tmpData = tmpData.PadRight(tmpData.Length + 4 - tmpData.Length % 4, '=');

            return JsonConvert.SerializeObject(JsonConvert.DeserializeObject
                (InflateJWSBytes(Convert.FromBase64String(tmpData))), Formatting.Indented);
        }
        private static string ToBase64Decode(string base64EncodedText)
        {
            if (String.IsNullOrEmpty(base64EncodedText)) { return base64EncodedText; }

            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedText);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
        private static string InflateJWSBytes(byte[] bytes)
        {
            using (var output = new MemoryStream())
            {
                using (var input = new MemoryStream(bytes))
                {
                    using (var unzip = new DeflateStream(input, CompressionMode.Decompress))
                    {
                        unzip.CopyTo(output, bytes.Length);
                        unzip.Close();
                    }
                    return Encoding.UTF8.GetString(output.ToArray());
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            txtInput.Text = ParseSHC(txtInput.Text);
        }
    }
}