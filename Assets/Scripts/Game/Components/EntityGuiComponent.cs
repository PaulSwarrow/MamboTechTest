using System;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    [RequireComponent(typeof(GameEntity))]
    public class EntityGuiComponent : MonoBehaviour
    {
        private GameEntity _entity;

        private void Awake()
        {
            _entity = GetComponent<GameEntity>();
        }

        private void OnGUI()
        {
            var rect = new Rect(10, 10, 200, 
                25 * _entity.Stats.Count +
                25 * _entity.StatusInfo.Count
                + 25
                );
            GUILayout.BeginArea(rect, GUI.skin.box);
            GUILayout.Box("Stats");
            foreach (var stat in _entity.Stats)
            {
                GUILayout.Label($"{stat.Key}: {stat.Value.Value} / {stat.Value.MaxValue}");
            }

            foreach (var status in _entity.StatusInfo)
            {
                GUILayout.Label($"{status.Info}");
            }
            
            GUILayout.EndArea();
        }
    }
}