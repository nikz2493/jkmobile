namespace JKMWindowsService
{
    public interface IWindowsService
    {
        void SchedulerCallback(object e);
        void ScheduleService();
        void TestStartupAndStop(string[] args);
    }
}