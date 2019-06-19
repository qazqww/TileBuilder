using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Texture icon;

    GUIStyle gUIStyle = new GUIStyle();

    void OnGUI()
    {
        gUIStyle.imagePosition = ImagePosition.ImageAbove;
        GUI.Button(new Rect(500, 500, 200, 200), new GUIContent("text", icon, "tooltip"));
        GUI.Button(new Rect(0, 0, 100, 100), new GUIContent("text", icon, "tooltip"));
    }
}