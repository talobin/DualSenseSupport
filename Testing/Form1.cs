using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSProject;
using DualSenseSupport;
using Microsoft.SqlServer.Server;
using System.Configuration;

namespace Testing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // AllocConsole();

            DualSenseSupport.Devices.Init();


            joystickComboIndex.DisplayMember = "Text";
            joystickComboIndex.ValueMember = "Value";
            var list = new List<object>();
            for (int i = 0; i < DualSenseSupport.Devices.GetDeviceCount(); i++)
            {
                list.Add(new {Text = $"Input #{i}", Value = i});
            }

            joystickComboIndex.DataSource = list;
            //setModeComboBox.SelectedIndex = 0;
            //pulseModeComboBox.SelectedIndex = 0;





            var leftTriggerModeList = new BindingList<object>();
            leftTriggerModeList.Add(new {Text = "Off", Value = DsTrigger.Modes.Off});
            leftTriggerModeList.Add(new {Text = "Rigid", Value = DsTrigger.Modes.Rigid});
            leftTriggerModeList.Add(new {Text = "Pulse", Value = DsTrigger.Modes.Pulse});
            leftTriggerModeList.Add(new {Text = "Rigid + Extra1", Value = DsTrigger.Modes.Rigid_A});
            leftTriggerModeList.Add(new {Text = "Rigid + Extra2", Value = DsTrigger.Modes.Rigid_B});
            leftTriggerModeList.Add(new {Text = "Rigid + Extra1 + Extra 2", Value = DsTrigger.Modes.Rigid_AB});
            leftTriggerModeList.Add(new {Text = "Pulse + Extra1 ", Value = DsTrigger.Modes.Pulse_A});
            leftTriggerModeList.Add(new {Text = "Pulse + Extra2 ", Value = DsTrigger.Modes.Pulse_B});
            leftTriggerModeList.Add(new {Text = "Pulse + Extra1 + Extra 2 ", Value = DsTrigger.Modes.Pulse_AB});
            leftTriggerModeList.Add(new {Text = "GameCube ", Value = DsTrigger.Modes.GameCube});
            leftTriggerModeList.Add(new {Text = "Calibration ", Value = DsTrigger.Modes.Calibration});
            var rightTriggerModeList = new List<object>();
            rightTriggerModeList.Add(new {Text = "Off", Value = DsTrigger.Modes.Off});
            rightTriggerModeList.Add(new {Text = "Rigid", Value = DsTrigger.Modes.Rigid});
            rightTriggerModeList.Add(new {Text = "Pulse", Value = DsTrigger.Modes.Pulse});
            rightTriggerModeList.Add(new {Text = "Rigid + Extra1", Value = DsTrigger.Modes.Rigid_A});
            rightTriggerModeList.Add(new {Text = "Rigid + Extra2", Value = DsTrigger.Modes.Rigid_B});
            rightTriggerModeList.Add(new {Text = "Rigid + Extra1 + Extra 2", Value = DsTrigger.Modes.Rigid_AB});
            rightTriggerModeList.Add(new {Text = "Pulse + Extra1 ", Value = DsTrigger.Modes.Pulse_A});
            rightTriggerModeList.Add(new {Text = "Pulse + Extra2 ", Value = DsTrigger.Modes.Pulse_B});
            rightTriggerModeList.Add(new {Text = "Pulse + Extra1 + Extra 2 ", Value = DsTrigger.Modes.Pulse_AB});
            rightTriggerModeList.Add(new {Text = "GameCube ", Value = DsTrigger.Modes.GameCube});
            rightTriggerModeList.Add(new {Text = "Calibration ", Value = DsTrigger.Modes.Calibration});
            comboLeftMode.DisplayMember = "Text";
            comboLeftMode.ValueMember = "Value";
            comboLeftMode.DataSource = leftTriggerModeList;
            comboRightMode.DisplayMember = "Text";
            comboRightMode.ValueMember = "Value";
            comboRightMode.DataSource = rightTriggerModeList;
            
            // Load Config From File
            loadFromConfigFile();

        }

        /**
         * Function to load all settings from App.config file
         * Applies all the config to the form
         **/
        private void loadFromConfigFile()
        {
            // LeftTrigger Settings
            string leftTriggerMode = ConfigurationManager.AppSettings["leftTriggerMode"];
            comboLeftMode.SelectedIndex = Int32.Parse(leftTriggerMode);
            string lv1 = ConfigurationManager.AppSettings["l_v_1"];
            l_v_1.Value = Int32.Parse(lv1);
            string lv2 = ConfigurationManager.AppSettings["l_v_2"];
            l_v_2.Value = Int32.Parse(lv2);
            string lv3 = ConfigurationManager.AppSettings["l_v_3"];
            l_v_3.Value = Int32.Parse(lv3);
            string lv4 = ConfigurationManager.AppSettings["l_v_4"];
            l_v_4.Value = Int32.Parse(lv4);
            string lv5 = ConfigurationManager.AppSettings["l_v_5"];
            l_v_5.Value = Int32.Parse(lv5);
            string lv6 = ConfigurationManager.AppSettings["l_v_6"];
            l_v_6.Value = Int32.Parse(lv6);
            string lv7 = ConfigurationManager.AppSettings["l_v_7"];
            l_v_7.Value = Int32.Parse(lv7);

            // RightTrigger Settings
            string rightTriggerMode = ConfigurationManager.AppSettings["rightTriggerMode"];
            comboRightMode.SelectedIndex = Int32.Parse(rightTriggerMode);
            string rv1 = ConfigurationManager.AppSettings["r_v_1"];
            r_v_1.Value = Int32.Parse(rv1);
            string rv2 = ConfigurationManager.AppSettings["r_v_2"];
            r_v_2.Value = Int32.Parse(rv2);
            string rv3 = ConfigurationManager.AppSettings["r_v_3"];
            r_v_3.Value = Int32.Parse(rv3);
            string rv4 = ConfigurationManager.AppSettings["r_v_4"];
            r_v_4.Value = Int32.Parse(rv4);
            string rv5 = ConfigurationManager.AppSettings["r_v_5"];
            r_v_5.Value = Int32.Parse(rv5);
            string rv6 = ConfigurationManager.AppSettings["r_v_6"];
            r_v_6.Value = Int32.Parse(rv6);
            string rv7 = ConfigurationManager.AppSettings["r_v_7"];
            r_v_7.Value = Int32.Parse(rv7);

            // Color Settings
            string setMode = ConfigurationManager.AppSettings["setMode"];
            setModeComboBox.SelectedIndex = Int32.Parse(setMode);
            string pulseMode = ConfigurationManager.AppSettings["pulseMode"];
            pulseModeComboBox.SelectedIndex = Int32.Parse(pulseMode);

            // LEDs Settings
            string playerNumber = ConfigurationManager.AppSettings["playerNumber"];
            playerNumberTrackBar.Value = Int32.Parse(playerNumber);
            labelPlayerNumber.Text = playerNumber;
            string brightness = ConfigurationManager.AppSettings["brightness"];
            brightnessTrackBar.Value = Int32.Parse(brightness);

            // Motors
            string overallMotors = ConfigurationManager.AppSettings["overallMotors"];
            overallMotorsTrackBar.Value = Int32.Parse(overallMotors);

            // Colors

            string red = ConfigurationManager.AppSettings["red"];
            trackR.Value = Int32.Parse(red);
            string green = ConfigurationManager.AppSettings["green"];
            trackG.Value = Int32.Parse(green);
            string blue = ConfigurationManager.AppSettings["blue"];
            trackB.Value = Int32.Parse(blue);
            ChangeImage();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void button1_Click(object sender, EventArgs e)
        {
            var result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (SelectedDevice().HasValue)
                {
                    Devices.GetDevice(SelectedDevice().Value)
                        .SetColor(colorDialog1.Color);
                    ChangeImageFromSelector();
                }
            }
        }

        private int? SelectedDevice()
        {
            return (int?) joystickComboIndex.SelectedValue;
        }

        private void trackR_Scroll(object sender, EventArgs e)
        {
            var color = Color.FromArgb(trackR.Value, 0, 0);

            trackR.BackColor = color;
            ChangeImage();
        }

        private void ChangeImageFromSelector()
        {
            trackR.Value = colorDialog1.Color.R;
            trackG.Value = colorDialog1.Color.G;
            trackB.Value = colorDialog1.Color.B;
            ChangeImage();
        }

        private void ChangeImage()
        {
            pictureBox1.BackColor = Color.FromArgb(trackR.Value, trackG.Value, trackB.Value);
            if (SelectedDevice().HasValue)
                Devices.GetDevice(SelectedDevice().Value)
                    .SetColor(pictureBox1.BackColor);

            // Save to Config File
            saveToConfigFile("red", trackR.Value);
            saveToConfigFile("green", trackG.Value);
            saveToConfigFile("blue", trackB.Value);
        }

        private void trackG_Scroll(object sender, EventArgs e)
        {
            var color = Color.FromArgb(0, trackG.Value, 0);

            trackG.BackColor = color;
            ChangeImage();
        }

        private void trackB_Scroll(object sender, EventArgs e)
        {
            var color = Color.FromArgb(0, 0, trackB.Value);

            trackB.BackColor = color;
            ChangeImage();
        }

        private void playerNumberTrackBar_Scroll(object sender, EventArgs e)
        {
            labelPlayerNumber.Text = playerNumberTrackBar.Value.ToString();
            if (SelectedDevice().HasValue)
                Devices.GetDevice(SelectedDevice().Value)
                    .SetPlayerNumber(playerNumberTrackBar.Value);

            //Save to Config
            saveToConfigFile("playerNumber", playerNumberTrackBar.Value);
        }

        private void brightnessTrackBar_Scroll(object sender, EventArgs e)
        {
            if (SelectedDevice().HasValue)
                Devices.GetDevice(SelectedDevice().Value)
                    .SetLightBrightness(brightnessTrackBar.Value);

            //Save to Config
            saveToConfigFile("brightness", brightnessTrackBar.Value);
        }


        private void setModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setModeComboBox.SelectedIndex == 0)
            {
                if (SelectedDevice().HasValue)
                    Devices.GetDevice(SelectedDevice().Value)
                        .SetLedMode(DSLight.LedOptions.None);
            }

            if (setModeComboBox.SelectedIndex == 1)
            {
                if (SelectedDevice().HasValue)
                    Devices.GetDevice(SelectedDevice().Value)
                        .SetLedMode(DSLight.LedOptions.PlayerLedBrightnes);
            }

            if (setModeComboBox.SelectedIndex == 2)
            {
                if (SelectedDevice().HasValue)
                    Devices.GetDevice(SelectedDevice().Value)
                        .SetLedMode(DSLight.LedOptions.UninterrumpableLed);
            }

            if (setModeComboBox.SelectedIndex == 3)
            {
                if (SelectedDevice().HasValue)
                    Devices.GetDevice(SelectedDevice().Value)
                        .SetLedMode(DSLight.LedOptions.Both);
            }

            //Save to Config File
            saveToConfigFile("setMode", setModeComboBox.SelectedIndex);

        }

        /**
         * Helper function to save any int value to App.config
         **/
        private void saveToConfigFile(String key,int value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value.ToString());
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void pulseModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pulseModeComboBox.SelectedIndex == 0)
            {
                if (SelectedDevice().HasValue)
                    Devices.GetDevice(SelectedDevice().Value)
                        .SetPulseMode(DSLight.PulseOptions.None);
            }

            if (pulseModeComboBox.SelectedIndex == 1)
            {
                trackB.Value=0;
                trackR.Value=0;
                trackG.Value=0;
                if (SelectedDevice().HasValue)
                    Devices.GetDevice(SelectedDevice().Value)
                        .SetPulseMode(DSLight.PulseOptions.FadeBlue);
            }

            if (pulseModeComboBox.SelectedIndex == 2)
            {
                trackB.Value=0;
                trackR.Value=0;
                trackG.Value=0;
                if (SelectedDevice().HasValue)
                    Devices.GetDevice(SelectedDevice().Value)
                        .SetPulseMode(DSLight.PulseOptions.FadeOut);
            }

            //Save to Config File
            saveToConfigFile("pulseMode", pulseModeComboBox.SelectedIndex);
        }

        private void comboLeftMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLeftTrigger();
        }

        private void l_v_1_Scroll(object sender, EventArgs e)
        {
            UpdateLeftTrigger();
        }

        private void UpdateLeftTrigger()
        {
            onChangeTrigger = true;
            l_n_1.Value = l_v_1.Value;
            l_n_2.Value = l_v_2.Value;
            l_n_3.Value = l_v_3.Value;
            l_n_4.Value = l_v_4.Value;
            l_n_5.Value = l_v_5.Value;
            l_n_6.Value = l_v_6.Value;
            l_n_7.Value = l_v_7.Value;
            onChangeTrigger = false;
            /*l_l_1.Text = l_v_1.Value.ToString();
            l_l_2.Text = l_v_2.Value.ToString();
            l_l_3.Text = l_v_3.Value.ToString();
            l_l_4.Text = l_v_4.Value.ToString();
            l_l_5.Text = l_v_5.Value.ToString();
            l_l_6.Text = l_v_6.Value.ToString();
            l_l_7.Text = l_v_7.Value.ToString();*/

            if (SelectedDevice().HasValue)
                Devices.GetDevice(SelectedDevice().Value)
                    .SetTriggerLeft(
                        new DsTrigger(
                            (DsTrigger.Modes) comboLeftMode.SelectedValue,
                            l_v_1.Value,
                            l_v_2.Value,
                            l_v_3.Value,
                            l_v_4.Value,
                            l_v_5.Value,
                            l_v_6.Value,
                            l_v_7.Value
                        ));

            //Save config to file
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings.Remove("leftTriggerMode");
            config.AppSettings.Settings.Add("leftTriggerMode", comboLeftMode.SelectedIndex.ToString());
            config.Save(ConfigurationSaveMode.Modified);
            config.AppSettings.Settings.Remove("l_v_1");
            config.AppSettings.Settings.Add("l_v_1", l_v_1.Value.ToString());
            config.AppSettings.Settings.Remove("l_v_2");
            config.AppSettings.Settings.Add("l_v_2", l_v_2.Value.ToString());
            config.AppSettings.Settings.Remove("l_v_3");
            config.AppSettings.Settings.Add("l_v_3", l_v_3.Value.ToString());
            config.AppSettings.Settings.Remove("l_v_4");
            config.AppSettings.Settings.Add("l_v_4", l_v_4.Value.ToString());
            config.AppSettings.Settings.Remove("l_v_5");
            config.AppSettings.Settings.Add("l_v_5", l_v_5.Value.ToString());
            config.AppSettings.Settings.Remove("l_v_6");
            config.AppSettings.Settings.Add("l_v_6", l_v_6.Value.ToString());
            config.AppSettings.Settings.Remove("l_v_7");
            config.AppSettings.Settings.Add("l_v_7", l_v_7.Value.ToString());
            config.Save(ConfigurationSaveMode.Modified);


        }

        private void l_v_2_Scroll(object sender, EventArgs e)
        {
            UpdateLeftTrigger();
        }


        private void l_v_4_Scroll(object sender, EventArgs e)
        {
            UpdateLeftTrigger();
        }

        private void l_v_5_Scroll(object sender, EventArgs e)
        {
            UpdateLeftTrigger();
        }

        private void l_v_7_Scroll(object sender, EventArgs e)
        {
            UpdateLeftTrigger();
        }

        private void l_v_6_Scroll(object sender, EventArgs e)
        {
            UpdateLeftTrigger();
        }

        private void l_v_3_Scroll(object sender, EventArgs e)
        {
            UpdateLeftTrigger();
        }

        private void comboRightMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRightTrigger();
        }

        public void UpdateRightTrigger()
        {
            onChangeTrigger = true;
            r_n_1.Value = r_v_1.Value;
            r_n_2.Value = r_v_2.Value;
            r_n_3.Value = r_v_3.Value;
            r_n_4.Value = r_v_4.Value;
            r_n_5.Value = r_v_5.Value;
            r_n_6.Value = r_v_6.Value;
            r_n_7.Value = r_v_7.Value;
            onChangeTrigger = false;
            /*r_l_1.Text = r_v_1.Value.ToString();
            r_l_2.Text = r_v_2.Value.ToString();
            r_l_3.Text = r_v_3.Value.ToString();
            r_l_4.Text = r_v_4.Value.ToString();
            r_l_5.Text = r_v_5.Value.ToString();
            r_l_6.Text = r_v_6.Value.ToString();
            r_l_7.Text = r_v_7.Value.ToString();*/

            if (SelectedDevice().HasValue)
                Devices.GetDevice(SelectedDevice().Value)
                    .SetTriggerRight(
                        new DsTrigger(
                            (DsTrigger.Modes)comboRightMode.SelectedValue,
                            r_v_1.Value,
                            r_v_2.Value,
                            r_v_3.Value,
                            r_v_4.Value,
                            r_v_5.Value,
                            r_v_6.Value,
                            r_v_7.Value
                        ));

            //Save config to file
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings.Remove("rightTriggerMode");
            config.AppSettings.Settings.Add("rightTriggerMode", comboRightMode.SelectedIndex.ToString());
            config.AppSettings.Settings.Remove("r_v_1");
            config.AppSettings.Settings.Add("r_v_1", r_v_1.Value.ToString());
            config.AppSettings.Settings.Remove("r_v_2");
            config.AppSettings.Settings.Add("r_v_2", r_v_2.Value.ToString());
            config.AppSettings.Settings.Remove("r_v_3");
            config.AppSettings.Settings.Add("r_v_3", r_v_3.Value.ToString());
            config.AppSettings.Settings.Remove("r_v_4");
            config.AppSettings.Settings.Add("r_v_4", r_v_4.Value.ToString());
            config.AppSettings.Settings.Remove("r_v_5");
            config.AppSettings.Settings.Add("r_v_5", r_v_5.Value.ToString());
            config.AppSettings.Settings.Remove("r_v_6");
            config.AppSettings.Settings.Add("r_v_6", r_v_6.Value.ToString());
            config.AppSettings.Settings.Remove("r_v_7");
            config.AppSettings.Settings.Add("r_v_7", r_v_7.Value.ToString());
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void r_v_1_Scroll(object sender, EventArgs e)
        {
            UpdateRightTrigger();
        }

        private void r_v_2_Scroll(object sender, EventArgs e)
        {
            UpdateRightTrigger();
        }

        private void r_v_3_Scroll(object sender, EventArgs e)
        {
            UpdateRightTrigger();
        }

        private void r_v_4_Scroll(object sender, EventArgs e)
        {
            UpdateRightTrigger();
        }


        private void r_v_5_Scroll(object sender, EventArgs e)
        {
            UpdateRightTrigger();
        }

        private void r_v_6_Scroll(object sender, EventArgs e)
        {
            UpdateRightTrigger();
        }

        private void r_v_7_Scroll(object sender, EventArgs e)
        {
            UpdateRightTrigger();
        }

        private void overallMotorsTrackBar_Scroll_1(object sender, EventArgs e)
        {
            if (SelectedDevice().HasValue)
                Devices.GetDevice(SelectedDevice().Value)
                    .SetOveralMotors(overallMotorsTrackBar.Value);

            //Save to Config File
            saveToConfigFile("overallMotors", overallMotorsTrackBar.Value);
        }

        private bool onChangeTrigger = false;

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (onChangeTrigger) return;
            l_v_1.Value = (int) l_n_1.Value;
        }

        private void l_n_2_ValueChanged(object sender, EventArgs e)
        {
            if (onChangeTrigger) return;
            l_v_2.Value = (int) l_n_2.Value;
        }

        private void l_n_3_ValueChanged(object sender, EventArgs e)
        {
            if (onChangeTrigger) return;
            l_v_3.Value = (int) l_n_3.Value;
        }

        private void l_n_4_ValueChanged(object sender, EventArgs e)
        {
            if (onChangeTrigger) return;
            l_v_4.Value = (int) l_n_4.Value;
        }

        private void l_n_5_ValueChanged(object sender, EventArgs e)
        {
            if (onChangeTrigger) return;
            l_v_5.Value = (int) l_n_5.Value;
        }

        private void l_n_6_ValueChanged(object sender, EventArgs e)
        {
            if (onChangeTrigger) return;
            l_v_6.Value = (int) l_n_6.Value;
        }

        private void l_n_7_ValueChanged(object sender, EventArgs e)
        {
            if (onChangeTrigger) return;
            l_v_7.Value = (int) l_n_7.Value;
        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            if (onChangeTrigger) return;
            r_v_1.Value = (int) r_n_1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (onChangeTrigger) return;
            r_v_2.Value = (int) r_n_2.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (onChangeTrigger) return;
            r_v_3.Value = (int) r_n_3.Value;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (onChangeTrigger) return;
            r_v_4.Value =(int) r_n_4.Value;
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (onChangeTrigger) return;
            r_v_5.Value =(int) r_n_5.Value;
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            if (onChangeTrigger) return;
            r_v_6.Value =(int) r_n_6.Value;
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            if (onChangeTrigger) return;
            r_v_7.Value =(int) r_n_7.Value;
        }
    }
}