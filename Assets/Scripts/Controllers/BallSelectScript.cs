using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BallSelectScript : MonoBehaviour
{
    private List<Button> buttons = new List<Button>();

    private void Awake()
    {
        GetButtonsAndAddListeners();
    }

    void GetButtonsAndAddListeners()
    {
        GameObject[] allButtons = GameObject.FindGameObjectsWithTag("MenuBall");

        for(int i = 0; i < allButtons.Length; i++)
        {
            buttons.Add(allButtons[i].GetComponent<Button>());
            buttons[i].onClick.AddListener(()=>SelectABall());
        }
    }

    public void SelectABall()
    {
        int index = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        if (GameManager.instance != null)
        {
            GameManager.instance.SetBallIndex(index);
        }
    }
}
