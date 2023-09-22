using NLog;
using ToBattle.Heroes;
using ToBattle.Services;

namespace ToBattle
{
    public class Game
    {
        private readonly ILogger _logger;
        private readonly HeroService _heroFactory;
        private readonly RandomService _randomService;
        private readonly SettingsService _settingsService;

        public Game(HeroService heroFactory, ILogger logger, RandomService randomService, SettingsService settingsService)
        {
            _heroFactory = heroFactory;
            _logger = logger;
            _randomService = randomService;
            _settingsService = settingsService;
        }

        public void Run()
        {
            _logger.Info("The game is running!");

            _logger.Info("Loading settings.");
            var settings = _settingsService.LoadSettings();
            if (settings.UseSeed)
            {
                _randomService.InitializeWithSeed(settings.Seed);
            }
            int heroCount = settings.HeroesCount;

            

            // Generate heroes
            _logger.Info($"=================================");
            _logger.Info($"Calling for {heroCount} heroes...");
            _logger.Info($"=================================");

            List<IHero> heroList = new List<IHero>();
            for (int i = 0; i < heroCount; i++)
            {
                var hero = _heroFactory.GenerateHero(i);
                heroList.Add(hero);

                _logger.Info($"Hero created: {hero}");
            }

            // Run the simulation
            _logger.Info("To battle!");

            int round = 1;
            var heroesAlive = heroList.Where(hero => hero.IsAlive).ToArray();
            while (heroesAlive.Length >= 2)
            {
                _logger.Info($"=====================");
                _logger.Info($"Round {round}, FIGHT!");
                _logger.Info($"=====================");

                var attacker = _randomService.SelectRandomItem(heroesAlive);
                var defender = _randomService.SelectRandomItem(heroesAlive.Where(hero => hero.Id != attacker.Id).ToArray());

                _logger.Info("Before the fight:");
                _logger.Info($"Attacker: {attacker}");
                _logger.Info($"Defender: {defender}");

                attacker.Attack(defender);
                defender.DefendAgainst(attacker);

                attacker.DegradeHealth();
                defender.DegradeHealth();

                // Log attacker and defender state:
                _logger.Info("After the fight:");
                _logger.Info($"Attacker: {attacker}");
                _logger.Info($"Defender: {defender}");


                // Others are healing
                var others = heroesAlive.Where(hero => hero.Id != attacker.Id && hero.Id != defender.Id).ToArray();
                if (others.Length > 0)
                {
                    _logger.Info("All the other heroes are healing.");
                    foreach (var hero in others)
                    {
                        hero.RegenerateHealth();
                    }
                }
                

                // update heroes alive
                heroesAlive = heroList.Where(hero => hero.IsAlive).ToArray();

                _logger.Info($"Heroes still alive: {heroesAlive.Length}");

                // New round
                round++;
            }

            // Announce the winner
            _logger.Info($"========================");
            _logger.Info($"Aaaand, the winner is...");
            _logger.Info($"========================");

            var winner = heroesAlive.FirstOrDefault();
            if (winner != null)
            {
                _logger.Warn($"{winner}");

                _logger.Warn($"Congratulations!");
                _logger.Warn($"You have just won a castle, and a unicorn!");
                _logger.Warn($"Unfortunately, the princess is in another castle...");
            }
            else
            {
                // This can happen:
                // One of the last two dies because of the attack/defense rules, and the other dies due to health degradation rules.
                _logger.Info($"Battle can be cruel sometimes...");
                _logger.Info($"At the end of the day, noone survived.");
            }
        }
    }
}
