// Editor script is an alternative animFinishedEvent design. It is a dynamic port here that toggles on/off with the
// 'waitForAnimationFinish" flag.

// using System.Linq;
// using UnityEditor;
// using XNode;
// using XNodeEditor;
//
// namespace xNodes.Nodes.Editor
// {
//     [CustomNodeEditor(typeof(TriggerAnimationNode))]
//     public class TriggerAnimationNodeEditor : BaseNodeEditor
//     {
//         private SerializedProperty _waitForAnimationFinishProperty;
//         private bool _prevValue;
//         private NodePort _callbackDynPort;
//
//         private const string CallbackDynPortName = "animFinishedEvent";
//
//         public override void OnCreate()
//         {
//             _waitForAnimationFinishProperty = serializedObject.FindProperty("waitForAnimationFinish");
//         }
//
//         public override void OnBodyGUI()
//         {
//             serializedObject.Update();
//             string[] excludes = { "m_Script", "graph", "position", "ports" };
//
//             if (_waitForAnimationFinishProperty.boolValue != _prevValue)
//             {
//                 bool portExists = target.HasPort(CallbackDynPortName);
//                 if (_waitForAnimationFinishProperty.boolValue)
//                 {
//                     if (portExists)
//                     {
//                         _callbackDynPort = target.GetPort(CallbackDynPortName);
//                     }
//                     else
//                     {
//                         _callbackDynPort = target.AddDynamicOutput(typeof(TriggerAnimationNode.AnimFinishedCallback),
//                             Node.ConnectionType.Multiple,
//                             Node.TypeConstraint.None, CallbackDynPortName);
//                     }
//                 }
//                 else
//                 {
//                     target.RemoveDynamicPort(_callbackDynPort);
//                 }
//             }
//
//             _prevValue = _waitForAnimationFinishProperty.boolValue;
//
//             SerializedProperty iterator = serializedObject.GetIterator();
//             bool enterChildren = true;
//             while (iterator.NextVisible(enterChildren))
//             {
//                 enterChildren = false;
//                 if (excludes.Contains(iterator.name))
//                 {
//                     continue;
//                 }
//
//                 NodeEditorGUILayout.PropertyField(iterator);
//             }
//
//             // Iterate through dynamic ports and draw them in the order in which they are serialized
//             foreach (NodePort dynamicPort in target.DynamicPorts)
//             {
//                 if (NodeEditorGUILayout.IsDynamicPortListPort(dynamicPort))
//                 {
//                     continue;
//                 }
//
//                 NodeEditorGUILayout.PortField(dynamicPort);
//             }
//
//             serializedObject.ApplyModifiedProperties();
//         }
//     }
// }

