using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MemoryLeak.Data;
using Microsoft.Extensions.DependencyInjection;

namespace MemoryLeak
{
    public partial class MainForm : BaseForm
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly MyAppContext _context;

        public MainForm(IServiceProvider serviceProvider, MyAppContext context)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _context = context;
        }

        protected override async void OnLoad(EventArgs e)
        {
            while (true)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                await Task.Delay(5000);
            }
        }

        private void OpenScopedChildMdiForm<T>() where T : Form
        {
            var scope = _serviceProvider.CreateScope();
            var form = scope.ServiceProvider.GetRequiredService<T>();

            form.FormClosed += (_, _) => scope.Dispose();

            form.Icon = this.Icon;
            form.MdiParent = this;
            form.Left = 1;
            form.Top = 1;
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenScopedChildMdiForm<ChildForm>();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 0_100_000; i++)
            {
                _context.Movies.Add(new Movie { Title = $"Movie{i}" });
            }

            await _context.SaveChangesAsync(GetCancellationToken()).ConfigureAwait(false);
        }
    }
}
