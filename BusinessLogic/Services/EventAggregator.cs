using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using BusinessLogic.Interfaces;

namespace BusinessLogic.Services
{
    /// <summary>
    /// The Event Aggregator is a service which intermediates messages (events) between two objects.
    /// It allows other objects to register and unregister to events.
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        private static readonly IEventAggregator instance = new EventAggregator();
        public static IEventAggregator Instance { get { return instance; } }

        private readonly ConcurrentDictionary<Type, List<object>> subscriptions = new ConcurrentDictionary<Type, List<object>>();

        public void Publish<T>(T message) where T : IEvent
        {
            List<object> subscribers;
            if (subscriptions.TryGetValue(typeof(T), out subscribers))
            {
                // To Array creates a copy in case someone unsubscribes in their own handler
                foreach (var subscriber in subscribers.ToArray())
                {
                    ((Action<T>)subscriber)(message);
                }
            }
        }

        public void Subscribe<T>(Action<T> action) where T : IEvent
        {
            var subscribers = subscriptions.GetOrAdd(typeof(T), t => new List<object>());
            lock (subscribers)
            {
                subscribers.Add(action);
            }
        }

        public void Unsubscribe<T>(Action<T> action) where T : IEvent
        {
            List<object> subscribers;
            if (subscriptions.TryGetValue(typeof(T), out subscribers))
            {
                lock (subscribers)
                {
                    subscribers.Remove(action);
                }
            }
        }

        public void Dispose()
        {
            subscriptions.Clear();
        }
    }
}
