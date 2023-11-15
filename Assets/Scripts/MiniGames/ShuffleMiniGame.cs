using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEditor;
using System;
using static Unity.Burst.Intrinsics.X86;
using UnityEngine.Rendering.VirtualTexturing;

namespace MiniGame
{
    
    public class ShuffleMiniGame : MonoBehaviour, IMiniGame
    {
        [Separator("Game Parameters")]
        [SerializeField] private int gameWidth = 4;
        [SerializeField] private int gameHeight = 4;
        [SerializeField] private int emptySpacesCount = 3;
        [SerializeField] private List<Vector2> objectiveCoordinates; 
        //[SerializeField] private ArrayLayout gameLayout;
        [Separator("Render Objects")]
        [SerializeField] private GameObject cardObject;
        [SerializeField] private GameObject emptySpaceObject;
        [SerializeField] private GameObject objectiveObject;
        [Separator("Draw Parameters")]
        [SerializeField] private Transform drawCenterTransform;
        [SerializeField] private float cellSize = 3.5f;

        
        private List<GameObject> _cardObjectList; 
        private MiniGameState _state;
        private Bounds _boardBounds;
        private List<int> _emptySpacesIndexList;
        private List<int> _objectiveIndexList;
        private int _cardPositionListSize;
        private bool slideCompleted;

        [ContextMenu("Configure Editor")]
        public void ConfigureEditor()
        {
            //gameLayout = new ArrayLayout(gameWidth);
        }
        public bool GameEnded()
        {
            if (_state == MiniGameState.ENDEDLOST || _state == MiniGameState.ENDEDWON)
            {
                return true;
            } else
            {
                return false;
            }
        }
        
        public void GenerateObjects()
        {
            
            if (cardObject == null || emptySpaceObject == null) return;
            while (_emptySpacesIndexList.Count < emptySpacesCount)
            {
                int randNumber = UnityEngine.Random.Range(0, _cardPositionListSize);
                if (!_emptySpacesIndexList.Contains(randNumber))
                {
                    _emptySpacesIndexList.Add(randNumber);
                }
                
            }

            _boardBounds = new Bounds(drawCenterTransform.position, new Vector2(cellSize * (gameWidth), cellSize * (gameHeight)));

            for (int i = 0; i < objectiveCoordinates.Count; ++i)
            {
                int x = (int)objectiveCoordinates[i].x;
                int y = (int)objectiveCoordinates[i].y;
                _objectiveIndexList.Add(GetListIndexAtPosition(x, y));

                Vector3 objectPosition = _boardBounds.center + new Vector3(x * cellSize + _boardBounds.min.x, y * cellSize + _boardBounds.min.y, 1);
                GameObject obj = Instantiate(objectiveObject, objectPosition, Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);
            }

            for (int i = 0; i < gameWidth * gameHeight; ++i)
            {
                int x = i % gameWidth;
                int y = i / gameWidth;
                Vector3 objectPosition = _boardBounds.center + new Vector3(x * cellSize + _boardBounds.min.x, y * cellSize + _boardBounds.min.y, 0);
                bool empty = false;
                for (int j = 0; j < emptySpacesCount; ++j)
                {
                    if (i == _emptySpacesIndexList[j])
                    {
                        empty = true;
                        break;
                    }
                        
                }
                if (empty)
                {
                    GameObject obj = Instantiate(emptySpaceObject, objectPosition, Quaternion.identity);
                    _cardObjectList.Add(obj);
                    obj.SetActive(false);
                        
                }
                else
                {
                    GameObject obj = Instantiate(cardObject, objectPosition, Quaternion.identity);
                    _cardObjectList.Add(obj);
                    _cardObjectList[i]?.GetComponent<ShuffleCard>().SetParent(this);
                    _cardObjectList[i]?.GetComponent<ShuffleCard>().SetID(i);
                }
                _cardObjectList[i].transform.SetParent(gameObject.transform);
                
            }
        }

        public void Shuffle(int cardID, DraggedDirection draggedDirection)
        {
            switch (draggedDirection)
            {
                case DraggedDirection.Left:
                    if (SwapIfValid(cardID, -1, 0)) { return; }
                    break;
                case DraggedDirection.Right:
                    if (SwapIfValid(cardID, +1, gameHeight - 1)) { return; }
                    break;
                case DraggedDirection.Up:
                    if (SwapIfValid(cardID, +gameWidth, gameHeight)) { return; }
                    break;
                case DraggedDirection.Down:
                    if (SwapIfValid(cardID, -gameWidth, gameHeight)) { return; }
                    break;

            }
        }
        public bool SwapIfValid(int cardID, int offset, int colCheck)
        {
            for (int i = 0; i < _emptySpacesIndexList.Count; ++i)
            {
                if (slideCompleted && ((cardID % gameWidth) != colCheck) && ((cardID + offset) == _emptySpacesIndexList[i]))
                {

                    _emptySpacesIndexList[i] = cardID;
                    _cardObjectList[cardID].GetComponent<ShuffleCard>().SetID(cardID + offset);
                    (_cardObjectList[cardID], _cardObjectList[cardID + offset]) = (_cardObjectList[cardID + offset], _cardObjectList[cardID]);
                    //GameObject temp = _cardObjectList[cardID];
                    //GameObject temp2 = _cardObjectList[cardID + offset];
                    //_cardObjectList[cardID] = temp2;
                    //_cardObjectList[cardID + offset] = temp;

                    //StartCoroutine(CardMovementSlide(cardID, offset, 0.5f));

                    //_cardObjectList[cardID + offset].transform.localPosition = _cardObjectList[cardID].transform.localPosition;
                    (_cardObjectList[cardID].transform.localPosition, _cardObjectList[cardID + offset].transform.localPosition) =
                        (_cardObjectList[cardID + offset].transform.localPosition, _cardObjectList[cardID].transform.localPosition);

                    CheckForWinCase();
                    return true;
                }
            }
            
            return false;
        }

        public void CheckForWinCase()
        {
            for (int i = 0; i < _objectiveIndexList.Count; ++i)
            {
                //Debug.Log("_emptySpacesIndexList[0] = " + _emptySpacesIndexList[0] +
                //    "_emptySpacesIndexList[1] = " + _emptySpacesIndexList[1] +
                //    "_emptySpacesIndexList[2] = " + _emptySpacesIndexList[2] +
                //    " | _objectiveIndexList = " + _objectiveIndexList[i]);
                if (!_emptySpacesIndexList.Contains(_objectiveIndexList[i]))
                {
                    return;
                }
            }
            _state = MiniGameState.ENDEDWON;
            Debug.Log("Won");
        }

        public MiniGameState GetGameState()
        {
            return _state;
        }

        public void StartGame()
        {
            _state = MiniGameState.PLAYING;
            _cardPositionListSize = GetListIndexAtPosition(gameWidth, gameHeight);
            if (_emptySpacesIndexList != null) _emptySpacesIndexList.Clear();
            _emptySpacesIndexList = new List<int>();

            if (_objectiveIndexList != null) _objectiveIndexList.Clear();
            _objectiveIndexList = new List<int>();

            if (_cardObjectList != null) 
            {
                for (int i = 0; i < _cardObjectList.Count; ++i)
                {
                    Destroy(_cardObjectList[i]);
                }
                _cardObjectList.Clear();
            }
            
            _cardObjectList = new List<GameObject>();
            slideCompleted = true;
            GenerateObjects();
        }

        public int GetListIndexAtPosition(int x, int y)
        {
            return gameWidth * y + x;
        }
        
        public int GenerateUniqueHex()
        {
            return -1;
        }
        
        IEnumerator CardMovementSlide(int cardID, int offset, float waitTime = 2f)
        {
            Debug.Log("Starting slide coroutine for card " + cardID);
            slideCompleted = false;
            float elapsedTime = 0;
            Vector3 currentPos = _cardObjectList[cardID].transform.localPosition;
            Vector3 finalPos = _cardObjectList[cardID + offset].transform.localPosition;
            while (elapsedTime < waitTime)
            {
                _cardObjectList[cardID].transform.localPosition = Vector3.Lerp(currentPos, 
                    finalPos, elapsedTime / waitTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            slideCompleted = true;
            _cardObjectList[cardID].transform.localPosition = finalPos;
            Debug.Log("Slide completed for card " + cardID);

            yield return null;
        }
    }
}






