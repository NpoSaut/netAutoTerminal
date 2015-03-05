using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirmwareBurner.Burning.Exceptions;
using FirmwareBurner.Project;
using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels
{
    public class BurningViewModel : ViewModelBase
    {
        private readonly IExceptionService _exceptionService;
        private readonly IProjectAssembler _projectAssembler;

        public BurningViewModel(IExceptionService ExceptionService, IProjectAssembler ProjectAssembler,
                                ChannelSelectorViewModel ChannelSelector, IList<BurningVariantViewModel> BurningVariants)
        {
            this.ChannelSelector = ChannelSelector;
            this.BurningVariants = BurningVariants;
            _exceptionService = ExceptionService;
            _projectAssembler = ProjectAssembler;
            foreach (BurningVariantViewModel burningVariant in BurningVariants)
                burningVariant.Activated += BurningVariantOnActivated;
        }

        public ChannelSelectorViewModel ChannelSelector { get; private set; }

        /// <summary>Способ прошивки по-умолчанию</summary>
        public IList<BurningVariantViewModel> BurningVariants { get; private set; }

        /// <summary>Варианты способов прошивки</summary>
        public BurningVariantViewModel DefaultVariant
        {
            get { return BurningVariants.FirstOrDefault(v => v.IsDefault); }
        }

        public ProgressViewModel BurningProgress { get; private set; }

        private void BurningVariantOnActivated(object Sender, BurningVariantActivatedEventArgs e)
        {
            BurningProgress = new ProgressViewModel();
            FirmwareProject project = _projectAssembler.GetProject(ChannelSelector.SelectedChannel.Number);
            RaisePropertyChanged(() => BurningProgress);
            Task.Factory.StartNew(() =>
                                  {
                                      try
                                      {
                                          e.BurningReceipt.Burn(project, BurningProgress);
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
