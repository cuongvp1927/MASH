using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [SerializeField] float required_rescue = 3;

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
        
    }

    public void WinGame(float cur_rescue)
    {
        if (cur_rescue >= required_rescue)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        Debug.Log("You lose");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        return;
    }
}
