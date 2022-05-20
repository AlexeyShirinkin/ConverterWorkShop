using System;
using System.Diagnostics;
using System.Timers;

namespace ConverterWorkshop
{
    public class UNRS
    {
        public int ID { get; set; } // номер UNRS
        private readonly int workTime; // базовое время работы
        public bool IsReady { get; set; } // готовность принять новую партию
        public Stopwatch chillTimer;

        public UNRS(int workTime, int ID)
        {
            this.ID = ID;
            this.workTime = workTime;
            IsReady = true;
            chillTimer = new Stopwatch();
            chillTimer.Start();
        }

        public void Work(Resources resources)
        {
            chillTimer.Stop();
            new Timer(workTime + new Random().Next(-100, 100))
            {
                AutoReset = false,
                Enabled = true
            }.Elapsed += (_, _) =>
            {
                Console.WriteLine($"УНРС {ID} завершила работу");
                chillTimer.Start();
                IsReady = true;
                resources.Slabs++;
            };
        }
    }
}