using System.Collections.Concurrent;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Statistics;
public class StatisticsServer : IStatisticsServer
{
    public Task DeletePlayerStats(Guid playerId)
    {
        throw new NotImplementedException();
    }

    public Task<ConcurrentDictionary<Guid, IPlayer>> LoadAllPlayersStats()
    {
        throw new NotImplementedException();
    }

    public Task<IPlayer> LoadPlayerStats(Guid playerId)
    {
        throw new NotImplementedException();
    }

    public Task SaveAllPlayersStats(ConcurrentDictionary<Guid, IPlayer> players)
    {
        throw new NotImplementedException();
    }

    public Task SavePlayerStats(IPlayer player)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePlayerFromReward(IPlayer player, IRewardable rewardable)
    {
        throw new NotImplementedException();
    }

}
