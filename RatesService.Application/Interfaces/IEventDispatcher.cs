
namespace RatesService
{
   public interface IEventDispatcher
    {
        void Publish<T>(T eventItem) where T : IDomainEvent;
        void Subscribe<T>(Action<T> handler) where T : IDomainEvent;
    }
}
