using Microsoft.EntityFrameworkCore;
using RobustWinForms.Data;
using RobustWinForms.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    public partial class ChildForm : BaseForm
    {
        //private readonly MyAppContext _context;
        private readonly MovieRepository _repository;

        public ChildForm(MovieRepository repository)
        {
            InitializeComponent();
            //_context = context;
            Load += ChildForm_Load;
//            Shown += (_, _) => Close();
            _repository = repository;
        }

        protected override async void OnLoad(EventArgs e)
        {
            await ExecuteAsync(async () => await OnLoadInternalAsync(GetCancellationToken()));
            //await ExecuteAsync(async () => await Task.Delay(1000, GetCancellationToken()));
            base.OnLoad(e);
        }

        private async void ChildForm_Load(object? sender, EventArgs e)
        {
//            await ExecuteAsync(async () => await Task.Delay(1000, GetCancellationToken()));
            //await ExecuteAsync(async () => await OnLoadInternalAsync(GetCancellationToken()));
//            Thread.Sleep(1000);
        }

        private async Task OnLoadInternalAsync(CancellationToken cancellationToken)
        {
            Thread.Sleep(1000);
            var tasks = new List<Task> { _repository.Load(cancellationToken) };
            //await Task.Delay(1);
            //Thread.Sleep(10000);
            Thread.Sleep(1000);

            await Task.WhenAll(tasks);
            //var movies = await _repository.Load(cancellationToken);
            //_repository.Load(cancellationToken);
            //Close();
            //await _context.Database.EnsureCreatedAsync(cancellationToken).ConfigureAwait(false);

        }

        protected override void OnShown(EventArgs e)
        {

            Close();
            base.OnShown(e);
        }
    }
}
