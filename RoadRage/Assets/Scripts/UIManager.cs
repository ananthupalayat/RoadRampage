using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_CarsEscapedCounter;

    [SerializeField] private TextMeshProUGUI m_RedCarsKilled;

    [SerializeField] private GameObject m_GameOverPanel;

    [SerializeField] private GameObject m_ShowUIPopUP;



    /// <summary>
    /// Updates Respective Counters
    /// </summary>
    /// <param name="value"></param>
    public void UpdateCarsEscapedCounter(int value)
    {
        m_CarsEscapedCounter.text = "Cars Escaped: " + value;
    }


    public void UpdateCarsKilledCounter(int value)
    {
        m_RedCarsKilled.text = "Reds Killed: " + value;
    }

    public void ShowGameOverScreen()
    {
        m_GameOverPanel.SetActive(true);
    }

    /// <summary>
    /// Shows PopUp UI on Red Cars
    /// </summary>
    /// <param name="position"></param>
    public void ShowUIPopUP(Vector3 position)
    {
        var spawnedPopUp = ObjectPool.pInstance.GetObject(m_ShowUIPopUP);
        spawnedPopUp.transform.position = position+Vector3.up;
        StartCoroutine(TurnOffPop(spawnedPopUp));
    }

    IEnumerator TurnOffPop(GameObject popUP)
    {
        yield return new WaitForSeconds(3);
        popUP.gameObject.SetActive(false);
    }

}
