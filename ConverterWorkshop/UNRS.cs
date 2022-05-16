using System;
using System.Timers;

namespace ConverterWorkshop
{
    public class UNRS
    {
        public int ID { get; set; } // номер UNRS
        private readonly int workTime; // базовое время работы
        public bool IsReady { get; set; } // готовность принять новую партию

        public UNRS(int workTime, int ID)
        {
            this.ID = ID;
            this.workTime = workTime;
            IsReady = true;
        }

        public void Work(Resources resources)
        {
            IsReady = false;
            
            new Timer(workTime + new Random().Next(-100, 100))
            {
                AutoReset = false,
                Enabled = true
            }.Elapsed += (_, _) =>
            {
                IsReady = true;
                resources.Slabs++;
                Console.WriteLine($"УНРС {ID} завершила работу");
            };
        }
    }
}