using libdebug;
using System.Net;
using System.Net.Sockets;

namespace ColdWarToolMrNiato
{
    public partial class Form1 : Form
    {
        #region Variables
        private PS4DBG PS4;
        private int pid;
        public static Socket soc;
        public static string payloadPath;
        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                PS4 = new PS4DBG(textBox1.Text);
                PS4.Connect();
                toolStripLabel2.Text = "Connected";
                toolStripLabel2.ForeColor = Color.Green;
                PS4.Notify(222, "PS4 Connected");
                MessageBox.Show("PS4 Connected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Process process in PS4.GetProcessList().processes)
                {
                    if (process.name == "eboot.bin")
                    {
                        pid = process.pid;
                        PS4.Notify(222, "Process Attached");
                        toolStripLabel5.Text = "Attached";
                        toolStripLabel5.ForeColor = Color.Green;
                        MessageBox.Show("Process Attached", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                groupBox2.Enabled = true;
            }
            else
            {
                groupBox2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                soc.ReceiveTimeout = 3000;
                soc.SendTimeout = 3000;
                soc.Connect(new IPEndPoint(IPAddress.Parse(textBox1.Text), Int32.Parse(textBox2.Text)));
                soc.SendFile(payloadPath);
                groupBox2.Enabled = true;
                MessageBox.Show("Payload Successfully injected !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            payloadPath = openFileDialog1.FileName;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            PS4.WriteMemory(pid, 0xB13DFF8, BitConverter.GetBytes((int)numericUpDown1.Value));
            PS4.WriteMemory(pid, 0xB13E01C, BitConverter.GetBytes((int)numericUpDown2.Value));
            PS4.Notify(222, "Kills changed to : " + numericUpDown1.Value);
            PS4.Notify(222, "Critical Kills changed to : " + numericUpDown2.Value);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            PS4.WriteMemory(pid, 0xB13DFF4, BitConverter.GetBytes((int)numericUpDown3.Value));
            PS4.Notify(222, "Money set to " + numericUpDown3.Value.ToString() + ", buy something to make it stick !");
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                PS4.WriteMemory(pid, 0xAA23E30, new byte[] { 0xFF, 0xFF, 0xFF });
                PS4.Notify(222, "God Mode : ON");
            }
            else
            {
                PS4.WriteMemory(pid, 0xAA23E30, new byte[] { 0x0A, 0x00, 0x00 });
                PS4.Notify(222, "God Mode : OFF");
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                PS4.WriteMemory(pid, 0xB1396B0, new byte[] { 0xFF, 0xFF, 0xFF });
                PS4.WriteMemory(pid, 0xB139640, new byte[] { 0xFF, 0xFF, 0xFF });
                PS4.WriteMemory(pid, 0xB1396B4, new byte[] { 0xFF, 0xFF, 0xFF });
                PS4.WriteMemory(pid, 0xB139648, new byte[] { 0xFF, 0xFF, 0xFF });
                PS4.Notify(222, "Unlimited Ammo : ON");
            }
            else
            {
                PS4.WriteMemory(pid, 0xB1396B0, new byte[] { 0x00, 0x00, 0x00, 0x00 });
                PS4.WriteMemory(pid, 0xB139640, new byte[] { 0x00, 0x00, 0x00, 0x00 });
                PS4.WriteMemory(pid, 0xB1396B4, new byte[] { 0x00, 0x00, 0x00, 0x00 });
                PS4.WriteMemory(pid, 0xB139648, new byte[] { 0x00, 0x00, 0x00, 0x00 });
                PS4.Notify(222, "Unlimited Ammo : OFF");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x01 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                PS4.WriteMemory(pid, 0xB139137, new byte[] { 0x40 });
                PS4.Notify(222, "Spectator Mode : ON");
            }
            else
            {
                PS4.WriteMemory(pid, 0xB139137, new byte[] { 0x20 });
                PS4.Notify(222, "Spectator Mode : OFF");
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            PS4.WriteMemory(pid, 0xB13E330, textBox3.Text);
            PS4.Notify(222, "Name changed to : " + textBox3.Text);
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            PS4.WriteMemory(pid, 0xB138380, BitConverter.GetBytes((int)numericUpDown4.Value));
            //91 Avion d'assault
            //94 ""
            //93 ""
            //97 Raid Aerien
        }
    }
}