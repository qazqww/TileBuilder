// TileBuilder 창에 버튼들을 그려줌

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileViewer : MonoBehaviour
{
    List<TileImg> tileList = new List<TileImg>();
    Vector2 scrollPosition = Vector2.zero;

    TileImg selTile = null;
    public TileImg SelTile
    {
        get { return selTile; }
    }

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
        int remain = tileList.Count % column;
        int rowCount = tileList.Count + (remain > 0 ? 1 : 0);

        // GUIContent는 렌더링할 대상을 정의하고, (시각적인 매체를 추가할 수 있는듯)
        // GUIStyle은 렌더링하는 방법을 정의한다. (양식, 수치 조절 등)

        GUIStyle guiStyle = new GUIStyle("Label");
        guiStyle.imagePosition = ImagePosition.ImageAbove;

        GUILayout.BeginArea(new Rect(x, y, width, height), EditorStyles.textField);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(width-5), GUILayout.Height(height));

        for (int i = 0; i < rowCount; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < column; j++)
            {
                int index = i * column + j;
                if (index < tileList.Count)
                {
                    GUIContent guiContent = new GUIContent(tileList[index].texture.name, tileList[index].texture);
                    
                    bool state = GUILayout.Button(guiContent, guiStyle, GUILayout.Width(150), GUILayout.Height(150));
                    if (state)
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
