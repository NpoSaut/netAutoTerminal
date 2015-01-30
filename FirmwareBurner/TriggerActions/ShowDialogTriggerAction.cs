using System;
using System.Windows;
using System.Windows.Interactivity;
using FirmwareBurner.ViewModels;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwareBurner.TriggerActions
{
    public class ShowDialogTriggerAction : TriggerAction<FrameworkElement>
    {
        public Uri ResourceDictionary { get; set; }

        /// <summary>Invokes the action.</summary>
        /// <param name="parameter">
        ///     The parameter to the action. If the action does not require a parameter, the parameter may be
        ///     set to a null reference.
        /// </param>
        protected override void Invoke(object parameter)
        {
            var e = (InteractionRequestedEventArgs)parameter;
            var context = (RequestDialogInteractionContext)e.Context;

            var dlg = new DialogShell { DataContext = context.ViewModel };
            if (ResourceDictionary != null)
                dlg.ResourceDictionary.MergedDictionaries.Add(new ResourceDictionary { Source = ResourceDictionary });
            dlg.ShowDialog();
        }
    }
}
