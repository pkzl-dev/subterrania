using System;

namespace subterraniaGLogic.world
{
    public class ocean
    {
        public string[,] oceanPasser(string[,] world, double waterDepth) {

            int waterLine = (int)Math.Truncate(waterDepth * world.GetLength(1));

            for(int i = world.GetLength(1) - waterLine; i < world.GetLength(1) - 1; i++) {

                for(int j = 1; j < world.GetLength(0) - 1; j++) {

                    world[j, i] = "~";
                }
            }
            return world;
        }
    }
}