using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [SerializeField] float required_rescue = 3;
    [SerializeField] GameObject win_screen;
    [SerializeField] GameObject lose_screen;

    private static GameMaster _instance =null;
    public static GameMaster Instance {
        get { return _instance; } 
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        //win_screen = GameObject.Find("YouWinCan");
        //lose_screen = GameObject.Find("YouLoseCan");
        win_screen.SetActive(false);
        lose_screen.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void TriggerEndless()
    {
        Debug.Log("stay");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WinGame(float cur_rescue)
    {
        if (cur_rescue >= required_rescue)
        {
            //RestartLevel();
            win_screen.SetActive(true);
            Debug.Log("you win");
        }
        else
        {
            Debug.Log("The game is not finish");
        }
        return;
    }

    public void LoseGame()
    {
        //Debug.Log("You lose");
        lose_screen.SetActive(true);
        return;
    }
}
