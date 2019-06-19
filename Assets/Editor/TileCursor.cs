// Scene 뷰에서 타일 작업을 할 수 있도록 해줌

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// SceneView.onSceneGUIDelegate(SceneView sceneView) (델리게이트 함수)
// Scene에서 사용되는 GUI의 업데이트가 필요할 때 함수를 연결하여 사용

// Handles (클래스)
// SceneView에서 식별값을 그리기 위한 클래스

public class TileCursor
{
    Vector3[] vertex = new Vector3[4];

    bool isShow;
    public bool IsShow
    {
        get { return isShow; }
    }

    public void SetShow(bool state)
    {
        isShow = state;

        if (state)
            SceneView.onSceneGUIDelegate += OnSceneFunc;
        else
            SceneView.onSceneGUIDelegate -= OnSceneFunc;
    }

    Vector3 clickPos = Vector3.zero;
    public Vector3 ClickPos
    {
        get { return clickPos; }
    }

    bool clickState = false;
    public bool IsClick
    {
        get { return clickState; }
    }

    public void SetClickState (bool state)
    {
        clickState = state;
    }

    /* 1. 마우스의 위치좌표를 화면좌표계로 바꾼다.
    * 2. 화면좌표계를 월드좌표계르 바꾼다.
    * 3. 월드 좌표계(실수)를 정수 형태로 바꾼다.
    */
    void OnSceneFunc(SceneView sceneView)
    {
        Event @event = Event.current;

        // @event.mousePosition은 뷰포트? 좌표계 (좌상단이 원점)
        // Input.mousePosition은 스크린 좌표계 (좌하단이 원점)
        Vector3 pos = new Vector3(@event.mousePosition.x, Camera.current.pixelHeight - @event.mousePosition.y, 0); // 1번 과정
        pos = Camera.current.ScreenToWorldPoint(pos); // 2번 과정
        pos.Set(Mathf.Floor(pos.x) + 0.5f, Mathf.Floor(pos.y) + 0.5f, 0); // 3번 과정

        vertex[0] = new Vector3(pos.x - 0.5f, pos.y + 0.5f, 0);
        vertex[1] = new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0);
        vertex[2] = new Vector3(pos.x + 0.5f, pos.y - 0.5f, 0);
        vertex[3] = new Vector3(pos.x - 0.5f, pos.y - 0.5f, 0);
               
        Handles.DrawSolidRectangleWithOutline(vertex, new Color(1, 0, 0, 0.2f), new Color(0, 0, 0, 1));
        // 현재 마우스 커서에 사각형을 그렸으므로, 신 뷰를 다시 그려줌
        SceneView.RepaintAll();

        if(@event.type == EventType.MouseDown)
        {
            clickPos = pos;
            clickState = true;
        }
    }
}
