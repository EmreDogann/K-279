using UnityEngine;
using XNode;
using xNodes.Nodes;

namespace xNodes.Graphs
{
    [CreateAssetMenu(fileName = "New Node Graph", menuName = "NodeGraph/Sequencer Graph")]
    public class SequencerGraph : NodeGraph
    {
        public BaseNode startNode;
        public BaseNode currentNode;

        public void Start()
        {
            if (startNode == null)
            {
                startNode = nodes[0] as BaseNode;
            }

            currentNode = startNode;
            Execute();
        }

        public void Execute()
        {
            currentNode.Execute();
        }
    }
}