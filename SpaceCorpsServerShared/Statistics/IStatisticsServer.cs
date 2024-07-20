using System.Collections.Concurrent;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Statistics;

public interface IStatisticsServer
{
    public Task SaveAllPlayersStatsAsync(ConcurrentDictionary<Guid, IPlayer> players);
    public Task SavePlayerStatsAsync(IPlayer player);
    public Task<ConcurrentDictionary<Guid, IPlayer>> LoadAllPlayersStatsAsync();
    public Task<IPlayer>LoadPlayerStatsAsync(Guid playerId);
    public Task UpdatePlayerFromRewardAsync(IPlayer player, IRewardable rewardable);
    public Task DeletePlayerStatsAsync(Guid playerId);
        
}
