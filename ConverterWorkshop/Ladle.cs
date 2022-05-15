using System;
using System.Timers;

namespace ConverterWorkshop
{
    public class Ladle
    {
        public bool IsReady { get; set; } = true; // Готовность ковша принять сталь
        public bool IsDone { get; set; } // Ковш выполнил работу

        public void Work(int timeWork)
        {
            IsReady = false;

            var timer = new Timer(timeWork)
            {
                AutoReset = false,
                Enabled = true
            };

            timer.Elapsed += (_, _) =>
            {
                IsDone = true;
                Console.WriteLine("Ковш выполнил работу");
            };
        }
    }
}