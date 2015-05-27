using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirmwareBurner.AsyncOperations;
using FirmwareBurner.Burning.Exceptions;
using FirmwareBurner.Progress;
using FirmwareBurner.Project;
using FirmwareBurner.ViewModels;

namespace FirmwareBurner.Burning
{
    public class BurningService : IBurningService
    {
        private readonly IBurningReceiptsCatalog _burningReceiptsCatalog;
        private readonly IExceptionService _exceptionService;
        private readonly IProgressControllerFactory _progressControllerFactory;

        public BurningService(IBurningReceiptsCatalog BurningReceiptsCatalog, IExceptionService ExceptionService,
                              IProgressControllerFactory ProgressControllerFactory)
        {
            _burningReceiptsCatalog = BurningReceiptsCatalog;
            _exceptionService = ExceptionService;
            _progressControllerFactory = ProgressControllerFactory;
        }

        public ICollection<IBurningMethod> GetBurningMethods(string DeviceName)
        {
            return _burningReceiptsCatalog.GetBurningReceiptFactories(DeviceName)
                                          .Select(brf => new BurningMethod(brf.GetReceipt(DeviceName)))
                                          .OfType<IBurningMethod>()
                                          .ToList();
        }

        public IAsyncOperationToken BeginBurn(IBurningReceipt Receipt, FirmwareProject Project)
        {
            var token = new BurningAsyncOperationToken(new ProgressProxy());
            Task.Factory.StartNew(() => Burn(Receipt, Project, token));
            return token;
        }

        private void Burn(IBurningReceipt Receipt, FirmwareProject Project, BurningAsyncOperationToken OperationToken)
        {
            using (_progressControllerFactory.CreateController(OperationToken.ProgressToken))
            {
                try
                {
                    Receipt.Burn(Project, OperationToken.ProgressToken);
                    OperationToken.Success();
                }
                catch (CreateImageException exception)
                {
                    _exceptionService.PublishException("Не удалось составить образ для прошивки", exception.InnerException);
                    OperationToken.Error(exception);
                }
                catch (BurningException exception)
                {
                    _exceptionService.PublishException("Не удалось прошить устройство", exception.InnerException);
                    OperationToken.Error(exception);
                }
            }
        }

        private class BurningAsyncOperationToken : AsyncOperationTokenBase
        {
            public BurningAsyncOperationToken(ProgressProxy ProgressProxy) : base(ProgressProxy, false) { ProgressToken = ProgressProxy; }
            public IProgressToken ProgressToken { get; private set; }

            public void Error(Exception Exception)
            {
                OnExceptionHandled(new ExceptionHandledEventArgs(Exception));
                OnCompleated(new AsyncOperationCompleatedEventArgs(AsyncOperationCompleatingStatus.Error));
            }

            public void Success() { OnCompleated(new AsyncOperationCompleatedEventArgs(AsyncOperationCompleatingStatus.Success)); }
        }
    }
}
