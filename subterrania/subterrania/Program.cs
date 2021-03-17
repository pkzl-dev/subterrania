using System;
using System.Diagnostics;
using subterrania.world;

namespace subterrania {

    public class Program {
        static void Main() {
            string[,] world = generateWorld.generateNewWorld(200, 15000);
            for(int i = 0; i < world.GetLength(1); i++) {
                for(int j = 0; j < world.GetLength(0); j++) {
                    Console.Write(world[j, i]);
                }
                Console.Write("\n");
            }
        }
    }
}
