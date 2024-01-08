using StarterAssets.Game.Data;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    public class DepletableEffectSourceComponent : MonoBehaviour, IEffectSource
    {
        [SerializeField] public EffectType effectType;
        [SerializeField] public int amount;
        [SerializeField] public int maxOutput;
        
        [SerializeField]
        private string depletedMessage;

        [SerializeField] private string usageMessage;


        public void ApplyEffect(IGameEntity effectTarget)
        {
            var output = Mathf.Min(amount, maxOutput);
            if (output == 0)
            {
                Debug.Log(depletedMessage);
            }
            
            //TODO remove hardcoded stat id!
            output = Mathf.Min(output, effectTarget.Stats[ObjectStatId.Health].Delta);
            if (output > 0)
            {
                Debug.Log(usageMessage);
                effectTarget.ApplyEffect(new EffectSpec(effectType, output));
            }

        }
    }
}