using System;
using subterraniaGLogic.world;

namespace subterraniaGLogic {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            string[,] world = generateWorld.generateNewWorld(200, 15000);
            for(int i = 0; i < world.GetLength(1); i++) {
                for(int j = 0; j < world.GetLength(0); j++) {
                    Console.Write(world[j, i]);
                }
                Console.Write("\n");
            }
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

