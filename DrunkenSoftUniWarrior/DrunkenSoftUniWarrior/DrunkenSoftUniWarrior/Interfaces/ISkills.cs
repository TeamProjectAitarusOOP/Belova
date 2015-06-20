using DrunkenSoftUniWarrior.Characters;

namespace DrunkenSoftUniWarrior.Interfaces
{
    public interface ISkills
    {
        int Level { get; set; }

        int Expirience { get; set; }

        double Health { get; set; }

        double Armor { get; set; }

        double Damage { get; set; }

        bool IsAlive { get; set; }
    }
}