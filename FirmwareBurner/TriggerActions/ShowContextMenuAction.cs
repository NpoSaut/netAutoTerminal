using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace FirmwareBurner.TriggerActions
{
    /// <summary>Открывает контекстное меню цели</summary>
    public class ShowContextMenuAction : TriggerAction<FrameworkElement>
    {
        /// <summary>Invokes the action.</summary>
        /// <param name="parameter">
        ///     The parameter to the action. If the action does not require a parameter, the parameter may be
        ///     set to a null reference.
        /// </param>
        protected override void Invoke(object parameter)
        {
            ContextMenu menu = AssociatedObject.ContextMenu;
            if (menu == null) return;
            menu.IsOpen = true;
            menu.PlacementTarget = AssociatedObject;
        }
    }
}
