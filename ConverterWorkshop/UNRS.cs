using System.Timers;

namespace ConverterWorkshop
{
    public class UNRS
    {
        private readonly int workTime; // время работы
        public bool IsReady { get; set; } // готовность принять новую партию

        public UNRS(int workTime)
        {
            this.workTime = workTime;
            IsReady = true;
        }

        public void Work(Resources resources)
        {
            IsReady = false;
            
            new Timer(workTime)
            {
                AutoReset = false,
                Enabled = true
            }.Elapsed += (_, _) => IsReady = true;

            resources.Slabs++;
        }
    }
}