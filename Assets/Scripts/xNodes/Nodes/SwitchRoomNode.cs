using Attributes;
using Rooms;
using UnityEngine;

namespace xNodes.Nodes
{
    [NodeWidth(300)]
    [CreateNodeMenu("Actions/Switch Room")]
    public class SwitchRoomNode : BaseNode
    {
        [NodeEnum] [SerializeField] private RoomType roomType;
        [SerializeField] private RoomManager roomManager;

        public override void Execute()
        {
            roomManager.SwitchRoom(roomType);
            NextNode("exit");
        }
    }
}