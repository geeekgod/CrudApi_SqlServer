using Hangfire;

namespace SqlServerRestApi.Jobs
{
    public class CronJob
    {
        public void TestJob()
        {
            RecurringJob.AddOrUpdate(() => Console.Write("Powerful!"), "*/10 * * * * *");
        }
    }
}
