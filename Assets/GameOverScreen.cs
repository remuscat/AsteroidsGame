using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString()+ " Points";
    }
    public void RestartButton(){
        SceneManager.LoadScene("Game");

    }

    public void ExitButton(){
        SceneManager.LoadScene("MainMenu");
    }
}
