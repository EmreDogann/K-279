using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Rooms;
using UnityEngine;

public class EventSequencer : MonoBehaviour {
    
    
    // Move the player into position, play Ship explosion noise, play alarm, play low oxygen voice, fade to normal.
    private Sequence _wakeUpSequence;
    private Transform playerTransform;
    private RoomSwitcher _switcher;
    

    private void Awake() {
    }

    // Start is called before the first frame update
    void Start() {
        _wakeUpSequence = DOTween.Sequence();

        _wakeUpSequence.AppendCallback(()=> {
            var playerTransformPosition = playerTransform.position;
            playerTransformPosition.x = 22;
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
