using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject MenuPanel;
    PauseManager pauseManager;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuPanel.activeInHierarchy)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void CloseMenu()
    {
        MenuPanel.SetActive(false);
        pauseManager.UnPauseGame();
    }
    public void OpenMenu()
    {
        MenuPanel.SetActive(true);
        pauseManager.PauseGame();
    }
}
