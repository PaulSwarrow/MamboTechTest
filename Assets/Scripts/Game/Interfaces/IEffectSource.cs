namespace Game.Interfaces
{
    /// <summary>
    /// Encapsulates effect source behavior. Applies the effect on demand
    /// </summary>
    public interface IEffectSource
    {
        void ApplyEffect(IGameEntity effectTarget);
    }
}