using Game.Data;

namespace Game.Interfaces
{
    /// <summary>
    /// Instance of a status on an object.
    /// Status applies an effect during specified lifespan
    /// </summary>
    public interface IObjectStatus
    {
        void Start();

        /// <summary>
        /// Interact with another status. May cause cancel or change
        /// </summary>
        /// <param name="objectStatus"></param>
        void Interact(IObjectStatus objectStatus);
        
        /// <summary>
        /// Effect type of immunity checks
        /// </summary>
        EffectType ImmuneTarget { get; }
        
        void Update(float deltaTime);
        
        /// <summary>
        /// Returns if status is over
        /// </summary>
        bool IsOver { get; }
        /// <summary>
        /// Returns if status has negative effects
        /// </summary>
        bool IsDamage { get; }
        /// <summary>
        /// Debug status info
        /// </summary>
        string Info { get; }

        /// <summary>
        /// Force status to finish
        /// </summary>
        /// <param name="cause"></param>
        void Finish(string cause);
    }
}