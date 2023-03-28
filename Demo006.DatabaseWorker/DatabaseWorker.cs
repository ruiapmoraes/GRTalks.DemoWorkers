using Demo006.DatabaseWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo006.DatabaseWorker
{
    public class DatabaseWorker : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private bool _isRunning;
        private Thread _thread;
        private FileSystemWatcher _watcher;
        private readonly ILogger<DatabaseWorker> _logger;

        public DatabaseWorker(IServiceScopeFactory scopeFactory, ILogger<DatabaseWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // file watcher
            _watcher = new FileSystemWatcher 
            {
                Filter = "*.csv",
                Path = Utils.GetAppSettings("PastaAlvo"),
                IncludeSubdirectories = false,
                EnableRaisingEvents = true
            };
            _watcher.Created += Watcher_Created;

            // thread
            _isRunning = true;
            ThreadStart start = new ThreadStart(Perform);

            _thread = new Thread(start);
            _thread.Start();

            _logger.LogInformation("DB Batch worker service iniciado");

           return Task.CompletedTask;
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                //busca o contexto do banco
                var context = scope.ServiceProvider.GetRequiredService<GrtalkDbContext>();

                //ler arquivo
                _logger.LogInformation("Ler arquivo CSV e carregar para o PostGreSQL");
                _logger.LogInformation(e.FullPath);
                using (var reader = new StreamReader(e.FullPath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');

                        //adicionar ao banco
                        Product o = new Product
                        {
                            ProductName = values[0],
                            Quantity = Convert.ToInt32(values[1]),
                            Price = Convert.ToInt32(values[2]),
                            Created = DateTime.Now

                        };
                        context.Products.Add(o);
                    }
                    // commitar informação
                    context.SaveChanges();
                }
                _logger.LogInformation("Finalizado o carregamento do dado para o PostGreSQL.");
                //excluir arquivo
                if (File.Exists(e.FullPath))
                    File.Delete(e.FullPath);
            }
        }

        private void Perform()
        {
            try
            {
                while (_isRunning)
                {
                    if (!_isRunning)
                        break;

                    Thread.Sleep(800);
                }
            }
            catch (Exception)
            {

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _isRunning = false;
            _thread.Join(500); // waiting to join and terminate          

            return Task.CompletedTask;
        }
        public void Dispose()
        {
            this.Dispose();
        }
    }
}
