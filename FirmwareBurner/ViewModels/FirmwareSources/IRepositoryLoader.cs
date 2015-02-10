using System;
using System.Collections.Generic;
using System.Threading;
using FirmwarePacking.Repositories;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public interface IRepositoryLoader
    {
        void StartLoading(CancellationToken CancellationToken);
        event EventHandler<RepositoryElementsLoadedEventArgs> ElementsLoaded;
    }

    public class RepositoryElementsLoadedEventArgs : EventArgs
    {
        public RepositoryElementsLoadedEventArgs(ICollection<IRepositoryElement> Elements) { this.Elements = Elements; }
        public ICollection<IRepositoryElement> Elements { get; private set; }
    }
}
