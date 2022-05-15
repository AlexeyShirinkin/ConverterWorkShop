using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConverterWorkshop
{
    internal static class Program
    {
        private static readonly Random Random = new();
        private static readonly Resources Resources = new();
        private static Ladle Ladle { get; } = new();

        private static ConverterFurnace[] Furnaces { get; } =
        {
            new(370 + Random.Next(-50, 50), 280),
            new(340 + Random.Next(-50, 50), 250),
            new(430 + Random.Next(-50, 50), 310)
        };

        private static UNRS[] UNRSs { get; } =
        {
            new(GetWorkTime()),
            new(GetWorkTime()),
            new(GetWorkTime()),
            new(GetWorkTime()),
            new(GetWorkTime())
        };

        public static void Main()
        {
            StartProcess();
        }

        private static void StartProcess()
        {
            var timer = Stopwatch.StartNew();

            foreach (var furnace in Furnaces)
                if (furnace.IsReady)
                {
                    furnace.IsReady = false;
                    furnace.Work(Resources);
                    Console.WriteLine($"Чугун={Resources.Iron}, Печи={furnace.IronAmount} ");
                }

            while (Resources.Iron > 0)
            {
                foreach (var furnace in Furnaces)
                    if (furnace.IsReady && Ladle.IsReady)
                    {
                        furnace.Work(Resources);
                        if (!Ladle.IsDone)
                            Ladle.Work(50 + Random.Next(-30, 30));
                        Console.WriteLine($"Чугун: {Resources.Iron}, Печи: {furnace.IronAmount}");
                    }

                if (Ladle.IsDone)
                    TryActiveFreeUNRS(UNRSs);
            }

            timer.Stop();
            Console.WriteLine($"Время: {timer.ElapsedMilliseconds}, слабы: {Resources.Slabs}");
        }

        private static int GetWorkTime()
        {
            return 710 + Random.Next(-100, 100) + 40 + Random.Next(-10, 10);
        }

        private static void TryActiveFreeUNRS(IEnumerable<UNRS> UNRSs)
        {
            var readyUNRSs = UNRSs
                .Where(x => x.IsReady)
                .Select((x, i) => (x, i)).ToList();

            if (readyUNRSs.Count == 0)
                return;

            var (UNRS, index) = readyUNRSs.First();

            Console.WriteLine($"УНРС {index} активна");
            Ladle.IsDone = false;
            Ladle.IsReady = true;
            UNRS.Work(Resources);
        }
    }
}