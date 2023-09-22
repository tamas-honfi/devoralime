using NLog;

namespace ToBattle.Heroes;

internal class Swordsman : HeroBase
{
    public Swordsman(int id, string name, ILogger logger) : base(id, name, HeroClassEnum.Swordsman, 120, logger)
    {
    }

    public override void Attack(IHero defender)
    {
        switch (defender.Class)
        {
            case HeroClassEnum.Archer:
                // Swordsman attacks Archer - Archer (defender dies)
                // Do nothing
                break;
            case HeroClassEnum.Cavalry:
                // Swordsman attacks Cavalry - nothing happens
                // Do nothing
                break;
            case HeroClassEnum.Swordsman:
                // Swordsman attacks Swordsman - Defender dies
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
                // Archer attacks Swordsman - Swordsman dies
                Die("The hero was attacked by an Archer.");
                break;
            case HeroClassEnum.Cavalry:
                // Cavalry attacks Swordsman - Cavalry (attacker) dies
                // Do nothing
                break;
            case HeroClassEnum.Swordsman:
                // Swordsman attacks Swordsman - defender dies
                Die("The hero was attacked by another Swordsman.");
                break;
            default:
                throw new NotSupportedException("Unknown defender class.");
        }
    }
}