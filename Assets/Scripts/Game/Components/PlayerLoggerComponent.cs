using System;
using StarterAssets.Game.Data;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    [RequireComponent(typeof(GameEntity))]
    public class PlayerLoggerComponent : MonoBehaviour
    {
        [SerializeField] private string damageLog = "I am a Wizard, and I got damage";
        private GameEntity _entity;

        private void Awake()
        {
            _entity = GetComponent<GameEntity>();
        }

        private void Start()
        {
            _entity.Stats.StatChangeEvent += OnStatChange;
        }

        private void OnDestroy()
        {
            _entity.Stats.StatChangeEvent -= OnStatChange;
        }


        private void OnGUI()
        {
            var rect = new Rect(10, 10, 200, 
                25 * _entity.Stats.Values.Count +
                25 * _entity.StatusInfo.Count
                + 25
                );
            GUILayout.BeginArea(rect, GUI.skin.box);
            GUILayout.Box("Stats");
            foreach (var stat in _entity.Stats.Values)
            {
                GUILayout.Label($"{stat.Key}: {stat.Value.Current} / {stat.Value.Max}");
            }

            foreach (var status in _entity.StatusInfo)
            {
                GUILayout.Label($"{status.Info}");
            }
            
            GUILayout.EndArea();
        }
        
        private void OnStatChange(ObjectStatId stat, int oldValue, int newValue)
        {
            if (stat == ObjectStatId.Health)
            {
                if(oldValue > newValue)
                    Debug.Log(damageLog);

            }
        }
    }
}