using System.Windows;
using System.Windows.Controls;
using FirmwareBurner.ViewModels.Targeting;

namespace FirmwareBurner.Views
{
    public class ModulesListItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SingleModificationItemTemplate { get; set; }
        public DataTemplate MultipleModificationsItemTemplate { get; set; }

        /// <summary>
        ///     При переопределении производного класса возвращает шаблон <see cref="T:System.Windows.DataTemplate" />,
        ///     основанный на настраиваемой логике.
        /// </summary>
        /// <returns>Возвращает шаблон <see cref="T:System.Windows.DataTemplate" /> или значение null.Значение по умолчанию — null.</returns>
        /// <param name="item">Объект данных, для которого необходимо выбрать шаблон.</param>
        /// <param name="container">Объект, привязанный к данным.</param>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var cellKind = (CellKindViewModel)item;
            return (cellKind.Modifications.Count == 1) ? SingleModificationItemTemplate : MultipleModificationsItemTemplate;
        }
    }
}
