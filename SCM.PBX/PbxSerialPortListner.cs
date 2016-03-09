using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SCM.PBX
{
    public class PbxSerialPortListner : IDisposable
    {
        private SerialPort serialPort;
        private delegate void HandleMessageCallback(string text);
        string InputData = String.Empty;
        private static StringBuilder receiveBuffer = new StringBuilder();
        string pattern;
        private Regex regex;
        private SqlConnection connection;
        private SqlCommand cmd;
        private bool isRunning = false;
        private string commandName;
        private List<string> parameters;
        private Dictionary<string, object> paramDic;
        private bool isDebug = false;
        private string fileName = "log.txt";


        public PbxSerialPortListner()
        {
            serialPort = new SerialPort();
            serialPort.DataReceived += SerialPort_DataReceived;
            parameters = new List<string>();
            paramDic = new Dictionary<string, object>();
        }

        public void Start()
        {
          if(!IsRunning())
            {
                if (connection == null)
                    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SCMLogger"].ConnectionString);

                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                if (serialPort.IsOpen)
                    serialPort.Close();

                serialPort.Open();
                isRunning = true;
                LogToFile("Started");
            }
        }

        public void Stop()
        {
            if(IsRunning())
            {
                if (serialPort.IsOpen)
                    serialPort.Close();

                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                    connection.Close();

                LogToFile("Stopped");
            }
        }

        public bool IsRunning()
        {
            return isRunning;
        }


        public void LogToFile(string msg)
        {
            if(isDebug)
                System.IO.File.AppendAllLines(fileName, new[] { msg });
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                InputData = serialPort.ReadExisting();
                if (InputData != String.Empty)
                {
                    LogToFile(InputData);
                    ProcessMessage(InputData);
                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(@"E:\SCM-ERRORS.txt", ex.Message + "  --DONE-- ");
                System.IO.File.AppendAllText(@"E:\SCM-ERRORS.txt", ex.StackTrace + "  --DONE-- ");

                throw;
            }
        }

        private void ProcessMessage(string str)
        {
            receiveBuffer.Append(str);
            Match match;
            do
            {
                match = regex.Match(receiveBuffer.ToString());
                if (match.Success)
                {
                    //"Process" the significant chunk of data

                    //remove what we've processed from the StringBuilder.
                    
                    receiveBuffer.Remove(match.Captures[0].Index, match.Captures[0].Length);
                    LogToFile("MATCH => " + match.Value);
                    //StringBuilder sb = new StringBuilder();
                    //for (int i = 1; i < match.Groups.Count; i++)
                    //{
                    //    sb.AppendFormat("{0},", match.Groups[i].Value);
                    //}
                    //sb.Append("\n");
                    //string data = sb.ToString();
                    //System.IO.File.AppendAllText("data.csv", data);
                    paramDic.Clear();
                    foreach(var p in parameters)
                    {
                        paramDic[p] = match.Groups[p].Value;
                        LogToFile("(" + p + ") = " + Convert.ToString(paramDic[p]) + ", ");
                    }
                    Log(paramDic);
                }
            } while (match.Success);
        }

        private void Log(IDictionary<string,object> _params)
        {
            if (connection == null)
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SCMLogger"].ConnectionString);

            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            cmd = connection.CreateCommand();
            cmd.CommandText = commandName;
            LogToFile("EXEC " + cmd.CommandText);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (_params != null & _params.Count > 0)
            {
                foreach (var p in _params)
                {
                    cmd.Parameters.AddWithValue("@" + p.Key, p.Value);
                    LogToFile("@" + p.Key + "(" + Convert.ToString(p.Value) + "), ");
                }
            }
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

        }

        public void UpdateSettings(IDictionary<string,string> settings)
        {
            if(serialPort != null && !serialPort.IsOpen && settings != null)
            {
                // Main Settings
                serialPort.PortName = settings["PortName"];
                serialPort.BaudRate = Convert.ToInt32(settings["BaudRate"]);
                serialPort.DataBits = Convert.ToInt32(settings["DataBits"]);
                serialPort.DiscardNull = Convert.ToBoolean(settings["DiscardNull"]);
                serialPort.Parity = (Parity) Enum.Parse(typeof(Parity), settings["Parity"]);
                serialPort.ParityReplace = Convert.ToByte(settings["ParityReplace"]);
                serialPort.DtrEnable = Convert.ToBoolean(settings["DtrEnable"]);
                serialPort.RtsEnable = Convert.ToBoolean(settings["RtsEnable"]);
                serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), settings["Handshake"]);

                // Others
                serialPort.ReadBufferSize = Convert.ToInt32(settings["ReadBufferSize"]);
                serialPort.WriteBufferSize = Convert.ToInt32(settings["WriteBufferSize"]);
                serialPort.ReadTimeout = Convert.ToInt32(settings["ReadTimeout"]);
                serialPort.WriteTimeout = Convert.ToInt32(settings["WriteTimeout"]);
                serialPort.ReceivedBytesThreshold = Convert.ToInt32(settings["ReceivedBytesThreshold"]);
                serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), settings["StopBits"]);
                if(!string.IsNullOrEmpty(settings["NewLine"]))
                    serialPort.NewLine = settings["NewLine"];

                // Database
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SCMLogger"].ConnectionString);
                }
                commandName = ConfigurationManager.AppSettings["Command"];
                string _params = ConfigurationManager.AppSettings["Parameters"];
                parameters.Clear();
                parameters.AddRange(_params.Split(','));

                // Pattern
                pattern = settings["Pattern"];
                regex = new Regex(pattern);

                // Debug
                isDebug = Convert.ToBoolean(ConfigurationManager.AppSettings["Debug"]);
                fileName = ConfigurationManager.AppSettings["LogFile"];

                LogToFile(pattern);
                LogToFile(commandName);
            }
        }

        public void Dispose()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
                serialPort = null;
            }

            if (connection != null && connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
                cmd = null;
                connection = null;
            }
        }
    }
}
