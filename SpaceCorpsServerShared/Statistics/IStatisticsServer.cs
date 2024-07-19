using System.Collections.Concurrent;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Statistics;

public interface IStatisticsServer
{
    public Task SaveAllPlayersStats(ConcurrentDictionary<Guid, IPlayer> players);
    public Task SavePlayerStats(IPlayer player);
    public Task<ConcurrentDictionary<Guid, IPlayer>> LoadAllPlayersStats();
    public Task<IPlayer>LoadPlayerStats(Guid playerId);
    public Task UpdatePlayerFromReward(IPlayer player, IRewardable rewardable);
    public Task DeletePlayerStats(Guid playerId);
        
}
