using System;
using System.Windows;
using System.Windows.Interactivity;

namespace FirmwareBurner.TriggerActions
{
    public class CloseWindowTriggerAction : TriggerAction<DependencyObject>
    {
        /// <summary>Invokes the action.</summary>
        /// <param name="parameter">
        ///     The parameter to the action. If the action does not require a parameter, the parameter may be
        ///     set to a null reference.
        /// </param>
        protected override void Invoke(object parameter)
        {
            Window window = Window.GetWindow(AssociatedObject);
            if (window == null)
                throw new ApplicationException("Состоялся запрос на закрытие окна для элемента, для которого по какой-то причине не удалось найти хост-окно");
            window.Close();
        }
    }
}
