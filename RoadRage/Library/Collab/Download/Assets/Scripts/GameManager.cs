using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int mNumberOfCarEscaped=0;
    [SerializeField] UIManager mUIManager;

    private bool mGameRestarted = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        Car.OnCarEscaped += CarEscaped;
        Car.OnCarHit += OnCarHit;
    }

    private void CarEscaped()
    {
        mNumberOfCarEscaped += 1;
    }

    private void OnCarHit(Car.CarType carType,Vector3 position)
    {
        
        if (carType == Car.CarType.REDCAR)
        {

        }
        else
        {
            if (!mGameRestarted)
            {
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
