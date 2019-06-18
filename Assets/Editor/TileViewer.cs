using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileViewer : MonoBehaviour
{
    List<TileImg> tileList = new List<TileImg>();

    public void SetTile(List<TileImg> tileImg)
    {
        tileList.Clear();
        foreach(TileImg i in tileImg)
        {
            tileList.Add(i);
        }
    }

    public void Draw(int x, int y, int width, int height)
    {
        GUILayout.BeginArea(new Rect(x, y, width, height), EditorStyles.textField);
        GUILayout.EndArea();
    }
}
