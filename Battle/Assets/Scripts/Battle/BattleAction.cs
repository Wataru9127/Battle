using Battle;

/// <summary>
/// Œˆ‚Ü‚Á‚½s“®‚ğA‡”Ô•t‚«‚Å—­‚ß‚Ä‚¨‚­” 
/// </summary>
public abstract class BattleAction
{
    public BattlePokemon Actor { get; }
    public int Priority { get; }

    public abstract void Execute(BattleContext context);
}
