using System;
using System.Linq;
using System.Windows;
using System.Windows.Interactivity;
using FirmwarePacker.TriggerActions.Notifications;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Win32;

namespace FirmwarePacker.TriggerActions
{
    public class ShowOpenFileDialog : TriggerAction<UIElement>
    {
        /// <summary>Invokes the action.</summary>
        /// <param name="parameter">
        ///     The parameter to the action. If the action does not require a parameter, the parameter may be
        ///     set to a null reference.
        /// </param>
        protected override void Invoke(object parameter)
        {
            var e = (InteractionRequestedEventArgs)parameter;
            var context = (OpenFileInteractionContext)e.Context;
            var dlg = new OpenFileDialog
                      {
                          FileName = context.RequestArguments.DefaultFileName,
                          CheckPathExists = true,
                          DefaultExt = context.RequestArguments.DefaultFileType,
                          Title = context.RequestArguments.Message,
                          FilterIndex = 0,
                          Filter =
                              string.Join("|", context.RequestArguments.FileTypes.Select(ft => String.Format("{1} (*.{0})|*.{0}", ft.Extension, ft.Description)))
                      };
            if (dlg.ShowDialog() == true)
                context.FileName = dlg.FileName;
            e.Callback();
        }
    }
}
