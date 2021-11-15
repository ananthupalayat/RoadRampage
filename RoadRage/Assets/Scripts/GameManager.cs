using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int mNumberOfCarEscaped=0;
    private int mNumberOfRedsKilled=0;
    [SerializeField] UIManager mUIManager;

    private bool mGameRestarted = false;

    void OnEnable()
    {
        Car.OnCarEscaped += CarEscaped;
        Car.OnCarHit += OnCarHit;
    }

    private void CarEscaped()
    {
        mNumberOfCarEscaped += 1;
        mUIManager.UpdateCarsEscapedCounter(mNumberOfCarEscaped);
    }

    private void OnCarHit(Car.CarType carType,Vector3 position)
    {
        
        if (carType == Car.CarType.REDCAR)
        {
            mUIManager.ShowUIPopUP(position);
            mNumberOfRedsKilled += 1;
            mUIManager.UpdateCarsKilledCounter(mNumberOfRedsKilled);
        }
        else
        {
            if (!mGameRestarted)
            {
                mUIManager.ShowGameOverScreen();
                Car.OnCarEscaped -= CarEscaped;
                Car.OnCarHit -= OnCarHit;
                mGameRestarted = true;
                Invoke("RestartGame", 5);
            }
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    


}
