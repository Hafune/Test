namespace Core
{
    public class CollectorService
    {
        public void EnableReceiver(ReceiverInstance instance)
        {
            instance.Activate();
        }
    }
}