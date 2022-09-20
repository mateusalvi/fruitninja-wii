using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class uiController : MonoBehaviour
{
    [SerializeField] int initialHealth;
    private int health;
    public int score;
    public GameObject[] hearts;
    [SerializeField] GameObject scoreCounter;
    private TextMeshProUGUI scoreText;
    public GameObject gameUI, mainMenu, postPlay;
    public Camera mainCamera;
    [SerializeField] GameObject EndScore;

    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
        score = 0;
        scoreText = scoreCounter.GetComponent<TextMeshProUGUI>();
        scoreText.text = score.ToString();
        SetEnabledUI("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        ManageUIClicks();
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;

        if (health <= 0)
        {
            hearts[0].SetActive(false);
            hearts[1].SetActive(false);
            hearts[2].SetActive(false);
            SetEnabledUI("PostGame");
            return;
        }

        switch (health)
        {
            case 2: hearts[0].SetActive(false);
            break;
            case 1: hearts[1].SetActive(false);
            break;
            case 0: hearts[2].SetActive(false);
            break;
        }
    }

    public void Heal(int heal)
    {
        health = health + heal;

        switch (health)
        {
            case 0:
                hearts[0].SetActive(true);
                break;
            case 1:
                hearts[1].SetActive(true);
                break;
        }
    }
    public void ChangeScore(int points)
    {
        score = score + points;
        scoreText.text = score.ToString();
    }
    
    public void SetEnabledUI(string UIChildName)
    //UIChildNames: "MainMenu", "GameUI" and "PostGame"//
    {
        if (UIChildName == "MainMenu")
        {
            //ENABLE AND DISABLE UIs ACORDINGLY
            mainMenu.SetActive(true);
            gameUI.SetActive(false);
            postPlay.SetActive(false);

            //DISABLE SPAWNERS
            mainCamera.transform.GetChild(0).gameObject.SetActive(false);
            mainCamera.transform.GetChild(1).gameObject.SetActive(false);

        }
        else if (UIChildName == "GameUI")
        {
            for (int i = 0; i < initialHealth; i++)
            {
                hearts[i].SetActive(true);
                health = initialHealth;
            }
            score = 0;
            scoreText.text = "0";
            mainMenu.SetActive(false);
            gameUI.SetActive(true);
            postPlay.SetActive(false);
            mainCamera.transform.GetChild(0).gameObject.SetActive(true);
            mainCamera.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (UIChildName == "PostGame")
        {
            mainMenu.SetActive(false);
            gameUI.SetActive(false);
            postPlay.SetActive(true);

            EndScore.GetComponent<TextMeshProUGUI>().text = score.ToString();

            mainCamera.transform.GetChild(0).gameObject.SetActive(false);
            mainCamera.transform.GetChild(1).gameObject.SetActive(false);
        }
        else return;
    }

    private void ManageUIClicks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsPointerOverUIElement();
        }
    }

    //COURTESY OF Krishx007 - unity forums
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    ///Returns 'true' if we touched or hovering on Unity UI element.
    public bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.name == "Novo Jogo")
            {
                SetEnabledUI("GameUI");
                score = 0;
            }
            if (curRaysastResult.gameObject.name == "Sair")
            {
                Application.Quit(0);
            }
            if (curRaysastResult.gameObject.name == "Jogar Novamente")
            {
                SetEnabledUI("GameUI");
                score = 0;
            }
            if(curRaysastResult.gameObject.name == "BackToMenu")
            {
                SetEnabledUI("MainMenu");
            }
                
        }
        return false;
    }

    ///Gets all event systen raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}