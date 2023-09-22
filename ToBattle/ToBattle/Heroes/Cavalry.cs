using NLog;
using ToBattle.Services;

namespace ToBattle.Heroes;

internal class Cavalry : HeroBase
{
    private readonly RandomService _randomService;

    public Cavalry(int id, string name, ILogger logger, RandomService randomService) : base(id, name, HeroClassEnum.Cavalry, 150, logger)
    {
        _randomService = randomService;
    }

    public override void Attack(IHero defender)
    {
        switch (defender.Class)
        {
            case HeroClassEnum.Archer:
                // Cavalry attacks Archer - Archer (defender) dies
                // Do nothing
                break;
            case HeroClassEnum.Cavalry:
                // Cavalry attacks Cavalry - defender dies
                // Do nothing
                break;
            case HeroClassEnum.Swordsman:
                // Cavalry attacks Swordsman -> Cavalry dies
                IsAlive = false;
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
                // Archer attacks Cavalry
                // 40% chance for Cavalry to die
                var randomNumber = _randomService.Next(0, 100);
                if (randomNumber < 40)
                {
                    Die("Dices rolled, and the hero was hit by an arrow of the attacking archer...");
                }
                else
                {
                    _logger.Info($"{IdAndName()} has a lucky day, and successfully evaded the arrows of the attacking archer!");
                }
                break;
            case HeroClassEnum.Cavalry:
                // Cavalry attacks Cavalry - defender dies
                Die("The hero was attacked by another Cavalry.");
                break;
            case HeroClassEnum.Swordsman:
                // Swordsman attacks Cavalry - Nothing happens
                // Do nothing
                _logger.Info($"{IdAndName()} gallops away from the attacking swordsman.");
                break;
            default:
                throw new NotSupportedException("Unknown defender class.");
        }
    }
}