using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceCorpsServerShared.Players;
public interface IStats
{
    public int GetCredits();
    public void SetCredits(int credits);
    public void AddCredits(int credits);
    public int GetThulium();
    public void SetThulium(int thulium);
    public void AddThulium(int thulium);
    public int GetHonor();
    public void SetHonor(int honor);
    public void AddHonor(int honor);

    public int GetExperience();
    public int SetExperience(int experience);
    public int AddExperience(int experience);

}
