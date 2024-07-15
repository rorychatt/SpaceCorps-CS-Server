namespace SpaceCorpsServerShared.Statistics;

public interface IStatisticsServer
{
    public void StorePlayerStats(Guid playerId, string stats);
    
}
