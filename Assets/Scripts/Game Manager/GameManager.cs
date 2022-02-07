using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int index = 0;

    private BallCreator ballCreator;

    private void Awake()
    {
        MakeSingleton();

        ballCreator = GetComponent<BallCreator>();
    }
    
    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void CreateBall()
    {
        ballCreator.CreateBall(index);
    }

    public void SetBallIndex(int index)
    {
        this.index = index;
    }

    private void OnLevelWasLoaded()
    {
        if(SceneManager.GetActiveScene().name == "GamePlay")
        {
            CreateBall();
        }
    }
}
