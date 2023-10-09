using UnityEngine;
using XNode;

namespace xNodes.Graphs
{
    public class SequencerSceneGraph : SceneGraph<SequencerGraph>
    {
        [SerializeField] private bool triggerOnStart;

        private void Start()
        {
            if (triggerOnStart)
            {
                graph.Start();
            }
        }
    }
}