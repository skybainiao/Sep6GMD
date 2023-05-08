using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum Difficulty { Easy, Normal, Hard }
    public Difficulty currentDifficulty;

    private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject);
    }
}

    public void SetDifficulty(int difficultyIndex)
{
    switch (difficultyIndex)
    {
        case 0:
            currentDifficulty = Difficulty.Easy;
            break;
        case 1:
            currentDifficulty = Difficulty.Normal;
            break;
        case 2:
            currentDifficulty = Difficulty.Hard;
            break;
        default:
            Debug.LogError("Invalid difficulty index");
            break;
    }
}

    public void StartGame()
{
    SceneManager.LoadScene("SampleScene");
    GameObject backgroundMusic = GameObject.Find("BackgroundMusic");
    if (backgroundMusic)
    {
        backgroundMusic.GetComponent<BackgroundMusicController>().PlayBackgroundMusic();
    }
}


    public void SetDifficulty(Difficulty difficulty)
    {
        currentDifficulty = difficulty;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
