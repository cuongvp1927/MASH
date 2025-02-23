using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private float required_rescue = 3;

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
    public void WinGame(float cur_rescue)
    {
        if (cur_rescue > required_rescue)
        {

        }
        Debug.Log("Game win?");
        return;
    }

    public void LoseGame()
    {
        Debug.Log("Game lose?");
        return;
    }
}
