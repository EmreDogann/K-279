using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Items
{
    public class ItemManager : MonoBehaviour
    {
        [SerializeField] private int prefabCount;
        [SerializeField] private List<GameObject> itemPrefabs;

        public static ItemManager Instance { get; private set; }
        private List<IItem> _inventoryItems;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _inventoryItems = new List<IItem>(prefabCount * itemPrefabs.Count);
            if (itemPrefabs.Count == 0)
            {
                return;
            }

            foreach (GameObject prefab in itemPrefabs.Where(prefab => prefab.GetComponent<IItem>() != null))
            {
                for (int i = 0; i < prefabCount; i++)
                {
                    _inventoryItems.Add(Instantiate(prefab, Vector3.up * -20.0f, Quaternion.identity, transform)
                        .GetComponent<IItem>());
                }
            }
        }

        public void RegisterItem(IItem item)
        {
            _inventoryItems.Add(item);
        }
    }
}