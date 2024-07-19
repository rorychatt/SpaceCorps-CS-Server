using SpaceCorpsServerShared.Statistics;

namespace SpaceCorpsServerShared.Players;

public class Stats : IStats
{
    private int Credits;
    private int Thulium;
    private int Honor;
    private int Experience;

    public Stats()
    {
        Credits = 0;
        Thulium = 0;
        Honor = 0;
        Experience = 0;
    }

    public void AddCredits(int credits)
    {
        Credits += credits;
    }

    public int AddExperience(int experience)
    {
        Experience += experience;
        return Experience;
    }

    public void AddHonor(int honor)
    {
        Honor += honor;
    }

    public void AddThulium(int thulium)
    {
        Thulium += thulium;
    }
    
    public int GetCredits()
    {
        return Credits;
    }

    public int GetExperience()
    {
        return Experience;
    }

    public int GetHonor()
    {
        return Honor;
    }

    public int GetThulium()
    {
        return Thulium;
    }

    public void SetCredits(int credits)
    {
        Credits = credits;
    }

    public int SetExperience(int experience)
    {
        Experience = experience;
        return Experience;
    }

    public void SetHonor(int honor)
    {
        Honor = honor;
    }

    public void SetThulium(int thulium)
    {
        Thulium = thulium;
    }
}
