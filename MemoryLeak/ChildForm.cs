using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MemoryLeak.Data.Repositories;

namespace MemoryLeak
{
    public partial class ChildForm : BaseForm
    {
        private readonly MovieRepository _repository;

        public ChildForm(MovieRepository repository)
        {
            InitializeComponent();
            _repository = repository;
        }

        protected override async void OnLoad(EventArgs e)
        {
            await ExecuteAsync(async () => await OnLoadInternalAsync(GetCancellationToken()));
            base.OnLoad(e);
        }

        private async Task OnLoadInternalAsync(CancellationToken cancellationToken)
        {
            Thread.Sleep(1000);
            var tasks = new List<Task> { _repository.Load(cancellationToken) };
            Thread.Sleep(1000);

            await Task.WhenAll(tasks);

        }
    }
}
