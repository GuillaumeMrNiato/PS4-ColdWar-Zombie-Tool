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
                        PS4.Notify(222, "COD Cold War RTM Tool for Zombie by MrNiato | Compatible with update 1.27");
                        PS4.Notify(222, "Twitter : @ImMrNiato");
                        toolStripLabel5.Text = "Attached";
                        toolStripLabel5.ForeColor = Color.Green;
                        groupBox3.Enabled = true;
                        groupBox4.Enabled = true;
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
                MessageBox.Show("Ok but please be sure that you already have injected PS4Debug.bin !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                godmode.Start();
                PS4.Notify(222, "God Mode : ON");
            }
            else
            {
                godmode.Stop();
                PS4.WriteMemory(pid, 0xAA23E30, new byte[] { 0x0A, 0x00, 0x00 });
                PS4.Notify(222, "God Mode : OFF");
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                ammo.Start();
                PS4.Notify(222, "Unlimited Ammo : ON");
            }
            else
            {
                ammo.Stop();
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
            if (comboBox1.SelectedIndex == 1)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x02 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 2)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x03 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 3)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x04 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 4)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x05 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 5)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x06 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 6)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x07 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 7)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x08 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 8)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x09 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 9)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x0A });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 10)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x0D });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 11)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x0E });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 12)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x10 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 13)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x11 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 14)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x12 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 15)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x13 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 16)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x14 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 17)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x15 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 18)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x18 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 19)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x1A });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 20)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x1B });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 21)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x1E });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 22)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x20 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 23)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x23 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 24)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x25 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 25)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x26 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 26)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x29 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 27)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x2B });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 28)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x2C });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 29)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x2D });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 30)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x2E });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 31)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x2F });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 32)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x31 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 33)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x32 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 34)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x34 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 35)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x35 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 36)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x36 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 37)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x37 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 38)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x38 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 39)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x39 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 40)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x3A });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 41)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x3C });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 42)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x3E });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 43)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x3F });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 44)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x40 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 45)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x42 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 46)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x43 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 47)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x44 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 48)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x45 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 49)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x46 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 50)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x47 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 51)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x48 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 52)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x49 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 53)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x4A });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 54)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x4B });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 55)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x4C });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 56)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x4D });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 57)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x4E });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 58)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x51 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 59)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x52 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 60)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x53 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 61)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x56 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 62)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x58 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 63)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x59 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 64)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x5A });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 65)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x5B });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 66)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x5D });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 67)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x61 });
                PS4.Notify(222, "Weapon Changed to : " + comboBox1.Text);
            }
            if (comboBox1.SelectedIndex == 68)
            {
                PS4.WriteMemory(pid, 0xB138380, new byte[] { 0x62 });
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
        private void godmode_Tick(object sender, EventArgs e)
        {
            PS4.WriteMemory(pid, 0xAA23E30, new byte[] { 0xFF, 0xFF, 0xFF });
        }

        private void ammo_Tick(object sender, EventArgs e)
        {
            PS4.WriteMemory(pid, 0xB1396B0, new byte[] { 0xFF, 0xFF, 0xFF });
            PS4.WriteMemory(pid, 0xB139640, new byte[] { 0xFF, 0xFF, 0xFF });
            PS4.WriteMemory(pid, 0xB1396B4, new byte[] { 0xFF, 0xFF, 0xFF });
            PS4.WriteMemory(pid, 0xB139648, new byte[] { 0xFF, 0xFF, 0xFF });
        }

        private void button10_Click(object sender, EventArgs e)
        {
            PS4.Notify(222, "PS4 will disconnect...");
            PS4.Disconnect();

            MessageBox.Show("PS4 Disconnected !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            PS4.Notify(222, "PS4 will reboot...");
            toolStripLabel2.Text = "Not Connected";
            toolStripLabel2.ForeColor = Color.Red;
            toolStripLabel5.Text = "Not Attached";
            toolStripLabel5.ForeColor = Color.Red;
            PS4.Reboot();
            MessageBox.Show("PS4 will reboot now...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripLabel8_Click(object sender, EventArgs e)
        {
        }
    }
}