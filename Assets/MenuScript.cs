using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class game : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartButton(){
        SceneManager.LoadScene("Game");

    }

    public void ExitButton(){
        // SceneManager.LoadScene("Exit");
    }
}
