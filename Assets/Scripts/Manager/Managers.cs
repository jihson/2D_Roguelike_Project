using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 매니저 인스턴스 유일성 보장
    static Managers Instance { get { init(); return s_instance; } } //모든걸 Instance로 통제

    InputManager _input = new InputManager();
    public static InputManager input { get { return Instance._input; } }

    void Start()
    {
        //초기화
        init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            // 없을 시 생성
            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            //삭제 방지
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
        
    }
}
