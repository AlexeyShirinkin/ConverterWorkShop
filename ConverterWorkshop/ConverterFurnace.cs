using System;
using System.Timers;

namespace ConverterWorkshop
{
    public class ConverterFurnace
    {
        private readonly int workTime; // время работы плавления
        public int IronAmount { get; } // количество вмещаемого чугуна
        public bool IsReady { get; set; } // готовность отдавать сталь
        public bool IsEmpty { get; set; } // готовность к принятию новой партии

        public ConverterFurnace(int workTime, int ironAmount)
        {
            IronAmount = ironAmount;
            this.workTime = workTime;
            IsEmpty = true;
        }

        public void Work(Resources resources)
        {
            resources.Iron -= IronAmount;
            IsEmpty = false;
            
            new Timer(workTime + new Random().Next(-50, 50) + 50 + new Random().Next(-20, 20))
            {
                Enabled = true,
                AutoReset = false
            }.Elapsed += (_, _) =>
            {
                IsReady = true;
            };
        }
    }
}