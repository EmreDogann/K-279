using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniGame
{
    public class ShuffleCard : MonoBehaviour, IPointerClickHandler
    {
        public int ID;
        public ShuffleMiniGame parentGame;
        private void Awake()
        {
        }
        public void SetParent(ShuffleMiniGame parent)
        {
            parentGame = parent;
        }
        public void SetID(int ID)
        {
            //parentGame = GetComponentInParent<ShuffleMiniGame>();
            
            this.ID = ID;
        }
        public int GetID()
        {
            return this.ID;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(ID);
            parentGame.Shuffle(ID);
        }
    }
}