using NLog;

namespace ToBattle.Heroes;

internal abstract class HeroBase : IHero
{
    protected ILogger _logger;

    public int Id { get; protected set; }
    public string Name { get; protected set; }
    public HeroClassEnum Class { get; protected set; }
    public int MaxHealth { get; protected set; }

    public int Health { get; protected set; }
    public bool IsAlive { get; protected set; }

    protected HeroBase(int id, string name, HeroClassEnum heroClass, int maxHealth, ILogger logger)
    {
        Id = id;
        Name = name;
        Class = heroClass;
        MaxHealth = maxHealth;
        Health = maxHealth;
        IsAlive = true;

        _logger = logger;
    }

    public void RegenerateHealth()
    {
        var oldHealth = Health;

        Health += 10;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }

        _logger.Info($"{IdAndName()} is healing from {oldHealth} to {Health} (max health: {MaxHealth}).");
    }

    public void DegradeHealth()
    {
        if (!IsAlive) return;

        Health = Health / 2;
        var currentHealthPercent = Math.Round(100.0 * Health / MaxHealth);

        _logger.Warn($"{IdAndName()} health degraded due to attending to a fight. Max health: {MaxHealth}, Current health: {Health}, Health percent: {currentHealthPercent}%");

        if (currentHealthPercent < 25)
        {
            
            Die($"Hero's health degraded below 25% of his max health. Max health: {MaxHealth}, Current health: {Health}, Health percent: {currentHealthPercent}%");
        }
    }

    public abstract void Attack(IHero defender);
    public abstract void DefendAgainst(IHero attacker);

    protected void Die(string causeOfDeath)
    {
        IsAlive = false;
        _logger.Error($"#{Id} {Class} {Name} died. Cause of death: {causeOfDeath}");
    }

    public string IdAndName()
    {
        return $"#{Id} {Class} {Name}";
    }

    public override string ToString()
    {
        var currentHealthPercent = Math.Round(100.0 * Health / MaxHealth);
        return $"#{Id} {Class} {Name} Max health: {MaxHealth}, Current health: {Health}, Health percent: {currentHealthPercent}%. Alive: {IsAlive}";
    }
}