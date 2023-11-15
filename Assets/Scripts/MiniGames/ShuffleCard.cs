using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniGame
{
    public enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }
    public class ShuffleCard : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
    {
        public int ID;
        public ShuffleMiniGame parentGame;

        #region FIELDS
        private Grid grid;

        
        #endregion
        #region  IDragHandler - IEndDragHandler
        public void OnEndDrag(PointerEventData eventData)
        {
            //Debug.Log("Press position + " + eventData.pressPosition);
            //Debug.Log("End position + " + eventData.position);
            Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
            //Debug.Log("norm + " + dragVectorDirection);
            parentGame.Shuffle(ID, GetDragDirection(dragVectorDirection));
        }

        //It must be implemented otherwise IEndDragHandler won't work 
        public void OnDrag(PointerEventData eventData)
        {

        }

        private DraggedDirection GetDragDirection(Vector3 dragVector)
        {
            float positiveX = Mathf.Abs(dragVector.x);
            float positiveY = Mathf.Abs(dragVector.y);
            DraggedDirection draggedDir;
            if (positiveX > positiveY)
            {
                draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
            }
            else
            {
                draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
            }
            //Debug.Log(draggedDir);
            return draggedDir;
        }
        #endregion



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
            //Debug.Log(eventData.position);
            //parentGame.Shuffle(ID);
        }
    }
}