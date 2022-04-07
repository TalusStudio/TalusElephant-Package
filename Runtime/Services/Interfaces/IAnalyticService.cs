namespace Backend.Services.Interfaces
{
    public interface IAnalyticService
    {
        public void Register();
        public void PushProgressionEvent(Progression progression);
        public void Unregister();
    }
}