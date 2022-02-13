using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField]
    private int scorePerHit = 12;
    [SerializeField]
    private int score;

    private Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    // Start is called before the first frame update

    public void ScoreHit()
    {
        score += scorePerHit;
        scoreText.text = score.ToString();
    }
}
