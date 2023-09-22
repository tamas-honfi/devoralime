using NLog;

namespace ToBattle.Heroes;

internal class Archer : HeroBase
{
    public Archer(int id, string name, ILogger logger) : base(id, name, HeroClassEnum.Archer, 100, logger)
    {
    }

    public override void Attack(IHero defender)
    {
        switch (defender.Class)
        {
            case HeroClassEnum.Archer:
                // Archer attacks Archer - defender dies
                // Do nothing
                break;
            case HeroClassEnum.Cavalry:
                // Archer attacks Cavalry - 40% for Cavalry (defender) to die
                // Do nothing
                break;
            case HeroClassEnum.Swordsman:
                // Archer attacks Swordsman - Swordsman (defender) dies
                // Do nothing
                break;
            default:
                throw new NotSupportedException("Unknown defender class.");
        }
    }

    public override void DefendAgainst(IHero attacker)
    {
        switch (attacker.Class)
        {
            case HeroClassEnum.Archer:
                // Archer attacks Archer - defender dies
                Die("The hero was hit by an arrow of another archer...");
                break;
            case HeroClassEnum.Cavalry:
                // Cavalry attacks Archer - Archer (defender) dies
                Die("The hero was attacked by a Cavalry");
                break;
            case HeroClassEnum.Swordsman:
                // Swordsman attacks Archer - Archer (defender) dies
                Die("The hero was attacked by a Swordsman");
                break;
            default:
                throw new NotSupportedException("Unknown defender class.");
        }
    }
}