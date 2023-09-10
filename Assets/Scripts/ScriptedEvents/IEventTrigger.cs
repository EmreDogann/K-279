using System;

namespace ScriptedEvents
{
    public interface IEventTrigger
    {
        event EventHandler EventTriggered;
        public bool IsTriggered();
    }
}