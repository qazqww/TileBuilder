using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileInfo
{
    int width;
    int height;

    public void Draw(int x, int y, int width, int height, TileImg tileImg)
    {                
        int layer = 0;
        this.width = 0;
        this.height = 0;
        string imageName = null;

        Texture2D texture = null;
        Color color = Color.white;
        ColliderType colliderType = ColliderType.None;
        TileType tileType = TileType.None;

        // 타일 이미지의 정보를 받아옴
        if(tileImg != null && tileImg.texture != null)
        {
            tileType = tileImg.tileType;
            colliderType = tileImg.colliderType;
            color = tileImg.color;

            this.width = (int)tileImg.sprite.textureRect.width;
            this.height = (int)tileImg.sprite.textureRect.height;
            imageName = tileImg.sprite.name;
            texture = tileImg.texture;
        }

        GUILayout.BeginArea(new Rect(x, y, width, height), EditorStyles.textField);

        #region 이미지의 사이즈 출력
        GUILayout.BeginHorizontal();
        GUILayout.Box("Width: " + this.width);
        //EditorGUILayout.IntField("", this.width, EditorStyles.numberField, 
        //                         GUILayout.MinWidth(30), GUILayout.MaxWidth(40), GUILayout.MinHeight(22));

        GUILayout.Box("Height: " + this.height);
        //EditorGUILayout.IntField("", this.height, EditorStyles.numberField, 
        //                         GUILayout.MinWidth(30), GUILayout.MaxWidth(40), GUILayout.MinHeight(22));

        GUILayout.EndHorizontal();
        #endregion
        
        #region 이미지 출력
        GUILayout.BeginHorizontal();
        GUILayout.Label("Image :");
        GUILayout.Button(texture, GUILayout.Width(100), GUILayout.Height(100));
        GUILayout.EndHorizontal();
        #endregion

        GUILayout.Label("Image Name: " + imageName);
        layer = tileImg != null ? tileImg.layerOrder : 0;
        GUILayout.Label("Layer Order: " + layer);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<", GUILayout.Width(93)))
        {
            if (tileImg != null)
                tileImg.layerOrder--;
        }
        if (GUILayout.Button(">", GUILayout.Width(93)))
        {
            if (tileImg != null)
                tileImg.layerOrder++;
        }
        GUILayout.EndHorizontal();

        color = EditorGUILayout.ColorField(color);

        GUILayout.Label("Collider Type");
        colliderType = (ColliderType)EditorGUILayout.EnumPopup(colliderType);

        GUILayout.Label("Tile Type");
        tileType = (TileType)EditorGUILayout.EnumPopup(tileType);

        if (tileImg != null)
        {
            tileImg.color = color;
            tileImg.colliderType = colliderType;
            tileImg.tileType = tileType;
        }

        GUILayout.EndArea();
    }
}
