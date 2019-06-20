// 유니티 에디터에 메뉴를 추가

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class OpenBuilder
{
    // 유니티 에디터 상에 메뉴를 추가할 수 있음!
    // 메뉴 실행 시 바로 밑에 붙은 함수가 작동
    [MenuItem("Window/OpenBuilder")]
    public static void OpenMenu()
    {
        TileBuilder builder = (TileBuilder)EditorWindow.GetWindow(typeof(TileBuilder));
        builder.minSize = new Vector2(600, 600);
    }
}
