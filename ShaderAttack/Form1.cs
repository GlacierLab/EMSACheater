using Binarysharp.MemoryManagement;
using Gma.System.MouseKeyHook;
using System.Buffers.Text;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace ShaderAttack
{
    public partial class Form1 : Form
    {
        private Process? GameProcess;
        private MemorySharp? MSharp;
        private IKeyboardMouseEvents? m_GlobalHook;
        private string Hearts = "";
        private IntPtr? EnergyAddress;

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
                MSharp[0x025A6854, false].Write(Int32.Parse(Hearts));
                Hearts = "";
                Beep((int)Music.Do2, 800);
            }
            if (e.KeyChar == 113)
            {
                Beep((int)Music.Re, 150);
                FullEnergy();
                Beep((int)Music.Re, 500);
            }
            if (e.KeyChar == 81)
            {
                EnergyAddress = null;
                Beep((int)Music.Re, 150);
                FullEnergy();
                Beep((int)Music.Re, 500);
            }
        }
        private int FullEnergy()
        {
            bool success = false;
            if (EnergyAddress != null)
            {
                try
                {
                    double Current = MSharp.Read<double>((nint)EnergyAddress, false);
                    if (Current <= 1000 && Current >= 0)
                    {
                        Console.WriteLine("Old Address Current Value:" + Current);
                        MSharp.Write((nint)EnergyAddress, Convert.ToDouble(1000), false);
                        success = true;
                        return 1;
                    }
                    else
                    {
                        throw new Exception("Ridiculous Number!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Old Address Unavaliable!");
                }
            }
            try
            {
                IntPtr address = MSharp.Read<IntPtr>(MSharp["Em_android.exe"].BaseAddress + 0x00034f28, false);
                Console.WriteLine(Convert.ToString(address, 16));
                address = MSharp.Read<IntPtr>(address + 0x1c, false);
                Console.WriteLine(Convert.ToString(address, 16));
                address = MSharp.Read<IntPtr>(address + 0x30, false);
                Console.WriteLine(Convert.ToString(address, 16));
                address = MSharp.Read<IntPtr>(address + 0x4, false);
                Console.WriteLine(Convert.ToString(address, 16));
                address = MSharp.Read<IntPtr>(address + 0xc0, false);
                Console.WriteLine(Convert.ToString(address, 16));
                address = MSharp.Read<IntPtr>(address + 0x2c, false);
                Console.WriteLine(Convert.ToString(address, 16));
                double Current = MSharp.Read<double>(address + 0xf0, false);
                if (Current <= 1000 && Current >= 0)
                {
                    Console.WriteLine("Current Value:" + Current);
                    MSharp.Write(address + 0xf0, Convert.ToDouble(1000), false);
                    success = true;
                    EnergyAddress = address + 0xf0;
                }
                else
                {
                    throw new Exception("Ridiculous Number!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (!success)
            {
                return FullEnergy();
            }
            else
            {
                return 1;
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