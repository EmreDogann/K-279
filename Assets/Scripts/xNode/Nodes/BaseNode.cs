using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;
using xNode.Graphs;

namespace xNode.Nodes
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
            state = State.None;
            var baseNodes = new List<BaseNode>();
            foreach (NodePort p in Ports)
            {
                if (p.fieldName == exitNode)
                {
                    if (p.Connection == null)
                    {
                        state = State.None;
                        return;
                    }

                    baseNodes.AddRange(p.GetConnections().Select(connection => connection.node as BaseNode));
                    break;
                }
            }

            if (baseNodes.Count > 0)
            {
                SequencerGraph sequencerGraph = graph as SequencerGraph;
                if (sequencerGraph != null)
                {
                    foreach (BaseNode baseNode in baseNodes)
                    {
                        baseNode.state = State.Running;
                        sequencerGraph.currentNode = baseNode;
                        sequencerGraph.Execute();
                    }
                }
                else
                {
                    Debug.LogError("Sequencer Graph not found!");
                }
            }
        }

        public void TriggerOutput(string outputName)
        {
            state = State.None;
            var baseNodes = new List<BaseNode>();
            foreach (NodePort p in Ports)
            {
                if (p.fieldName == outputName)
                {
                    if (p.Connection == null)
                    {
                        state = State.None;
                        return;
                    }

                    baseNodes.AddRange(p.GetConnections().Select(connection => connection.node as BaseNode));
                    break;
                }
            }

            if (baseNodes.Count > 0)
            {
                SequencerGraph sequencerGraph = graph as SequencerGraph;
                if (sequencerGraph != null)
                {
                    foreach (BaseNode baseNode in baseNodes)
                    {
                        baseNode.state = State.Running;
                        baseNode.Execute();
                    }
                }
                else
                {
                    Debug.LogError("Sequencer Graph not found!");
                }
            }
        }
    }
}