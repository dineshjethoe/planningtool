using System;

namespace BusinessLogic.Interfaces
{
    public interface IEventAggregator
    {
        void Publish<T>(T message) where T : IEvent;
        void Subscribe<T>(Action<T> action) where T : IEvent;
        void Unsubscribe<T>(Action<T> action) where T : IEvent;
    }
}
