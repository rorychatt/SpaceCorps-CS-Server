using SpaceCorpsServerShared.Players;
using SpaceCorpsServerShared.Statistics;

namespace SpaceCorpsServerShared.Entity;

public interface IKillable
{
    public Stats StatsDrop { get; }

}