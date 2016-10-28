using Nancy.Hosting.Self;
using System;
using System.Configuration;

namespace SchedulerMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var ip = IpHelper.GetLocalIPAddress();

            try
            {
                using (var host = new NancyHost(
                new Uri("http://127.0.0.1:" + ConfigurationManager.AppSettings["Port"].ToString())))
                //new Uri("http://" + ip + ":" + ConfigurationManager.AppSettings["Port"].ToString())))
                {
                    try
                    {
                        host.Start();
                        Console.WriteLine("Press any key to stop...");
                        Console.Read();
                        host.Stop();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("Message:{0}, StackTrace:{1}", ex.Message, ex.StackTrace));
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Message:{0}, StackTrace:{1}", ex.Message, ex.StackTrace));
                Console.ReadKey();
            }
            
        }
    }
}
