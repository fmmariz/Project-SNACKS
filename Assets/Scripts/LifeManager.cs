using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public static int currentLife;

    public Image lifePrefab;
    public Transform LifePoints;
    public GameObject GameOverMessage;
    
    public int maxLife = 3;

    private float heartSpacing = 70.0f;
   
    private void Start()
    {
        GameOverMessage.SetActive(false);
 
        currentLife = maxLife;
        UpdateUI();
    }

    public void UpdateUI()
    {
        // Clear all existing hearts
        foreach (Transform child in LifePoints)
        {
            Destroy(child.gameObject);
        }

        float xPos = -867;

 
        // Position hearts based on the current life
        for (int i = 0; i < currentLife; i++)
        {
            Image newHeart = Instantiate(lifePrefab, LifePoints);

            Vector3 newPosition = new Vector3(xPos, 444.0f, 0.0f);
            newHeart.rectTransform.anchoredPosition = newPosition;

            xPos += heartSpacing;
        }


    }

    public void Update()
    {
        if (currentLife == 0)
        {
            GameOverMessage.SetActive(true);
            GameController.Instance.SetCurrentGameState(GameController.GameState.GAMEOVER);
        }
    }

    public void ReloadGame()
    {
        maxLife = 3;
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
