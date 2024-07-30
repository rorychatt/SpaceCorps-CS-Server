using System.Numerics;

namespace SpaceCorpsServerShared.Entity;

public class Alien : IEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    public Vector3 Position { get; set; }
}