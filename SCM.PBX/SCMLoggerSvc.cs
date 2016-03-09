using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO.Ports;

namespace SCM.PBX
{
    partial class SCMLoggerSvc : ServiceBase
    {

        private PbxSerialPortListner listner;

        public SCMLoggerSvc()
        {
            InitializeComponent();
            listner = new PbxSerialPortListner();
        }

        private IDictionary<string, string> GetSettings()
        {
            //System.Configuration.Configuration config =
            //                   ConfigurationManager.OpenExeConfiguration(
            //                         ConfigurationUserLevel.None);
            //config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            //AppSettingsSection appSettingSection =
            //    (AppSettingsSection)config.GetSection("appSettings");

            var settings = new Dictionary<string, string>();
            foreach (string key in ConfigurationManager.AppSettings.AllKeys)
                settings[key] = ConfigurationManager.AppSettings[key];

             
            return settings;
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                listner.UpdateSettings(GetSettings());

                listner.Start();               
            }
            catch(Exception ex)
            {
                System.IO.File.AppendAllText(@"E:\SCM-Error.txt", ex.Message + " -DONE- ");
                System.IO.File.AppendAllText(@"E:\SCM-Error.txt", ex.StackTrace + " -DONE- ");
            }
        }

        protected override void OnStop()
        {
            listner.Stop();
        }

        
    }
}
