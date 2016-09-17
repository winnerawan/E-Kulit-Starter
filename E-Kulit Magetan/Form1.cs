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
using System.IO;
using System.Reflection;


namespace E_Kulit_Magetan
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            showClock();
            showDay();
        }
        // funggsi dipanggil ketika aplikasi di minimize
        private void Form1_Resize(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipTitle = "E-Kulit Minimized to Tray";
            notifyIcon1.BalloonTipText = "Double Click to Maximized";

            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Help
            MessageBox.Show("Your Application Settings 1");
        }

        private void showClock()
        {
            label3.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void showDay()
        {
            label4.Text = DateTime.Now.ToString("dddd");
        }


        public void checkUp(string command)
        {
            int ExitCode;
            ProcessStartInfo ProcessInfo;
            Process process;
            //C:\\xampp\\tomcat\\bin\\catalina.bat

            ProcessInfo = new ProcessStartInfo("cmd.exe ", command);
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.Arguments = "/C netstat -na | find "+"LISTENING" +"| find /C /I "+":8080";
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.WorkingDirectory = "C:\\Windows\\System32";
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
            if (ec.Equals("0"))
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
            process.Close();
        }

        public void checkDown(string command)
        {
            int ExitCode;
            ProcessStartInfo ProcessInfo;
            Process process;
            //C:\\xampp\\tomcat\\bin\\catalina.bat

            ProcessInfo = new ProcessStartInfo("cmd.exe ", command);
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.Arguments = "/C netstat -na | find " + "LISTENING" + "| find /C /I " + ":8080";
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.WorkingDirectory = "C:\\Windows\\System32";
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
            string t = "Tomcat Shutdown";
            string tt = "Upps...Tomcat Still Running";
            if (ec.Equals("0"))
            {
                MessageBox.Show(tt);
            }
            else
            {
                MessageBox.Show(t);
                label5.ForeColor = Color.Red;
            }
            //MessageBox.Show("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            //MessageBox.Show("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            //MessageBox.Show("ExitCode: " + ExitCode.ToString(), "ExecuteCommand");
            process.Close();
        }
        private void startTomcat()
        {
            //ExecuteCommand(" run");
            Process proc = null;
            int ExitCode;
            try
            {
                string batDir = string.Format(@"C:\xampp\tomcat\bin");
                proc = new Process();
                proc.StartInfo.WorkingDirectory = batDir;
                proc.StartInfo.FileName = "catalina.bat";
                proc.StartInfo.Arguments = " run";
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                //proc.WaitForExit();
                //MessageBox.Show("Bat file executed !!");
                proc.Close();



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
                ExitCode = proc.ExitCode;
                string ec = ExitCode.ToString();
                string t = "Tomcat Server Started";
                string tt = "Upps...Tomcat Not Started";
                if (ec.Equals("1"))
                {
                    MessageBox.Show(tt);
                }
                else
                {
                    MessageBox.Show(t);
                    label5.ForeColor = Color.Green;
                }
                

            }


        }

        private void stopTomcat()
        {
            Process proc = null;
            int ExitCode;
            try
            {
                string batDir = string.Format(@"C:\xampp\tomcat\bin");
                proc = new Process();
                proc.StartInfo.WorkingDirectory = batDir;
                proc.StartInfo.FileName = "shutdown.bat";
                //proc.StartInfo.Arguments = " run";
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                //proc.WaitForExit();
                //MessageBox.Show("Bat file executed !!");
                string output = proc.StandardOutput.ReadToEnd();
                string error = proc.StandardError.ReadToEnd();

                ExitCode = proc.ExitCode;
                string ec = ExitCode.ToString();
                string t = "Tomcat Server has Been Shutdown";
                string tt = "Upps...Tomcat Still Running";
                if (ec.Equals("1"))
                {
                    MessageBox.Show(tt);
                }
                else
                {
                    MessageBox.Show(t);
                    label5.ForeColor = Color.Red;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }
        


        private void label8_Click(object sender, EventArgs e)
        {
            startTomcat();
            checkUp("/C netstat -na | find " + "LISTENING" + "| find /C /I " + ":8080");
        }

        private void label9_Click(object sender, EventArgs e)
        {
            stopTomcat();
            checkDown("/C netstat -na | find " + "LISTENING" + "| find /C /I " + ":8080");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void extractTomcat()
        {
            String strAppPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            String strFilePath = Path.Combine(strAppPath, "Resources");
            String strFullFilename = Path.Combine(strFilePath, "apache-tomcat-7.0.70.zip");

        }
    }
}
