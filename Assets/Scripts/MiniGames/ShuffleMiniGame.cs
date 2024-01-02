using System.Collections;
using System.Collections.Generic;
using MiniGame;
using MyBox;
using UnityEngine;

namespace MiniGames
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

        public bool GameEnded()
        {
            if (_state == MiniGameState.ENDEDLOST || _state == MiniGameState.ENDEDWON)
            {
                return true;
            }

            return false;
        }

        public void GenerateObjects()
        {
            if (cardObject == null || emptySpaceObject == null)
            {
                return;
            }

            var uniqueIndices = new HashSet<int>();
            while (uniqueIndices.Count < emptySpacesCount)
            {
                int randNumber = Random.Range(0, gameWidth * gameHeight);

                uniqueIndices.Add(randNumber);
            }

            _emptySpacesIndexList = new List<int>(uniqueIndices);

            _boardBounds = new Bounds(drawCenterTransform.position,
                new Vector2(cellSize * gameWidth, cellSize * gameHeight));

            for (int i = 0; i < objectiveCoordinates.Count; ++i)
            {
                int x = (int)objectiveCoordinates[i].x;
                int y = (int)objectiveCoordinates[i].y;
                _objectiveIndexList.Add(GetListIndexAtPosition(x, y));

                Vector3 objectPosition = _boardBounds.center + new Vector3(x * cellSize + _boardBounds.min.x,
                    y * cellSize + _boardBounds.min.y, 1);
                GameObject obj = Instantiate(objectiveObject, objectPosition, Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);
            }

            for (int i = 0; i < gameWidth * gameHeight; ++i)
            {
                int x = i % gameWidth;
                int y = i / gameWidth;
                Vector3 objectPosition = _boardBounds.center + new Vector3(x * cellSize + _boardBounds.min.x,
                    y * cellSize + _boardBounds.min.y, 0);
                if (_emptySpacesIndexList.Contains(i))
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
                    if (SwapIfValid(cardID, -1, 0)) {}

                    break;
                case DraggedDirection.Right:
                    if (SwapIfValid(cardID, +1, gameHeight - 1)) {}

                    break;
                case DraggedDirection.Up:
                    if (SwapIfValid(cardID, +gameWidth, gameHeight)) {}

                    break;
                case DraggedDirection.Down:
                    if (SwapIfValid(cardID, -gameWidth, gameHeight)) {}

                    break;
            }
        }

        public bool SwapIfValid(int cardID, int offset, int colCheck)
        {
            for (int i = 0; i < _emptySpacesIndexList.Count; ++i)
            {
                if (slideCompleted && cardID % gameWidth != colCheck && cardID + offset == _emptySpacesIndexList[i])
                {
                    _emptySpacesIndexList[i] = cardID;
                    StartCoroutine(CardMovementSlide(cardID, offset, 0.5f));
                    CheckForWinCase();
                    return true;
                }
            }

            return false;
        }

        private IEnumerator CardMovementSlide(int cardID, int offset, float waitTime = 2f)
        {
            slideCompleted = false;
            float elapsedTime = 0;
            Vector3 startPos = _cardObjectList[cardID].transform.localPosition;
            Vector3 finalPos = _cardObjectList[cardID + offset].transform.localPosition;

            _cardObjectList[cardID + offset].transform.localPosition = _cardObjectList[cardID].transform.localPosition;

            while (elapsedTime < waitTime)
            {
                _cardObjectList[cardID].transform.localPosition =
                    Vector3.Lerp(startPos, finalPos, elapsedTime / waitTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            slideCompleted = true;
            _cardObjectList[cardID].transform.localPosition = finalPos;


            _cardObjectList[cardID].GetComponent<ShuffleCard>().SetID(cardID + offset);
            (_cardObjectList[cardID], _cardObjectList[cardID + offset]) =
                (_cardObjectList[cardID + offset], _cardObjectList[cardID]);


            yield return null;
        }

        public void CheckForWinCase()
        {
            for (int i = 0; i < _objectiveIndexList.Count; ++i)
            {
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
            if (_emptySpacesIndexList != null)
            {
                _emptySpacesIndexList.Clear();
            }

            _emptySpacesIndexList = new List<int>();

            if (_objectiveIndexList != null)
            {
                _objectiveIndexList.Clear();
            }

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
    }
}