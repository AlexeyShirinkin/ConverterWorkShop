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
            new(370, 280),
            new(340, 250),
            new(430, 310)
        };

        private static UNRS[] UNRSs { get; } =
        {
            new(710, 1),
            new(710, 2),
            new(710, 3),
            new(710, 4),
            new(710, 5)
        };

        public static void Main()
        {
            StartProcess();
        }

        private static void StartProcess()
        {
            var timer = Stopwatch.StartNew();
            do
            {
                foreach (var furnace in Furnaces)
                {
                    if (furnace.IsEmpty && Resources.Iron > furnace.IronAmount)
                    {
                        furnace.Work(Resources);
                        Console.WriteLine($"Чугун: {Resources.Iron}, загружено в печь: {furnace.IronAmount}");
                    }

                    if (furnace.IsReady && Ladle.IsReady)
                    {
                        TryActiveFreeUNRS(UNRSs, furnace);
                    }
                }
                if (Furnaces.All(f => Resources.Iron < f.IronAmount))
                    timer.Stop();
            } while (Furnaces.Any(f => !f.IsEmpty) || UNRSs.Any(u => !u.IsReady));
            
            Console.WriteLine($"\nВремя: {timer.ElapsedMilliseconds}, слябы: {Resources.Slabs}");
            foreach (var unrS in UNRSs)
            {
                Console.WriteLine($"\nВремя простоя УНРС {unrS.ID}: {unrS.chillTimer.ElapsedMilliseconds}");
            }
        }

        private static void TryActiveFreeUNRS(IEnumerable<UNRS> UNRSs, ConverterFurnace furnace)
        {
            var readyUNRS = UNRSs.FirstOrDefault(unrs => unrs.IsReady);

            if (readyUNRS == null)
                return;
            Ladle.Work(readyUNRS, Resources);
            furnace.IsEmpty = true;
            furnace.IsReady = false;
            
            Console.WriteLine($"Чугун: {Resources.Iron}, взят из печи: {furnace.IronAmount}");
        }
    }
}