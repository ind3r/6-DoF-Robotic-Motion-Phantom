﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhantomControl
{
    public partial class Settings_tab : UserControl
    {
        URServer urServer = new URServer();

        public Settings_tab()
        {
            if (!this.DesignMode)
            {
                InitializeComponent();

                //setup MotionType combo-box
                LoadDefaultMotionTypes();

                loadSettings();
                updateUISettings();            
            }
        }

        /// <summary>
        /// KM - added default motion types - movej & movel
        /// </summary>
        private void LoadDefaultMotionTypes()
        {
            var items = new BindingList<KeyValuePair<string, string>>();
            items.Add(new KeyValuePair<string, string>("movej", "movej"));
            items.Add(new KeyValuePair<string, string>("movel", "movel"));
            dropdown_motionType.DataSource = items;
            dropdown_motionType.ValueMember = "Key";
            dropdown_motionType.DisplayMember = "Value";
        }

        private void updateUISettings()
        {
            txtBox_TCPx.Text = Convert.ToString(UrSettings.TCP[0]);
            txtBox_TCPy.Text = Convert.ToString(UrSettings.TCP[1]);
            txtBox_TCPz.Text = Convert.ToString(UrSettings.TCP[2]);
            txtBox_TCPrx.Text = Convert.ToString(UrSettings.TCP[3]);
            txtBox_TCPry.Text = Convert.ToString(UrSettings.TCP[4]);
            txtBox_TCPrz.Text = Convert.ToString(UrSettings.TCP[5]);

            txtBox_AlignedPos_x.Text = Convert.ToString(UrSettings.alingedPos[0]);
            txtBox_AlignedPos_y.Text = Convert.ToString(UrSettings.alingedPos[1]);
            txtBox_AlignedPos_z.Text = Convert.ToString(UrSettings.alingedPos[2]);
            txtBox_AlignedPos_rx.Text = Convert.ToString(UrSettings.alingedPos[3]);
            txtBox_AlignedPos_ry.Text = Convert.ToString(UrSettings.alingedPos[4]);
            txtBox_AlignedPos_rz.Text = Convert.ToString(UrSettings.alingedPos[5]);

            if (UrSettings.MotionType == "movej"){
                dropdown_motionType.SelectedIndex = 0;
            }
            if (UrSettings.MotionType == "movel"){
                dropdown_motionType.SelectedIndex = 1;
            }

            txtBox_time.Text = Convert.ToString(UrSettings.TimeKinematics);
            txtBox_Payload.Text = Convert.ToString(UrSettings.PayLoad);
            txtBox_IP.Text = UrSettings.IP;
            //switch_LogFile.Value = Convert.ToBoolean(UrSettings.writeLogFile);
            //switch_UrScript.Value = Convert.ToBoolean(UrSettings.writeUrScriptFile);
            //switch_Data.Value = Convert.ToBoolean(UrSettings.writeDataFile);

            switch_LogFile.Checked = Convert.ToBoolean(UrSettings.writeLogFile);
            switch_UrScript.Checked = Convert.ToBoolean(UrSettings.writeUrScriptFile);
            switch_Data.Checked = Convert.ToBoolean(UrSettings.writeDataFile);
        }

        private void loadSettings()
        {
            if (File.Exists("settings.txt"))
            {
                try
                {
                    var dictionary = File.ReadAllLines("settings.txt")
                    .Select(l => l.Split(new[] { '=' }))
                    .ToDictionary(s => s[0].Trim(), s => s[1].Trim());

                    UrSettings.hostIPAddress = dictionary["IP Address"];
                    UrSettings.payLoadMass = Convert.ToDouble(dictionary["Phantom Weight [Kg]"]);
                    UrSettings.tcp[0] = Convert.ToDouble(dictionary["TCPX [mm]"]);
                    UrSettings.tcp[1] = Convert.ToDouble(dictionary["TCPY [mm]"]);
                    UrSettings.tcp[2] = Convert.ToDouble(dictionary["TCPZ [mm]"]);
                    UrSettings.tcp[3] = Convert.ToDouble(dictionary["TCPRx [rad]"]);
                    UrSettings.tcp[4] = Convert.ToDouble(dictionary["TCPRy [rad]"]);
                    UrSettings.tcp[5] = Convert.ToDouble(dictionary["TCPRz [rad]"]);
                    UrSettings.motionType = dictionary["Motion Type"];
                    UrSettings.timeKinematics = Convert.ToDouble(dictionary["Time [s]"]);
                    UrSettings.writeLogFile = Convert.ToBoolean(dictionary["Output Log File"]);
                    UrSettings.writeUrScriptFile = Convert.ToBoolean(dictionary["Output UrScript File"]);
                    UrSettings.writeDataFile = Convert.ToBoolean(dictionary["Output Data File"]);
                    UrSettings.alingedPos[0] = Convert.ToDouble(dictionary["Aligned PositionX [mm]"]);
                    UrSettings.alingedPos[1] = Convert.ToDouble(dictionary["Aligned PositionY [mm]"]);
                    UrSettings.alingedPos[2] = Convert.ToDouble(dictionary["Aligned PositionZ [mm]"]);
                    UrSettings.alingedPos[3] = Convert.ToDouble(dictionary["Aligned PositionRx [rad]"]);
                    UrSettings.alingedPos[4] = Convert.ToDouble(dictionary["Aligned PositionRy [rad]"]);
                    UrSettings.alingedPos[5] = Convert.ToDouble(dictionary["Aligned PositionRz [rad]"]);
                }
                catch (Exception excep)
                {
                    UpdateStatusBarMessage.ShowStatusMessage("Error loading settings from settings.txt");
                    Logger.addToLogFile("Error loading settings from settings.txt");
                }
            }
        }

        public void SaveSettings()
        {
            string[] lines = new string[19];

            lines[0] = "IP Address = " + UrSettings.hostIPAddress.ToString();
            lines[1] = "Phantom Weight [Kg] = " + UrSettings.payLoadMass.ToString();
            lines[2] = "TCPX [mm] = " + UrSettings.tcp[0].ToString();
            lines[3] = "TCPY [mm] = " + UrSettings.tcp[1].ToString();
            lines[4] = "TCPZ [mm] = " + UrSettings.tcp[2].ToString();
            lines[5] = "TCPRx [rad] = " + UrSettings.tcp[3].ToString();
            lines[6] = "TCPRy [rad] = " + UrSettings.tcp[4].ToString();
            lines[7] = "TCPRz [rad] = " + UrSettings.tcp[5].ToString();
            lines[8] = "Motion Type = " + UrSettings.motionType;
            lines[9] = "Time [s] = " + UrSettings.timeKinematics;
            lines[10] = "Output Log File = " + UrSettings.writeLogFile.ToString();
            lines[11] = "Output UrScript File = " + UrSettings.writeUrScriptFile.ToString();
            lines[12] = "Output Data File = " + UrSettings.writeDataFile.ToString();
            lines[13] = "Aligned PositionX [mm] = " + UrSettings.alingedPos[0].ToString();
            lines[14] = "Aligned PositionY [mm] = " + UrSettings.alingedPos[1].ToString();
            lines[15] = "Aligned PositionZ [mm] = " + UrSettings.alingedPos[2].ToString();
            lines[16] = "Aligned PositionRx [rad] = " + UrSettings.alingedPos[3].ToString();
            lines[17] = "Aligned PositionRy [rad] = " + UrSettings.alingedPos[4].ToString();
            lines[18] = "Aligned PositionRz [rad] = " + UrSettings.alingedPos[5].ToString();


            using (StreamWriter outputFile = new StreamWriter("settings.txt"))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }

        private void txtBox_TCPx_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_TCPx.Text != "")
            {
                double value;

                if (Double.TryParse(txtBox_TCPx.Text, out value))
                {
                    UrSettings.tcp[0] = value / 1000;
                }
            }
        }

        private void txtBox_TCPy_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_TCPy.Text != "")
            {
                double value;

                if (Double.TryParse(txtBox_TCPy.Text, out value))
                {
                    UrSettings.tcp[1] = value / 1000;
                }
            }
        }

        private void txtBox_TCPz_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_TCPz.Text != "")
            {
                double value;

                if (Double.TryParse(txtBox_TCPz.Text, out value))
                {
                    UrSettings.tcp[2] = value / 1000;
                }              
            }
        }

        private void txtBox_TCPrx_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_TCPrx.Text != "")
            {
                double value;

                if (Double.TryParse(txtBox_TCPrx.Text, out value))
                {
                    UrSettings.tcp[3] = value;
                }  
            }
        }

        private void txtBox_TCPry_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_TCPry.Text != "")
            {
                double value;

                if (Double.TryParse(txtBox_TCPry.Text, out value))
                {
                    UrSettings.tcp[4] = value;
                }                
            }
        }

        private void txtBox_TCPrz_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_TCPrz.Text != "")
            {
                double value;

                if (Double.TryParse(txtBox_TCPrz.Text, out value))
                {
                    UrSettings.tcp[5] = value;
                }    
            }
        }

        private void txtBox_Payload_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_Payload.Text != "")
            {
                double value;

                if (Double.TryParse(txtBox_Payload.Text, out value))
                {
                    UrSettings.payLoadMass = value;
                }            
            }
        }

        private void switch_LogFile_Click(object sender, EventArgs e)
        {
            //bool val = switch_LogFile.Value;
            bool val = switch_LogFile.Checked;


            if (val == true)
            {
                UrSettings.writeLogFile = true; 
            }
            if (val == false)
            {
                UrSettings.writeLogFile = false; 
            }
        }

        private void switch_UrScript_Click(object sender, EventArgs e)
        {
            // bool val = switch_UrScript.Value;
            bool val = switch_UrScript.Checked;

            if (val == true)
            {
                UrSettings.writeUrScriptFile = true;
            }
            if (val == false)
            {
                UrSettings.writeUrScriptFile = false;
            }
        }

        private void switch_Data_Click(object sender, EventArgs e)
        {
            // bool val = switch_Data.Value;
            bool val = switch_Data.Checked;

            if (val == true)
            {
                UrSettings.writeDataFile = true;
 
            }
            if (val == false)
            {
                UrSettings.writeDataFile = false;

            }
        }

        private void textbox_IP_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtBox_IP.Text, "[^0-9.-]"))
            {
                txtBox_IP.Text = txtBox_IP.Text.Remove(txtBox_IP.Text.Length - 1);
            }

            if (txtBox_IP.Text != "")
            {
                UrSettings.IP = txtBox_IP.Text;
            }
        }

       

        private void dropdown_motionType_onItemSelected(object sender, EventArgs e)
        {
            //string value = dropdown_motionType.selectedValue;
            string value = dropdown_motionType.Text;

            UrSettings.motionType = value;
        }

        private void txtBox_AlignedPos_x_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_AlignedPos_x.Text != "")
            {
                double value;

                if (Double.TryParse(txtBox_AlignedPos_x.Text, out value))
                {
                    UrSettings.alingedPos[0] = value / 1000;
                }
            }
        }

        private void txtBox_AlignedPos_y_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_AlignedPos_y.Text != "")
            {
                double value;

                if (Double.TryParse(txtBox_AlignedPos_y.Text, out value))
                {
                    UrSettings.alingedPos[1] = value / 1000;
                }
            }
        }

        private void txtBox_AlignedPos_z_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_AlignedPos_z.Text != "")
            {
                double value;

                if (Double.TryParse(txtBox_AlignedPos_z.Text, out value))
                {
                    UrSettings.alingedPos[2] = value / 1000;
                }
            }
        }

        private void txtBox_AlignedPos_rx_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_AlignedPos_rx.Text != "")
            {
                double value;

                if (Double.TryParse(txtBox_AlignedPos_rx.Text, out value))
                {
                    UrSettings.alingedPos[3] = value;
                }
            }
        }

        private void txtBox_AlignedPos_ry_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_AlignedPos_ry.Text != "")
            {
                double value;

                if (Double.TryParse(txtBox_AlignedPos_ry.Text, out value))
                {
                    UrSettings.alingedPos[4] = value;
                }
            }
        }

        private void txtBox_AlignedPos_rz_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_AlignedPos_rz.Text != "")
            {
                double value;

                if (Double.TryParse(txtBox_AlignedPos_rz.Text, out value))
                {
                    UrSettings.alingedPos[5] = value;
                }
            }
        }

        private void flatButton_MoveToAliPos_Click(object sender, EventArgs e)
        {
            if (UrScriptProgram.alinged.Count() != 0)
            {
                UrScriptProgram.alinged.Clear();
            }

            string TCP = "set_tcp(p[0, 0, 0, 0, 0, 0])";

            UrScriptProgram.alinged.Add("def goAlignedPos():");
            UrScriptProgram.alinged.Add(TCP);
            UrScriptProgram.alinged.Add("movej(p[" + UrSettings.alingedPos[0].ToString() + ", " + UrSettings.alingedPos[1].ToString() + ", " + UrSettings.alingedPos[2].ToString() + ", " + UrSettings.alingedPos[3].ToString() + ", " + UrSettings.alingedPos[4].ToString() + ", " + UrSettings.alingedPos[5].ToString() + "]" + ", " + "a = " + "0.2" + ", " + "v = " + "0.2" + ")");
            UrScriptProgram.alinged.Add("end");

            urServer.sendUrScript(UrScriptProgram.alinged);
        }

        private void switch_LogFile_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label_TCPy_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel11_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel4_Click(object sender, EventArgs e)
        {

        }

        private void textbox_time_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_time.Text != "" || txtBox_time.Text !="0")
            {
                double value;

                if (Double.TryParse(txtBox_time.Text, out value))
                {
                   UrSettings.timeKinematics = value;
                }
            }

            //string s = "Hi";
            //UrSettings.timeKinematics = Convert.ToDouble(txtBox_time.Text);
        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label_timeUnits_Click(object sender, EventArgs e)
        {

        }

        private void label_time_Click(object sender, EventArgs e)
        {

        }
    }
}
