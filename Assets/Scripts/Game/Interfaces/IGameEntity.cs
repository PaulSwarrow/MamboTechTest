using Game.Data;

namespace Game.Interfaces
{
    /// <summary>
    /// Facade for all game entities. All object interactions should be through this component to keep the logic clear
    /// </summary>
    public interface IGameEntity 
    {
        void ApplyEffect(EffectSpec effect);
        
        IEntityStats Stats { get; }
    }
}