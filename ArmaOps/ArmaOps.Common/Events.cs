using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Common
{
    [Preserve(AllMembers = true)]
    public class DialogEventArgs : EventArgs
    {
        public string Title { get; }
        public string Message { get; }
        public string PositiveButtonTitle { get; }
        public DialogEventArgs(
            string title,
            string message,
            string pButtonTitle)
        {
            Title = title;
            Message = message;
            PositiveButtonTitle = pButtonTitle;
        }
    }
}
