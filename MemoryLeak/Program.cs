using MemoryLeak.Data;
using MemoryLeak.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows.Forms;

namespace MemoryLeak
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services
                        .AddTransient<MainForm>()
                        .AddTransient<ChildForm>()
                        .AddDbContext<MyAppContext>(o => o.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WinformsMemoryLeak"), ServiceLifetime.Transient)
                        .AddTransient<MovieRepository>();
                });
            var host = builder.Build();

            AppDomain.CurrentDomain.UnhandledException += (_, e) => LogUnhandledUIException((Exception)e.ExceptionObject, false);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (_, t) => LogUnhandledUIException(t.Exception, true);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using var serviceScope = host.Services.CreateScope();
            var services = serviceScope.ServiceProvider;

            Application.Run(services.GetRequiredService<MainForm>());
        }


        private static void LogUnhandledUIException(Exception exception, bool showPopup)
        {
            if (!showPopup)
                return;

            var result = ShowThreadExceptionDialog(exception);
            if (result == DialogResult.Abort)
                Application.Exit();
        }

        private static DialogResult ShowThreadExceptionDialog(Exception e)
        {
            var errorMsg = $"An application error occurred:\n\n{e.Message}";
            return MessageBox.Show(errorMsg, "Application UI Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
        }
    }
}

