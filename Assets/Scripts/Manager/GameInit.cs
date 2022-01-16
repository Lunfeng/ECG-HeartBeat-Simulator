using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    private GameManager game;

    private void Start()
    {
#if UNITY_EDITOR
        GameManager.DestroyGameManager();
#endif
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        game = GameManager.GetInstance();
        yield return 0;
        game.Init();
    }
}
