using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{
    [SerializeField]
    private Button playBtn, exitBtn;

    private void Awake()
    {
        playBtn.onClick.AddListener(PlayGame);
        exitBtn.onClick.AddListener(ExitGame);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
