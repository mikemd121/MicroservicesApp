


namespace PositionsService
{
   public interface IPositionProcessor
    {
        public void OnRateUpdate(RateChangedIntegrationEvent eventItem);
        public  void OnPositionCreate(CreatePositionEvent eventItem);
    }
}
