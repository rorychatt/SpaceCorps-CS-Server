﻿using SpaceCorpsServerShared;

class Program
{
    static void Main(string[] args)
    {
        Server server = new();
        server.StartAsync(args);
    }
}