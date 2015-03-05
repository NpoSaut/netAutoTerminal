using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace FirmwareBurner.ViewModels.Dialogs
{
    public class ExceptionDialogViewModel : DialogViewModelBase
    {
        public ExceptionDialogViewModel(string Title, String Message, string Details)
        {
            this.Title = Title;
            this.Message = Message;
            this.Details = Details;

            CloseCommand = new DelegateCommand(Close);
            CopyDetailsCommand = new DelegateCommand(CopyDetails);
        }

        public String Title { get; private set; }
        public String Message { get; private set; }
        public String Details { get; private set; }

        public ICommand CloseCommand { get; private set; }
        public ICommand CopyDetailsCommand { get; private set; }

        private void CopyDetails()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Ошибка в программе FirmwareBurner");
            sb.AppendLine(String.Format("Дата возникновения: {0:F}", DateTime.Now));
            sb.AppendLine(Title);
            sb.AppendLine(Message);
            sb.AppendLine();
            sb.AppendLine(Details);
            Clipboard.SetText(sb.ToString());
        }
    }
}
