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

    public PlayerEntityDTO(string username, string mapName, string companyName, float positionX, float positionY, int credits, int thulium, int experience, int honor, int level)
    {
        Username = username;
        MapName = mapName;
        CompanyName = companyName;
        PositionX = positionX;
        PositionY = positionY;
        Credits = credits;
        Thulium = thulium;
        Experience = experience;
        Honor = honor;
        Level = level;
    }
}