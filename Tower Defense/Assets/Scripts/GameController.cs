using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Player lives and UI display
    public int lives = 15;
    public Text livesText;

    //Player available money and UI display
    public int money = 100;
    public Text moneyText;

    //Level and stage and UI display
    public Text levelStageText;

    void Start()
    {
        //Assign UI elements correctly
        GameObject[] UIList = GameObject.FindGameObjectsWithTag("UI");
        foreach(GameObject elem in UIList)
        {

            if (elem.name == "Lives")
            {
                livesText = elem.GetComponent<Text>();
                livesText.text = "Lives: " + lives;
            }
            else if(elem.name == "Money")
            {
                moneyText = elem.GetComponent<Text>();
                moneyText.text = "Money: " + money;
            }
            else if(elem.name == "Stage")
            {
                levelStageText = elem.GetComponent<Text>();
                levelStageText.text = "Level/Stage: " + 1 + "/" + 1;
            }
        }
    }

    //Used when the stage is changed to update UI
    public void StageChange(int level, int stage)
    {
        levelStageText.text = "Level/Stage: " + level + "/" + stage;
    }

    //Used when lives are lost to update display
    public void LivesLost(int damage)
    {
        lives = lives - damage;
        StartCoroutine(flashLivesText());

        if (lives <= 0)
        {

        }
    }

    //Makes health text flash red when lives are being lost
    public IEnumerator flashLivesText()
    {
        livesText.color = Color.red;
        yield return new WaitForSeconds(.2f);
        livesText.text = "Lives: " + lives;
        livesText.color = Color.white;
    }
}
