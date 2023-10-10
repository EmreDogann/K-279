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
                foreach (Node node in nodes)
                {
                    if (node is StartNode baseNode)
                    {
                        startNode = baseNode;
                        break;
                    }
                }

                if (startNode == null)
                {
                    Debug.LogError("No start node found!");
                }
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