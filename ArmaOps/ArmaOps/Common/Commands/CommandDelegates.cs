using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
# nullable enable

namespace ArmaOps.Droid.Common.Commands
{
    public class CommandDelegates 
    {
        public ICommand Command { get; }
        public Delegate Handler { get; }
        public Delegate? CanExecuteHandler { get; }
        public object Element { get; }
        public CommandDelegates(ICommand command, Delegate commandHandler, Delegate? canExecuteHandler, object element) 
        {
            Command = command;
            Handler = commandHandler;
            CanExecuteHandler = canExecuteHandler;
            Element = element;
        }
    }
}