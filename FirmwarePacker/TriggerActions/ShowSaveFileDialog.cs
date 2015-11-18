using System;
using System.Linq;
using System.Windows;
using System.Windows.Interactivity;
using FirmwarePacker.TriggerActions.Notifications;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Win32;

namespace FirmwarePacker.TriggerActions
{
    public class ShowSaveFileDialog : TriggerAction<UIElement>
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof (String), typeof (ShowSaveFileDialog), new PropertyMetadata("Сохранить файл"));

        public String Title
        {
            get { return (String)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>Invokes the action.</summary>
        /// <param name="parameter">
        ///     The parameter to the action. If the action does not require a parameter, the parameter may be
        ///     set to a null reference.
        /// </param>
        protected override void Invoke(object parameter)
        {
            var e = (InteractionRequestedEventArgs)parameter;
            var context = (SaveFileInteractionContext)e.Context;
            var dlg = new SaveFileDialog
                      {
                          FileName = context.RequestArguments.DefaultFileName,
                          CheckPathExists = true,
                          DefaultExt = context.RequestArguments.DefaultFileType,
                          Title = Title,
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
