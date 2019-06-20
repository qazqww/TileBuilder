using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InsertTag
{
    string mainTag;    
    public string MainTag
    {
        get { return mainTag; }
    }

    string subTag;
    public string SubTag
    {
        get { return subTag; }
    }

    public void Draw(int x, int y, int width, int height, ref bool mainState)
    {
        GUILayout.BeginArea(new Rect(x, y, width, height), EditorStyles.textField);

        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Main Tag: ", GUILayout.Width(60));
            mainTag = GUILayout.TextField(mainTag);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Sub Tag: ", GUILayout.Width(60));
            subTag = GUILayout.TextField(subTag);            
            GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        if (GUILayout.Button("ADD", GUILayout.Width(40), GUILayout.Height(30)))
            mainState = true;
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
}
