using Binarysharp.MemoryManagement;
using Gma.System.MouseKeyHook;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ShaderAttack
{
    public partial class Form1 : Form
    {
        private Process? GameProcess;
        private MemorySharp? MSharp;
        private IKeyboardMouseEvents? m_GlobalHook;
        private string Hearts = "";

        [DllImport("kernel32.dll")]
        private static extern int Beep(int dwFreq, int dwDuration);

        public enum Music
        {
            Do = 523,
            Re = 587,
            Mi = 659,
            Fa = 698,
            So = 784,
            La = 880,
            Ti = 988,
            Do2 = 1046
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void OpenProcess_Click(object sender, EventArgs e)
        {
            GameProcess = null;
            switch (Process.GetProcessesByName("Em_android").Length)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        GameProcess = Process.GetProcessesByName("Em_android")[0];
                        break;
                    }
                default:
                    {
                        foreach (Process ProcessToChoose in Process.GetProcessesByName("Em_android"))
                        {
                            DialogResult dialogResult = MessageBox.Show("你要选择的进程是不是:" + ProcessToChoose.Id.ToString(), "选择进程", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                GameProcess = ProcessToChoose;
                                break;
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                continue;
                            }
                        }
                        break;
                    }
            }
            if (GameProcess != null)
            {
                OpenProcess.Text = "当前选择进程:" + GameProcess.Id.ToString();
                MSharp = new MemorySharp(GameProcess);
                ServiceSwitch.Enabled = true;
            }
            else
            {
                MessageBox.Show("未找到游戏进程！");
            }

        }
        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("KeyPress: \t{0}", (int)e.KeyChar);
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                Hearts += e.KeyChar.ToString();
                Beep((int)Music.Do, 300);
            }
            if (e.KeyChar == 13 && Hearts != "")
            {
                MSharp[0x02156854, false].Write(Int32.Parse(Hearts));
                Hearts = "";
                Beep((int)Music.Do2, 800);
            }
        }

        private void AddHeart_Click(object sender, EventArgs e)
        {
            MSharp[0x02156854, false].Write(8);
        }

        private void ServiceSwitch_Click(object sender, EventArgs e)
        {
            if (m_GlobalHook == null)
            {
                m_GlobalHook = Hook.GlobalEvents();
                m_GlobalHook.KeyPress += GlobalHookKeyPress;
                ServiceSwitch.Text = "停止服务";
            }
            else
            {
                m_GlobalHook.KeyPress -= GlobalHookKeyPress;
                m_GlobalHook.Dispose();
                m_GlobalHook = null;
                ServiceSwitch.Text = "启动服务";
            }

        }
    }
}