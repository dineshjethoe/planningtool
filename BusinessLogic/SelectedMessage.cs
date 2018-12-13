using BusinessLogic.Interfaces;

namespace BusinessLogic
{
    public abstract class SelectedMessage<T> : IApplicationEvent
    {
        public SelectedMessage()
        {

        }

        public T Message { get; private set; }
    }
}
