//using UnityEngine;
//using UnityEditor;
//using MiniGame;
//using System.Collections;


//[CustomEditor(typeof(ArrayLayout))]
//public class DrawScript : Editor
//{



//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();

//        //Cast target to MyScript
//        MyScript m = (MyScript)target;

//        //Never let user go below 1 w/h
//        m.height = Mathf.Max(1, EditorGUILayout.IntField("Height:", m.height));
//        m.width = Mathf.Max(1, EditorGUILayout.IntField("Width:", m.width));

//        //Check that the array sizes match w/h values
//        CheckArraySizes(m);

//        //Draw popups
//        for (int i = 0; i < m.myThings.Length; i++)
//        {
//            GUILayout.BeginHorizontal();
//            for (int j = 0; j < m.myThings[i].entries.Length; j++)
//            {
//                m.myThings[i].entries[j] = (MyScript.Things)EditorGUILayout.EnumPopup(m.myThings[i].entries[j]);
//            }
//            GUILayout.EndHorizontal();
//        }
//    }

//    void CheckArraySizes(MyScript m)
//    {
//        if (m.myThings == null ||
//            m.myThings.Length == 0 ||
//            m.myThings[0] == null ||
//            m.myThings[0].entries.Length == 0)
//        {
//            //Create/init new array when there isn't one
//            m.myThings = new MyScript.Row[m.height];
//            for (int i = 0; i < m.myThings.Length; i++)
//            {
//                m.myThings[i] = new MyScript.Row();
//                m.myThings[i].entries = new MyScript.Things[m.width];
//            }
//        }
//        else if (m.myThings.Length != m.height)
//        {
//            //resizing number of rows
//            int oldHeight = m.myThings.Length;
//            bool growing = m.height > m.myThings.Length;
//            System.Array.Resize(ref m.myThings, m.height);
//            if (growing)
//            {
//                //Add new rows to array when growing array
//                for (int i = oldHeight; i < m.height; i++)
//                {
//                    m.myThings[i] = new MyScript.Row();
//                    m.myThings[i].entries = new MyScript.Things[m.width];
//                }
//            }

//        }
//        else if (m.myThings[0].entries.Length != m.width)
//        {
//            //resizing number of entries per row
//            for (int i = 0; i < m.myThings.Length; i++)
//            {
//                System.Array.Resize(ref m.myThings[i].entries, m.width);
//            }
//        }
//    }
//}
