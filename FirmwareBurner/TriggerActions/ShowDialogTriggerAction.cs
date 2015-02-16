using System.Windows;
using System.Windows.Interactivity;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwareBurner.TriggerActions
{
    public class ShowDialogTriggerAction : TriggerAction<DependencyObject>
    {
        public ResourceDictionary DialogMergedResources { get; set; }

        /// <summary>Invokes the action.</summary>
        /// <param name="parameter">
        ///     The parameter to the action. If the action does not require a parameter, the parameter may be
        ///     set to a null reference.
        /// </param>
        protected override void Invoke(object parameter)
        {
            var e = (InteractionRequestedEventArgs)parameter;

            var dlg = new DialogShell
                      {
                          DataContext = e.Context.Content,
                          Title = e.Context.Title,
                          Owner = Window.GetWindow(AssociatedObject)
                      };

            if (DialogMergedResources != null)
                dlg.ResourceDictionary.MergedDictionaries.Add(DialogMergedResources);

            dlg.ShowDialog();

            e.Callback();
        }
    }
}
