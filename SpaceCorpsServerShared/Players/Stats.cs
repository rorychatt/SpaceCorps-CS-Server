using SpaceCorpsServerShared.Statistics;

namespace SpaceCorpsServerShared.Players;

public class Stats : IStats
{
    private int _credits = 0;
    private int _thulium = 0;
    private int _honor = 0;
    private int _experience = 0;

    public void AddCredits(int credits)
    {
        _credits += credits;
    }

    public int AddExperience(int experience)
    {
        _experience += experience;
        return _experience;
    }

    public void AddHonor(int honor)
    {
        _honor += honor;
    }

    public void AddThulium(int thulium)
    {
        _thulium += thulium;
    }
    
    public int GetCredits()
    {
        return _credits;
    }

    public int GetExperience()
    {
        return _experience;
    }

    public int GetHonor()
    {
        return _honor;
    }

    public int GetThulium()
    {
        return _thulium;
    }

    public void SetCredits(int credits)
    {
        _credits = credits;
    }

    public int SetExperience(int experience)
    {
        _experience = experience;
        return _experience;
    }

    public void SetHonor(int honor)
    {
        _honor = honor;
    }

    public void SetThulium(int thulium)
    {
        _thulium = thulium;
    }
}
