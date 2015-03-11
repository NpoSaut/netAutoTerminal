using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public BurningService(IBurningReceiptsCatalog BurningReceiptsCatalog, IExceptionService ExceptionService)
        {
            _burningReceiptsCatalog = BurningReceiptsCatalog;
            _exceptionService = ExceptionService;
        }

        public ICollection<IBurningMethod> GetBurningMethods(string DeviceName)
        {
            return _burningReceiptsCatalog.GetBurningReceiptFactories(DeviceName)
                                          .Select(brf => new BurningMethod(brf.GetReceipt(DeviceName)))
                                          .OfType<IBurningMethod>()
                                          .ToList();
        }

        public void BeginBurn(IBurningReceipt Receipt, FirmwareProject Project, IProgressToken ProgressToken)
        {
            Task.Factory.StartNew(() =>
                                  {
                                      try
                                      {
                                          Receipt.Burn(Project, ProgressToken);
                                      }
                                      catch (CreateImageException exception)
                                      {
                                          _exceptionService.PublishException("Не удалось составить образ для прошивки", exception.InnerException);
                                      }
                                      catch (BurningException exception)
                                      {
                                          _exceptionService.PublishException("Не удалось прошить устройство", exception.InnerException);
                                      }
                                  });
        }
    }
}
