
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace SpaceCorpsServerShared.Entity
{
    public class Player : IEntity
    {
        public Guid Id { get; }
        public Vector3 Position { get; set; }

        public Player()
        {
            Id = Guid.NewGuid();
        }
    }
}