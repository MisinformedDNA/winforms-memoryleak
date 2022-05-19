using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryLeak
{
    public partial class BaseForm : Form
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        protected BaseForm()
        {
            InitializeComponent();
        }

        public CancellationToken GetCancellationToken() => _cancellationTokenSource.Token;

        protected async Task ExecuteAsync(Func<Task> func)
        {
            try
            {
                await func();
            }
            catch (OperationCanceledException)
            {
                // Operation cancelled. OK to ignore exceptions.
            }
            catch (Exception) when (IsDisposed)
            {
                // Form is disposed. OK to ignore exceptions.
                // Often `ObjectDisposedException`, but not always.  
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _cancellationTokenSource.Cancel();
            base.OnFormClosing(e);
        }
    }
}
