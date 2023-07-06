using System.Text;

namespace ShaderAttack
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
                             Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                             //处理非UI线程异常
                 AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.Run(new Form1());
        }
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
             string str = GetExceptionMsg(e.Exception, e.ToString());
             MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
 
         static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
         {
             string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
             MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
        static string GetExceptionMsg(Exception ex, string backStr)
         {
                 StringBuilder sb = new StringBuilder();
                 sb.AppendLine("****************************异常文本****************************");
                 sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
                 if (ex != null)
                    {
                         sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                        sb.AppendLine("【异常信息】：" + ex.Message);
                         sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
                    }
                 else
                    {
                        sb.AppendLine("【未处理异常】：" + backStr);
                    }
               sb.AppendLine("***************************************************************");
                 return sb.ToString();
            }
    }
}