using StarterAssets.Game.Data;

namespace StarterAssets.Game.Components
{
    public interface IObjectStatus
    {
        void Start();

        void Interact(IObjectStatus objectStatus);
        
        EffectType ImmuneTarget { get; }
        
        void Update(float deltaTime);
        
        bool IsOver { get; }
        bool IsDamage { get; }

        void Finish(string cause);
    }
}