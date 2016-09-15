using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace E_Kulit_Magetan
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            showClock();
            showDay();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void showClock()
        {
            label3.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void showDay()
        {
            label4.Text = DateTime.Now.ToString("dddd");
        }

        public void ExecuteCommand(string command)
        {
            int ExitCode;
            ProcessStartInfo ProcessInfo;
            Process process;
            //C:\\xampp\\tomcat\\bin\\catalina.bat

            ProcessInfo = new ProcessStartInfo("C:\\xampp\\tomcat\\bin\\startup.bat", command);
            ProcessInfo.UseShellExecute = false;
            //ProcessInfo.Arguments = "run";
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.WorkingDirectory = "C:\\xampp\\tomcat\\bin";
            // *** Redirect the output ***
            ProcessInfo.RedirectStandardError = true;
            ProcessInfo.LoadUserProfile = true;
            ProcessInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ProcessInfo.RedirectStandardOutput = true;
            ProcessInfo.RedirectStandardInput = true;
            process = Process.Start(ProcessInfo);
            process.EnableRaisingEvents = true;
            //process.WaitForExit();

            // *** Read the streams ***
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            ExitCode = process.ExitCode;
            string ec = ExitCode.ToString();
            string t = "Tomcat Started";
            string tt = "Upps...Tomcat Not Started";
            if (ec.Equals("1"))
            {
                MessageBox.Show(tt);
            } else
            {
                MessageBox.Show(t);
                label5.ForeColor = Color.Green;

            }
            //MessageBox.Show("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            //MessageBox.Show("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            //MessageBox.Show("ExitCode: " + ExitCode.ToString(), "ExecuteCommand");
            //process.Close();
        }

       

        private void label8_Click(object sender, EventArgs e)
        {
            string debug = "tes.bat";
            ExecuteCommand("startup.bat");
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
