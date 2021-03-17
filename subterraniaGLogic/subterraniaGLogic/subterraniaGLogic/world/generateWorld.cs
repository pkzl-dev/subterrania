namespace subterraniaGLogic.world
{
    public static class generateWorld
    {
        public static string[,] generateNewWorld(int width, int height)
        {
            var n = new caves();
            var o = new ocean();
            string[,] world = n.stonePasser(width, height);
            world = n.cavesPasser(world);
            world = n.caveSeeder(world, 0);
            world = o.oceanPasser(world, 0.04);
            world = n.caveSmoothing(world, width, height);

            return world;
        }
    }
}