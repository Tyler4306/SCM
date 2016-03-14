using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace LogReader
{
    public class CallRecord
    {
        public string callDate { get; set; }
        public string callTime { get; set; }
        public string Ext { get; set; }
        public string CallerId { get; set; }
        public string CallDateTime
        {
            get { return callDate + " " + callTime; } 
        }
    }

    public partial class Form1 : Form
    {
        delegate void SetTextCallback(string text);
        string InputData = String.Empty;
        Regex r;
        private static StringBuilder receiveBuffer = new StringBuilder();

        public Form1()
        {
            InitializeComponent();
            serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void SerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            InputData = serialPort.ReadExisting();
            if (InputData != String.Empty)
            {

                this.BeginInvoke(new SetTextCallback(SetRawText), new object[] { InputData });

            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(serialPort.IsOpen)
            {
                serialPort.Close();
            }
            base.OnClosing(e);
        }

        private void SetRawText(string str)
        {
            receiveBuffer.Append(str);
            rawText.Text = receiveBuffer.ToString();
        }

        private void SetText(string text)
        {
            receiveBuffer.Append(text);

            var regex = new Regex(rawText.Text);
            Match match;
            do
            {
                match = regex.Match(receiveBuffer.ToString());
                if (match.Success)
                {
                    //"Process" the significant chunk of data

                    //remove what we've processed from the StringBuilder.
                    receiveBuffer.Remove(match.Captures[0].Index, match.Captures[0].Length);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 1; i < match.Groups.Count; i++)
                    {
                        sb.AppendFormat("{0},", match.Groups[i].Value);
                    }
                    sb.Append("\n");
                    string data = sb.ToString();
                    System.IO.File.AppendAllText("data.csv", data);
                    patternText.Text += match.Captures[0].Value;

                }
            } while (match.Success);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "*.*";
            dlg.Filter = "All Files (*.*) | *.*";
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    rawText.Text = File.ReadAllText(dlg.FileName);

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private bool IsMatchString(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
                button1.Text = "Open";
            }
            else
            {
                rawText.Text = "";
                serialPort.Open();
                button1.Text = "Close";
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(IsMatchString(rawText.Text, patternText.Text).ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Regex rg = new Regex(patternText.Text);
            
            StringBuilder s = new StringBuilder(rawText.Text);

            Match match;
            do
            {
                match = rg.Match(s.ToString());

                if (match.Success)
                {
                    s.Remove(match.Captures[0].Index, match.Captures[0].Length);
                    StringBuilder res = new StringBuilder();

                        foreach (Group g in match.Groups)
                        {

                            res.Append(g.Value.Trim());
                            res.Append(",");
                        }

                        if (res[res.Length - 1] == ',')
                            res.Remove(res.Length - 1, 1);

                    StringBuilder x = new StringBuilder();
                    //CallRecord record = new CallRecord()
                    //{
                    //    callDate = match.Groups["date"].Value,
                    //    callTime = match.Groups["time"].Value,
                    //    Ext = match.Groups["ext"].Value,
                    //    CallerId = match.Groups["callerId"].Value
                    //};
                    for (int i = 0; i < match.Groups.Count; i++)
                    {
                        x.Append(match.Groups[i].Value);
                        x.Append(" - ");
                    }
                    listBox1.Items.Add(x.ToString());
                    //listBox1.Items.Add(record.CallDateTime + " - " + record.CallerId + " - " + record.Ext??"N/A") ;
                }
            } while (match.Success);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "*.*";
            dlg.Filter = "All Files (*.*) | *.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    patternText.Text = File.ReadAllText(dlg.FileName);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
