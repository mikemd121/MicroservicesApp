

namespace PositionsService
{
    public class InMemoryEventDispatcher : IEventDispatcher
    {
        private readonly Dictionary<Type, List<Delegate>> _subscriptions = new();

        public void Publish<T>(T eventItem) where T : IDomainEvent
        {
            if (_subscriptions.TryGetValue(typeof(T), out var subscribers))
            {
                foreach (var subscriber in subscribers.Cast<Action<T>>())
                    subscriber(eventItem);
            }
        }

        public void Subscribe<T>(Action<T> handler) where T : IDomainEvent
        {
            if (!_subscriptions.TryGetValue(typeof(T), out var subscribers))
                subscribers = _subscriptions[typeof(T)] = new List<Delegate>();

            subscribers.Add(handler);
        }
    }
}
