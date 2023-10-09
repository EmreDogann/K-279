using System;
using UnityEngine;
using XNode;
using xNodes.Graphs;

namespace xNodes.Nodes
{
    [Serializable]
    public abstract class BaseNode : Node
    {
        [Input] public int entry;
        [Output] public int exit;

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }

        public virtual void Execute()
        {
            Debug.Log("Executing Node: " + name);
            NextNode("exit");
        }

        public void NextNode(string exitNode)
        {
            BaseNode baseNode = null;
            foreach (NodePort p in Ports)
            {
                if (p.fieldName == exitNode)
                {
                    if (p.Connection == null)
                    {
                        return;
                    }

                    baseNode = p.Connection.node as BaseNode;
                    break;
                }
            }

            if (baseNode != null)
            {
                SequencerGraph sequencerGraph = graph as SequencerGraph;
                if (sequencerGraph != null)
                {
                    sequencerGraph.currentNode = baseNode;
                    sequencerGraph.Execute();
                }
                else
                {
                    Debug.LogError("Sequencer Graph not found!");
                }
            }
        }
    }
}