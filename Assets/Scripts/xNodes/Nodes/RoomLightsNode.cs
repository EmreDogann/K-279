using Attributes;
using Rooms;
using UnityEngine;

namespace xNodes.Nodes
{
    [NodeWidth(300)]
    [CreateNodeMenu("Actions/Lights/Room Lights")]
    public class RoomLightsNode : BaseNode
    {
        [NodeEnum] [SerializeField] private RoomType roomType;
        [SerializeField] private RoomManager roomManager;
        [Space]
        [SerializeField] private bool turnLightsOn;
        [SerializeField] private float switchDuration = 2.0f;

        public override void Execute()
        {
            roomManager.GetRoom(roomType).ControlLights(turnLightsOn, switchDuration);
            NextNode("exit");
        }
    }
}