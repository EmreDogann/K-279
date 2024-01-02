using Attributes;
using Rooms;
using ScriptableObjects.Rooms;
using UnityEngine;

namespace xNode.Nodes
{
    [NodeWidth(300)]
    [CreateNodeMenu("Actions/Switch Room")]
    public class SwitchRoomNode : BaseNode
    {
        [NodeEnum] [SerializeField] private RoomType roomType;
        [SerializeField] private RoomManager roomManager;
        [SerializeField] private float transitionTime = -1.0f;

        public override void Execute()
        {
            roomManager.SwitchRoom(roomType, transitionTime);
            NextNode("exit");
        }
    }
}