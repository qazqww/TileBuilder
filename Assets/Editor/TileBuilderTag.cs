using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 메인 태그에 따라 다른 서브태그 인덱스를 갖도록 수정한다면?
//

public class TileBuilderTag
{
    Dictionary<string, List<string>> tagDic = new Dictionary<string, List<string>>();

    string mainTag;
    int mainTagIdx;
    int subTagIdx;    

    bool changed = false;
    public bool Changed
    {
        get
        {
            bool state = changed;
            changed = false;
            return state;
        }
    }

    public string Tag
    {
        get
        {
            if(tagDic.ContainsKey(mainTag))
                return tagDic[mainTag][subTagIdx];
            return null;
        }
    }

    public void Draw(int x, int y, int width, int height)
    {
        if (tagDic.Count == 0)
            return;

        GUILayout.BeginArea(new Rect(x, y, width, height), EditorStyles.textField);

        List<string> mainTags = new List<string>();

        foreach(var pair in tagDic)
            mainTags.Add(pair.Key);

        int prevTagIdx = mainTagIdx;
        mainTagIdx = GUILayout.Toolbar(prevTagIdx, mainTags.ToArray(), EditorStyles.toolbarButton);
        mainTag = mainTags[mainTagIdx];

        if (prevTagIdx != mainTagIdx)
        {
            subTagIdx = 0;
            changed = true;
        }

        mainTag = mainTags[mainTagIdx];
        string[] subTags = tagDic[mainTag].ToArray();
        prevTagIdx = subTagIdx;
        subTagIdx = GUILayout.Toolbar(prevTagIdx, subTags, EditorStyles.toolbarButton);

        if (prevTagIdx != subTagIdx)
            changed = true;

        GUILayout.EndArea();
    }

    public void AddTag(string key, string subVal)
    {
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(subVal))
            return;

        if (!tagDic.ContainsKey(key))
            tagDic.Add(key, new List<string>());

        if (!tagDic[key].Contains(subVal))
            tagDic[key].Add(subVal);
    }

    public void SetMainTag(string tag)
    {
        mainTag = tag;
    }
}
