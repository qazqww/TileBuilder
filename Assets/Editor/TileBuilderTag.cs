using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    
    public void SaveTag(string path, string filename)
    {
        if(string.IsNullOrEmpty(path) || tagDic.Count == 0)
            return;

        if(!Directory.Exists(path))
            Directory.CreateDirectory(path);

        if (File.Exists(path))
            File.Delete(path);

        string savepath = path + filename;
        File.Create(savepath).Close();
        TextWriter writer = new StreamWriter(savepath); // TextWriter는 추상 클래스, StreamWriter는 TextWriter를 상속받음


        foreach(var pair in tagDic)
        {
            string mainKey = pair.Key + "|";
            string strSubString = string.Empty;
            foreach (var subKey in pair.Value)
            {
                strSubString += (subKey + ",");
            }
            mainKey += strSubString;
            mainKey = mainKey.Remove(mainKey.Length - 1);
            writer.WriteLine(mainKey);
        }

        writer.Close();
    }

    public void LoadTag(string path)
    {
        tagDic.Clear();

        TextAsset textAsset = Resources.Load<TextAsset>(path);
        if (textAsset != null)
            return;

        char[] removeCh = { '\r', '\n' };

        string[] row = textAsset.text.Split(removeCh, System.StringSplitOptions.RemoveEmptyEntries); // 비어있는 요소를 배제하는 옵션
        for (int i = 0; i < row.Length; i++)
        {
            string[] col = row[i].Split('|');
            string mainKey = col[0];
            if(col.Length > 1)
            {
                string[] subKey = col[1].Split(',');
                for (int j = 0; j < subKey.Length; j++)
                    AddTag(mainTag, subKey[j]);
            }
        }
    }
}
