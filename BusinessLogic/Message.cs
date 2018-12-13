using BusinessLogic.Interfaces;

namespace BusinessLogic
{
    /// <summary>
    /// Types that need to be sent as a message between subscriber and publisher (objects) must inherit from this abstract message class 
    /// </summary>
    /// <typeparam name="T">
    /// This can be of any type (entity or model or poco class).
    /// </typeparam>
    public abstract class Message<T> : IEvent
    {
        public T Entity { get; private set; }

        public Message(T entity)
        {
            Entity = entity;
        }
    }
}
