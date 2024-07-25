using MySql.Data.MySqlClient;
namespace SpaceCorpsServerShared.Players;

public record PlayerEntityDTO : IPlayerEntityDTO
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

    public PlayerEntityDTO(Dictionary<string, object> parameters)
    {
        Username = parameters["username"].ToString()!;
        MapName = parameters["mapName"].ToString()!;
        CompanyName = parameters["company"].ToString()!;
        PositionX = Convert.ToSingle(parameters["positionX"]);
        PositionY = Convert.ToSingle(parameters["positionY"]);
        Credits = Convert.ToInt32(parameters["credits"]);
        Thulium = Convert.ToInt32(parameters["thulium"]);
        Experience = Convert.ToInt32(parameters["experience"]);
        Honor = Convert.ToInt32(parameters["honor"]);
        Level = Convert.ToInt32(parameters["level"]);
    }
}