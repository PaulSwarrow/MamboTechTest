using System;
using StarterAssets.Game.Components;
using StarterAssets.Game.Data;
using StarterAssets.Game.Model;
using StarterAssets.Game.Model.Impl;

namespace StarterAssets.Game.Factories
{
    public class StatusFactory
    {

        public IObjectStatus Create(StatsComponent component, EffectSpec spec)
        {
            switch (spec.Mode)
            {
                //TODO replace inheritance with composition. Make strategies for status interaction, lifespan etc
                case EffectMode.Apply:
                    return new ChangeStatObjectStatus(
                        component,
                        ObjectStatId.Health,
                        spec.Duration,
                        spec.Period,
                        spec.Type == EffectType.Poison,
                        spec.Type,
                        spec.Value);
                case EffectMode.Immune:
                    return new ImmuneObjectStatus(component, spec.Duration, spec.Period, spec.Type);
                default:
                    throw new Exception($"Unknown effect type");
            };
        }

        public IObjectStatus CreateImmortality(StatsComponent component)
        {
            //assuming immortality is a permanent status.  
            return new ImmortalityObjectStatus(component, -1, 0);
        }
    }
}