using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace time
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeFormat();
            Console.ReadKey();
        }
        public static void TimeFormat() {
            var time = DateTime.Now;
            Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.fff}", time);
        }
        public static void UnixTimeNow()
        {
            var span = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);
            Console.WriteLine(span.TotalSeconds);
            Console.WriteLine((Int64)span.TotalSeconds);
        }
        public static void TimeSpan()
        {
            var start = DateTime.Now;
            Thread.Sleep(5000);
            var end = DateTime.Now;
            var span = end - start;
            Console.WriteLine(span.TotalSeconds);
        }
        public static void ParseDateTimeString()
        {
            // Assume the current culture is en-US. 
            // The date is February 16, 2008, 12 hours, 15 minutes and 12 seconds.
            // Use standard en-US date and time value
            DateTime dateValue;
            string dateString = "2/16/2008 12:15:12 PM";
            try
            {
                dateValue = DateTime.Parse(dateString);
                Console.WriteLine("'{0}' converted to {1}.", dateString, dateValue);
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to convert '{0}'.", dateString);
            }

            // Reverse month and day to conform to the fr-FR culture.
            // The date is February 16, 2008, 12 hours, 15 minutes and 12 seconds.
            dateString = "16/02/2008 12:15:12";
            dateValue = DateTime.Parse(dateString, new CultureInfo("fr-FR", false));
            Console.WriteLine("'{0}' converted to {1}.", dateString, dateValue);

            // Parse string with date but no time component.
            dateString = "2/16/2008";
            dateValue = DateTime.Parse(dateString);
            Console.WriteLine("'{0}' converted to {1}.", dateString, dateValue);
        }
        public static void Timer()
        {
            // Create a timer with a two second interval.
            var aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            Console.WriteLine("\nPress the Enter key to exit the application...\n");
            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            Console.ReadLine();
            aTimer.Stop();
            aTimer.Dispose();

            Console.WriteLine("Terminating the application...");
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);
        }
    }
}
