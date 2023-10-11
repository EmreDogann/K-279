using System;
using System.Collections.Generic;
using UnityEngine;
using xNodes.Nodes.Delay;

namespace xNodes.Nodes
{
    [NodeWidth(450)]
    [CreateNodeMenu("Actions/Set Active State")]
    public class SetActiveNode : BaseNode
    {
        [Serializable]
        private class GameObjectState
        {
            public GameObject gameObject;
            public bool setActive;
        }

        [SerializeField] private List<GameObjectState> gameObjects;

        public override void Execute()
        {
            foreach (GameObjectState objectState in gameObjects)
            {
                objectState.gameObject.SetActive(objectState.setActive);
            }

            NextNode("exit");
        }
    }
}