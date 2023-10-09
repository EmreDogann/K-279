using Attributes;
using UnityEngine;

namespace xNodes.Nodes
{
    [NodeWidth(300)]
    [CreateNodeMenu("Actions/Object Transformation")]
    public class ObjectTransformationNode : BaseNode
    {
        public enum TransformationSpace
        {
            Global,
            Local
        }

        public enum TransformationMode
        {
            Absolute,
            Relative
        }

        [NodeEnum] [SerializeField] private TransformationSpace space = TransformationSpace.Global;
        [NodeEnum] [SerializeField] private TransformationMode mode = TransformationMode.Absolute;
        [Space]
        [SerializeField] private Transform transform;
        [Space]
        [SerializeField] private Vector3 translation;
        [SerializeField] private Vector3 rotation;
        [SerializeField] private Vector3 scale;

        public override void Execute()
        {
            switch (mode)
            {
                case TransformationMode.Absolute:
                    if (space == TransformationSpace.Global)
                    {
                        transform.position = translation;
                        transform.rotation = Quaternion.Euler(rotation);
                        transform.localScale = scale;
                    }
                    else
                    {
                        transform.localPosition = translation;
                        transform.localRotation = Quaternion.Euler(rotation);
                        transform.localScale = scale;
                    }

                    break;
                case TransformationMode.Relative:
                    if (space == TransformationSpace.Global)
                    {
                        transform.position += translation;
                        transform.rotation *= Quaternion.Euler(rotation);
                        transform.localScale += scale;
                    }
                    else
                    {
                        transform.localPosition += translation;
                        transform.localRotation *= Quaternion.Euler(rotation);
                        transform.localScale += scale;
                    }

                    break;
            }

            NextNode("exit");
        }
    }
}