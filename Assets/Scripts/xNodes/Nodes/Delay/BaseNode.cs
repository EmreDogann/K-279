using System;
using UnityEngine;
using XNode;
using xNodes.Graphs;

namespace xNodes.Nodes.Delay
{
    [Serializable]
    public abstract class BaseNode : Node
    {
        [Input] public int entry;
        [Output] public int exit;

        public enum State
        {
            None,
            Running,
            Failure,
            Success
        }

        [HideInInspector] public State state = State.None;

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
                        state = State.None;
                        return;
                    }

                    baseNode = p.Connection.node as BaseNode;
                    break;
                }
            }

            state = State.None;
            if (baseNode != null)
            {
                SequencerGraph sequencerGraph = graph as SequencerGraph;
                if (sequencerGraph != null)
                {
                    baseNode.state = State.Running;
                    sequencerGraph.currentNode = baseNode;
                    sequencerGraph.Execute();
                }
                else
                {
                    Debug.LogError("Sequencer Graph not found!");
                }
            }
        }

        public void TriggerOutput(string outputName)
        {
            BaseNode baseNode = null;
            foreach (NodePort p in Ports)
            {
                if (p.fieldName == outputName)
                {
                    if (p.Connection == null)
                    {
                        state = State.None;
                        return;
                    }

                    baseNode = p.Connection.node as BaseNode;
                    break;
                }
            }

            state = State.None;
            if (baseNode != null)
            {
                SequencerGraph sequencerGraph = graph as SequencerGraph;
                if (sequencerGraph != null)
                {
                    baseNode.state = State.Running;
                    baseNode.Execute();
                }
                else
                {
                    Debug.LogError("Sequencer Graph not found!");
                }
            }
        }
    }
}