using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using generation.world;

namespace generation {

    public class Program {

        static void Main(string[] args) {

            var n = new caves();
            var o = new ocean();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string[,] world = n.stonePasser(200, 15000);
            world = n.cavesPasser(world);
            world = n.caveSeeder(world, 0);
            world = o.oceanPasser(world, 0.04);
			world = n.caveSmoothing(world, 200, 15000);
            for(int i = 0; i < world.GetLength(1); i++) {

                for(int j = 0; j < world.GetLength(0); j++) {

                    Console.Write(world[j, i]);

                }

                Console.Write("\n");

            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Console.WriteLine($"{world.GetLength(0) * world.GetLength(1)} tiles placed successfully");
            Console.WriteLine($"Process took {ts.Minutes}:{ts.Seconds}");
            Console.WriteLine($"Average speed: {(world.GetLength(0) * world.GetLength(1)) / (ts.Minutes * 60 + ts.Seconds)} tiles per second");
        }
    }
}
