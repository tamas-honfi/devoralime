namespace ToBattle.Heroes
{
    public interface IHero
    {
        public int Id { get; }
        public string Name { get; }
        public HeroClassEnum Class { get; }
        public int MaxHealth { get; }

        public int Health { get; }
        public bool IsAlive { get; }

        void Attack(IHero defender);
        void DefendAgainst(IHero attacker);

        void RegenerateHealth();
        void DegradeHealth();
    }
}
