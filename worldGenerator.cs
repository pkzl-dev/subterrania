using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
namespace generation {

    public class worldGenerator {
		
		public string[,] caveSmoothing(string[,] world, int width, int height) {
            string[,] check = new string[3, 3];
            for (int x = 1; x < width; x++) {
                // arbitrary height definition
                // i am a nimrod ^
                for (int y = 1; y < height; y++) {
                    // get 3x3 square around thing
                    check[0, 0] = world[x-1, y-1];
                    check[0, 1] = world[x-1, y];
                    check[0, 2] = world[x-1, y+1];
                    check[1, 0] = world[x, y-1];
                    check[1, 1] = world[x, y];
                    check[1, 2] = world[x, y+1];
                    check[2, 0] = world[x+1, y-1];
                    check[2, 1] = world[x+1, y];
                    check[2, 2] = world[x+1, y+1];
                    
                    int ore = 0;
                    int stone = 0;
                    // remove singular ores
                    if (check[1, 1] == "@") {
                        foreach (string t in check) {
                            if (t == "@") {
                                ore++;
                            }
                        }
                        // there must be 2 other ore pieces (NOT INCLUDING WORLD[1, 1])
                        // passes:  fails:
                        // XX@      XX@
                        // X@@      X@X
                        // XXX      XXX
                        
                        if (ore - 1 < 2)
                        {
                            world[x, y] = "X";
                        }
                    }

                    if (check[1, 1] == "X") {
                        foreach (string t in check) {
                            if (t == "X") {
                                stone++;
                            }
                        }
                        // there must be 3 other stone pieces
                        // passes:  fails:
                        // XXX      _XX
                        // _X_      _X_
                        // ___      ___
                        
                        if (stone - 1 < 3) {
                            world[x, y] = "_";
                        }
                        
                        //Console.WriteLine($"stone num: {stone}, world before: {check[1, 1]}, world (changed to): {world[x, y]}");
                    }
                }
            }
            Console.WriteLine("cave smoothing complete");
            return world;
        }
		
        public string[,] stonePasser(int width, int height) {

            string[,] output = new string[width+2, height+2];

            for(int i = 1; i < height - 1; i++) {

                for(int j = 1; j < width - 1; j++) {

                    string current = "X";
                    output[j, i] = current;
                }
            }

            for(int i = 0; i < height; i++) {

                for(int j = 0; j < width; j++) {

                    if(output[j, i] != "X") {
                        string current = "0";
                        output[j, i] = current;
                    }
                    
                }
            }

            Console.WriteLine("Stone passed successfully");
            return output;
            
        }

        public string[,] cavesPasser(string[,] world) {

            double interval = 0.002;

            for(int i = 0; i < world.GetLength(1); i++) {

                for(int j = 0; j < world.GetLength(0); j++) {

                    var r = new Random();
                    double chance = r.NextDouble();

                    if(interval >= chance && world[j, i] == "X") {

                        world[j, i] = "_";
                    }
                }

            }

            Console.WriteLine("Caves passed successfully");
            return world;
        }
        public string[,] caveSeeder(string[,] world, int count) {

            var r = new Random();
            double interval = 0.50;
            List<List<int>> c = new List<List<int>>();

                    //  foreach _ in world
            if(count < 16) {
                for(int i = 0; i < world.GetLength(1); i++) {

                    for(int j = 0; j < world.GetLength(0); j++) {

                        if(world[j, i] == "_") {

                            //brute force is the only way to approach this ;(
                            double test = r.NextDouble();

                            if(world[j, i + 1] == "X" && test <= interval) {
                                c.Add(new List<int> { j, i + 1 });

                            }

                            test = r.NextDouble();

                            if(world[j + 1, i + 1] == "X" && test <= interval) {
                                c.Add(new List<int> { j + 1, i + 1 });

                            }

                            test = r.NextDouble();

                            if(world[j + 1, i] == "X" && test <= interval) {
                                c.Add(new List<int> { j + 1, i });

                            }

                            test = r.NextDouble();

                            if(world[j + 1, i - 1] == "X" && test <= interval) {
                                c.Add(new List<int> { j + 1, i - 1 });

                            }

                            test = r.NextDouble();

                            if(world[j, i - 1] == "X" && test <= interval) {
                                c.Add(new List<int> { j, i - 1 });

                            }

                            test = r.NextDouble();

                            if(world[j - 1, i - 1] == "X" && test <= interval) {
                                c.Add(new List<int> { j - 1, i - 1 });

                            }

                            test = r.NextDouble();

                            if(world[j - 1, i] == "X" && test <= interval) {
                                c.Add(new List<int> { j - 1, i });

                            }

                            test = r.NextDouble();

                            if(world[j - 1, i + 1] == "X" && test <= interval) {
                                c.Add(new List<int> { j - 1, i + 1 });

                            }

                            
                            //  recursively call fn to each successful neighbor
                            /*
                             *              1
                             *             / \
                             *            2   5
                             *           / \   \
                             *          3   4   6
                             *                 / \
                             *                7   8
                             */
                            //  execute changes in c to world
                        }
                    }

                }

                Console.WriteLine($"Caves seeded with count: {count}");
                recursiveCaveExpander(world, c, count + 1);
            }
            
            return world;
        }

        public List<List<int>> recursiveCaveExpander(string[,] world, List<List<int>> c, int co) {
            if(co < 16) {
             
                foreach(List<int> e in c) {
                    world[e[0], e[1]] = "_";
                }
                Console.WriteLine($"Caves expanded with count: {co}");
                caveSeeder(world, co + 1);
            }
            return c;
        }

        public string[,] oceanPasser(string[,] world, double waterDepth) {

            int waterLine = (int)Math.Truncate(waterDepth * world.GetLength(1));

            for(int i = world.GetLength(1) - waterLine; i < world.GetLength(1) - 1; i++) {

                for(int j = 1; j < world.GetLength(0) - 1; j++) {

                    world[j, i] = "~";
                }
            }
            return world;
        }
        
        static void Main(string[] args) {

            var n = new worldGenerator();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string[,] world = n.stonePasser(200, 15000);
            world = n.cavesPasser(world);
            world = n.caveSeeder(world, 0);
            world = n.oceanPasser(world, 0.04);
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
