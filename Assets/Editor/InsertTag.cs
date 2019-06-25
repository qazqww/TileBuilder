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

    bool save = false;
    public bool Save
    {
        get { return Trigger(ref save); }
    }

    bool load = false;
    public bool Load
    {
        get { return Trigger(ref load); }
    }

    bool Trigger(ref bool trigger)
    {
        bool state = trigger;
        trigger = false;
        return state;
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

        if (GUILayout.Button("ADD", GUILayout.Width(40), GUILayout.Height(35)))
            mainState = true;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("SAVE"))
            save = true;
        if (GUILayout.Button("LOAD"))
            load = true;
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
}
