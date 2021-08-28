using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonsScenesAndOtherUI : MonoBehaviour
{
    [SerializeField] private Button _play;
    [SerializeField] private Button _Tutorial;
    [SerializeField] private Button _Exit;
    [SerializeField] private Button _back;
    [SerializeField] private Button _pause;
    [SerializeField] private Button _resume;
    [SerializeField] private Button _pauseRestart;
    [SerializeField] private Button _pauseMainMenu;
    [SerializeField] private Button _gameOverRestart;
    [SerializeField] private Button _gameOverMainMenu;

    [SerializeField] private GameObject _pausePanel;
    void Awake()
    {
        Time.timeScale = 1;

        _play?.onClick.AddListener(PlayGame);
        _Tutorial?.onClick.AddListener(Tutorial);
        _Exit?.onClick.AddListener(Exit);
        _back?.onClick.AddListener(MainMenu);
        _pause?.onClick.AddListener(Pause);
        _resume?.onClick.AddListener(Resume);
        _pauseRestart?.onClick.AddListener(Restart);
        _pauseMainMenu?.onClick.AddListener(MainMenu);
        _gameOverRestart?.onClick.AddListener(Restart);
        _gameOverMainMenu?.onClick.AddListener(MainMenu);
    }

    void PlayGame()
    {
       SceneManager.LoadSceneAsync(2);
    }

    void Tutorial()
    {
        SceneManager.LoadSceneAsync(1);
    }

    void Exit() 
    {
        Application.Quit();
    }

    void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    void Pause()
    {
      Time.timeScale = 0;
      _pausePanel.SetActive(true);
    }

    void Resume()
    {
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
    }

    void Restart()
    {
        SceneManager.LoadSceneAsync(2);
    }

   
}
