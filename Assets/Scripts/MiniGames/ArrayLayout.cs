using UnityEngine;
using System.Collections;

namespace MiniGame
{
    [System.Serializable]
    public class ArrayLayout
    {

        [System.Serializable]
        public struct rowData
        {
            public bool[] row;
        }

        public rowData[] rows ; //Grid of 7x7

        public ArrayLayout(int size)
        {
            this.rows = new rowData[size];
        }
    }
}
