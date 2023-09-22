using NLog;
using ToBattle.Heroes;

namespace ToBattle.Services;

public class HeroService
{
    private readonly ILogger _logger;
    private readonly RandomService _randomService;

    public HeroService(ILogger logger, RandomService randomService)
    {
        _logger = logger;
        _randomService = randomService;
    }

    public IHero GenerateHero(int id)
    {
        var heroClass = GenerateHeroClass();
        var heroName = GenerateName(id);

        switch (heroClass)
        {
            case HeroClassEnum.Archer:
                return new Archer(id, heroName, _logger);
            case HeroClassEnum.Cavalry:
                return new Cavalry(id, heroName, _logger, _randomService);
            case HeroClassEnum.Swordsman:
                return new Swordsman(id, heroName, _logger);
            default:
                throw new NotSupportedException("Unknown hero class");
        }
    }

    private HeroClassEnum GenerateHeroClass()
    {
        int randomNumber = _randomService.Next(0, 3);
        var heroClass = (HeroClassEnum)randomNumber;
        return heroClass;
    }

    private string GenerateName(int id)
    {
        // https://7esl.com/english-names/
        string[] firstNames = {
            "Wade",
            "Dave",
            "Seth",
            "Ivan",
            "Riley",
            "Gilbert",
            "Jorge",
            "Dan",
            "Brian",
            "Roberto",
            "Ramon",
            "Miles",
            "Liam",
            "Nathaniel",
            "Ethan",
            "Lewis",
            "Milton",
            "Claudie",
            "Joshua",
            "Glen"
        };

        string[] lastNames = {
            "Williams",
            "Harris",
            "Thomas",
            "Robinson",
            "Walker",
            "Scott",
            "Nelson",
            "Mitchell",
            "Morgan",
            "Cooper",
            "Howard",
            "Davis",
            "Miller",
            "Martin",
            "Smith",
            "Anderson",
            "White",
            "Perry",
            "Clark",
            "Richards"
        };

        string firstName = _randomService.SelectRandomItem(firstNames);
        string lastName = _randomService.SelectRandomItem(lastNames);

        return $"{firstName} {lastName} ({id})";
    }
}