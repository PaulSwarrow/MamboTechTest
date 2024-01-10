using StarterAssets.Game.Data;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    public class HealEffectSourceComponent : MonoBehaviour, IEffectSource
    {
        [Tooltip("Total amount before depletion")] [SerializeField]
        public int amount = 100;

        [Tooltip("Maximum heal per interaction")] [SerializeField]
        public int maxOutput = 10;

        [SerializeField] private string depletedMessage;

        [SerializeField] private string usageMessage;


        public void ApplyEffect(IGameEntity effectTarget)
        {
            var output = Mathf.Min(amount, maxOutput);
            if (output == 0)
            {
                if (!string.IsNullOrEmpty(depletedMessage))
                    Debug.Log(depletedMessage);
            }

            //TODO remove hardcoded stat id!
            output = Mathf.Min(output, effectTarget.Stats.Values[ObjectStatId.Health].Delta);
            if (output > 0)
            {
                if (!string.IsNullOrEmpty(usageMessage))
                    Debug.Log(usageMessage);
                effectTarget.ApplyEffect(new EffectSpec(EffectType.Heal, output));
            }

            amount -= output;
        }
    }
}