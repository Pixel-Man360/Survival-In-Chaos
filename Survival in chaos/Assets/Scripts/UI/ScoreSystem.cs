using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _scoreTextGameOver;

    private int _score = 0;

    void Start()
    {
        
    }

    void OnEnable()
    {
        Balls.OnPointsGained += AddScore;
        PlayerHealthControl.OnPlayerDead += PlayerDied;
    }

    void OnDisable()
    {
        Balls.OnPointsGained -= AddScore;
        PlayerHealthControl.OnPlayerDead -= PlayerDied;
    }

    void AddScore(int score) => _score += score;

    void PlayerDied() => _scoreTextGameOver.SetText("Your Score: " + _score.ToString());

    
    void Update()
    {
        _scoreText.SetText("Score: " + _score.ToString());
    }
}
