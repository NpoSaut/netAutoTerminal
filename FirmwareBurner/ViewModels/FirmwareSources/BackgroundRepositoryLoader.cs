using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FirmwarePacking;
using FirmwarePacking.Repositories;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    internal class BackgroundRepositoryLoader : IRepositoryLoader
    {
        private readonly IRepository _repository;

        private readonly ICollection<ComponentTarget> _targets;
        private CancellationToken _cancellationToken;

        public BackgroundRepositoryLoader(IRepository Repository, ICollection<ComponentTarget> Targets)
        {
            _repository = Repository;
            _targets = Targets;
        }

        public void StartLoading(CancellationToken CancellationToken)
        {
            _cancellationToken = CancellationToken;
            Task.Factory.StartNew(() =>
                                  {
                                      IEnumerator<IRepositoryElement> enumerator = _repository.GetPackagesForTargets(_targets).GetEnumerator();
                                      BeginLoadNext(CancellationToken, enumerator);
                                  }, CancellationToken);
        }

        public event EventHandler<RepositoryElementsLoadedEventArgs> ElementsLoaded;

        private void BeginLoadNext(CancellationToken CancellationToken, IEnumerator<IRepositoryElement> enumerator)
        {
            if (enumerator.MoveNext())
                Task.Factory.StartNew(LoadNext, enumerator, CancellationToken);
        }

        private void LoadNext(object Parameter)
        {
            var enumerator = (IEnumerator<IRepositoryElement>)Parameter;
            OnElementsLoaded(new RepositoryElementsLoadedEventArgs(new[] { enumerator.Current }));
            Thread.Sleep(4000);
            BeginLoadNext(_cancellationToken, enumerator);
        }

        protected virtual void OnElementsLoaded(RepositoryElementsLoadedEventArgs E)
        {
            EventHandler<RepositoryElementsLoadedEventArgs> handler = ElementsLoaded;
            if (handler != null) handler(this, E);
        }
    }
}
