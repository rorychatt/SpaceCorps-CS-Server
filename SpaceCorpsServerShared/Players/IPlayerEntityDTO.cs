using MySql.Data.MySqlClient;

namespace SpaceCorpsServerShared.Players;

public interface IPlayerEntityDTO
{
    public string Username { get; init; }
    public string MapName { get; init; }
    public string CompanyName { get; init; }
    public float PositionX { get; init; }
    public float PositionY { get; init; }
    public int Credits { get; init; }
    public int Thulium { get; init; }
    public int Experience { get; init; }
    public int Honor { get; init; }
    public int Level { get; init; }

}