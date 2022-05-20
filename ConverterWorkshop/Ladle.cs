using System;
using System.Timers;

namespace ConverterWorkshop
{
    public class Ladle
    {
        public bool IsReady { get; set; } = true; // Готовность ковша принять сталь

        public void Work(UNRS unrs, Resources resources)
        {
            IsReady = false;
            unrs.IsReady = false;

            new Timer(90 + new Random().Next(-30, 30) + new Random().Next(-10, 10))
            {
                AutoReset = false,
                Enabled = true
            }.Elapsed += (_, _) =>
            {
                IsReady = true;
                unrs.Work(resources);
                Console.WriteLine("Ковш выполнил работу");
                Console.WriteLine($"УНРС {unrs.ID} активна");
            };
        }
    }
}