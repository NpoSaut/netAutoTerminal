﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Interactivity;
using FirmwareBurner.ViewModels;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Win32;

namespace FirmwareBurner.TriggerActions
{
    public class ShowSaveFileDialog : TriggerAction<UIElement>
    {
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
                          CheckPathExists = true,
                          DefaultExt = context.RequestArguments.DefaultFileType,
                          Title = context.RequestArguments.Message,
                          FilterIndex = 0,
                          Filter = string.Join("|", context.RequestArguments.FileTypes.Select(ft => String.Format("{1} (*.{0})|*.{0}", ft.Extension, ft.Description)))
                      };
            dlg.ShowDialog();
            context.FileName = dlg.FileName;
            e.Callback();
        }
    }
}
