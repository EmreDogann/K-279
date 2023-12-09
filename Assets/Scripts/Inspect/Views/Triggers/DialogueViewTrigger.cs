using System;
using Cinemachine;
using UnityEngine;

namespace Inspect.Views.Triggers
{
    public class DialogueViewTrigger : ViewTrigger
    {
        [SerializeField] private CinemachineVirtualCamera vCam;

        [TextArea(3, 5)]
        [SerializeField] private string dialogueToDisplay;

        public static event Action<CinemachineVirtualCamera, string> TriggerDialogueView;

        public override void TriggerView()
        {
            TriggerDialogueView?.Invoke(vCam, dialogueToDisplay);
        }
    }
}