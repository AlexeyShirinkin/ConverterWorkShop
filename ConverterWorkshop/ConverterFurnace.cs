using System.Timers;

namespace ConverterWorkshop
{
    public class ConverterFurnace
    {
        private readonly int workTime; // время работы плавления
        public int IronAmount { get; } // количество вмещаемого чугуна
        public bool IsReady { get; set; } // готовность к принятию новой партиии

        public ConverterFurnace(int workTime, int ironAmount)
        {
            IronAmount = ironAmount;
            this.workTime = workTime;
            IsReady = true;
        }

        public void Work(Resources resources)
        {
            IsReady = false;
                
            new Timer(workTime)
            {
                Enabled = true,
                AutoReset = false
            }.Elapsed += (_, _) => IsReady = true;

            resources.Iron -= IronAmount;
        }
    }
}