using System.Numerics;
using SpaceCorpsServerShared.Players;
using SpaceCorpsServerShared.Statistics;

namespace SpaceCorpsServerShared.Entity;

public class Alien : IEntity, IKillable
{
    public Guid Id { get; }
    public Vector3 Position { get; set; }
    public Stats StatsDrop { get; }
    
    public Alien()
    {
        StatsDrop = new Stats();
        Position = Vector3.Zero;
        Id = Guid.NewGuid();
    }
}