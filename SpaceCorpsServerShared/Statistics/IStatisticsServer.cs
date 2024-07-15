using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceCorpsServerShared.Statistics;

public interface IStatisticsServer
{
    public void StorePlayerStats(Guid playerId, string stats);
    
}
