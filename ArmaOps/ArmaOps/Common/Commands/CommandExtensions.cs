using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
#nullable enable

namespace ArmaOps.Droid.Common.Commands
{
    public static class CommandExtensions
    {
        public static void UnsubscribeCommand(this CommandDelegates commandDelegates)
        {
            UnsubscribeCommand(commandDelegates, string.Empty);
        }

        public static void UnsubscribeCommand(this CommandDelegates commandDelegates, string eventName)
        {
            var t = commandDelegates.Element.GetType();
            var e = t.GetEventInfoForControl(eventName);

            e.RemoveEventHandler(commandDelegates.Element, commandDelegates.Handler);

            if (commandDelegates.CanExecuteHandler != null)
                commandDelegates.Command.CanExecuteChanged -= (EventHandler)commandDelegates.CanExecuteHandler;
        }

        public static CommandDelegates? SubscribeCommand(
            this object element,
            ICommand command)
        {
            return SubscribeCommand(element, string.Empty, command);
        }

        /// <summary>
        /// Sets a non-generic RelayCommand to an object and actuates the command when a specific event is raised. This method
        /// can only be used when the event uses a standard EventHandler. 
        /// </summary>
        /// <param name="element">The element to which the command is added.</param>
        /// <param name="eventName">The name of the event that will be subscribed to to actuate the command.</param>
        /// <param name="command">The command that must be added to the element.</param>
        public static CommandDelegates SubscribeCommand(
            this object element,
            string eventName,
            ICommand command)
        {
            var t = element.GetType();
            var e = t.GetEventInfoForControl(eventName);

            var commandHandler = e.GetCommandHandler(eventName, t, command);
            if (commandHandler == null)
                throw new ArgumentNullException("Must provide a command handler for the event");

            e.AddEventHandler(
                element,
                commandHandler);

            EventHandler? commandCanExecuteHandler = null;
            var enabledProperty = t.GetProperty("Enabled");
            if (enabledProperty != null)
            {
                enabledProperty.SetValue(element, command.CanExecute(null));

                commandCanExecuteHandler = (s, args) =>
                {

                    enabledProperty.SetValue(
                        element,
                        command.CanExecute(null));
                };

                command.CanExecuteChanged += commandCanExecuteHandler;
            }

            return new CommandDelegates(command, commandHandler, commandCanExecuteHandler, element);
        }

        internal static EventInfo GetEventInfoForControl(this Type type, string? eventName)
        {
            if (string.IsNullOrEmpty(eventName))
            {
                eventName = type.GetDefaultEventNameForControl();
            }

            if (string.IsNullOrEmpty(eventName))
            {
                throw new ArgumentException("Event not found", "eventName");
            }

            var info = type.GetEvent(eventName);

            if (info == null)
            {
                throw new ArgumentException("Event not found: " + eventName, "eventName");
            }

            return info;
        }

        internal static string? GetDefaultEventNameForControl(this Type type)
        {
            string? eventName = null;

            if (type == typeof(CheckBox)
                || typeof(CheckBox).IsAssignableFrom(type))
            {
                eventName = "CheckedChange";
            }
            else if (type == typeof(Button)
                     || typeof(Button).IsAssignableFrom(type))
            {
                eventName = "Click";
            }

            return eventName;
        }

        internal static Delegate GetCommandHandler(
            this EventInfo info,
            string eventName,
            Type elementType,
            ICommand command)
        {
            Delegate result;

            if (string.IsNullOrEmpty(eventName)
                && elementType == typeof(CheckBox))
            {
                EventHandler<CompoundButton.CheckedChangeEventArgs> handler = (s, args) =>
                {
                    if (command.CanExecute(null))
                    {
                        command.Execute(null);
                    }
                };

                result = handler;
            }
            else
            {
                EventHandler handler = (s, args) =>
                {
                    if (command.CanExecute(null))
                    {
                        command.Execute(null);
                    }
                };

                result = handler;
            }

            return result;
        }
    }
}