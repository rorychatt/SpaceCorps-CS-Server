namespace SpaceCorpsServerShared.Players;
public interface IStats
{
    public int GetCredits();
    public IStats SetCredits(int credits);
    public void AddCredits(int credits);
    public int GetThulium();
    public IStats SetThulium(int thulium);
    public void AddThulium(int thulium);
    public int GetHonor();
    public IStats SetHonor(int honor);
    public void AddHonor(int honor);

    public int GetExperience();
    public IStats SetExperience(int experience);
    public int AddExperience(int experience);

}
