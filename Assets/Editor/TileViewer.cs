using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileViewer : MonoBehaviour
{
    List<TileImg> tileList = new List<TileImg>();
    Vector2 scrollPosition = Vector2.zero;
    TileImg selTile = null;

    public void SetTile(List<TileImg> tileImg)
    {
        tileList.Clear();
        foreach(TileImg i in tileImg)
        {
            tileList.Add(i);
        }
    }

    public void Draw(int x, int y, int width, int height, int column)
    {        
        int rest = tileList.Count % column;
        int rowCount = tileList.Count + (rest > 0 ? 1 : 0); 

        GUILayout.BeginArea(new Rect(x, y, width, height), EditorStyles.textField);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(width), GUILayout.Height(height));

        for (int i = 0; i < rowCount; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < column; j++)
            {
                int index = i * column + j;
                if (index < tileList.Count)
                {
                    bool state = GUILayout.Button(tileList[index].texture, GUILayout.Width(100), GUILayout.Height(100));
                    selTile = tileList[index];
                }
                else
                    break;
            }
            GUILayout.EndHorizontal();
        }        

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
}
