using System.Collections.Concurrent;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Statistics;
public class StatisticsServer : IStatisticsServer
{
    public Task DeletePlayerStatsAsync(Guid playerId)
    {
        throw new NotImplementedException();
    }

    public Task<ConcurrentDictionary<Guid, IPlayer>> LoadAllPlayersStatsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IPlayer> LoadPlayerStatsAsync(Guid playerId)
    {
        throw new NotImplementedException();
    }

    public Task SaveAllPlayersStatsAsync(ConcurrentDictionary<Guid, IPlayer> players)
    {
        throw new NotImplementedException();
    }

    public Task SavePlayerStatsAsync(IPlayer player)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePlayerFromRewardAsync(IPlayer player, IRewardable rewardable)
    {
        return Task.Run(() => player.UpdateStats(rewardable));
    }

}