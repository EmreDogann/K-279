using System;
using System.Collections.Generic;
using UnityEngine;
using xNodes.Nodes.Delay;

namespace xNodes.Nodes.Lights
{
    [NodeWidth(450)]
    [CreateNodeMenu("Actions/Lights/Set Flickering")]
    public class SetActiveFlickeringNode : BaseNode
    {
        [Serializable]
        private class FlickerState
        {
            public LightFlickerEffect flickerComponent;
            public bool setActive;
        }

        [SerializeField] private List<FlickerState> flickeringLights;

        public override void Execute()
        {
            foreach (FlickerState lightEffect in flickeringLights)
            {
                if (lightEffect.setActive)
                {
                    lightEffect.flickerComponent.EnableEffect();
                }
                else
                {
                    lightEffect.flickerComponent.DisableEffect();
                }
            }

            NextNode("exit");
        }
    }
}