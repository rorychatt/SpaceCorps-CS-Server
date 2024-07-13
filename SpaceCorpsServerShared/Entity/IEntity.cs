using System.Numerics;

namespace SpaceCorpsServerShared.Entity
{
    public interface IEntity
    {
        public Guid Id { get; }
        public Vector3 Position { get; set; }
    }

}