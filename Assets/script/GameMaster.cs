using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameMaster : MonoBehaviour
{
    [SerializeField] float required_rescue = 3;
    [SerializeField] GameObject win_screen;
    [SerializeField] GameObject lose_screen;
    [SerializeField] GameObject cheater_screen;
    [SerializeField] AudioClip bg_music;
    [SerializeField] GameObject popup_obj;

    // for endless mode
    private bool endless;
    // this include spawn mechanic
    [SerializeField] GameObject spawn_soldier;
    [SerializeField] GameObject spawn_tree;
    // for spawn area
    [SerializeField] GameObject spawn_area_soldier;
    [SerializeField] GameObject spawn_area_tree;
    //spawn timer;
    [SerializeField] float tree_spawn_lower;
    [SerializeField] float tree_spawn_upper;
    [SerializeField] float tree_destroy_lower;
    [SerializeField] float tree_destroy_upper;
    [SerializeField] float soldier_spawn_lower;
    [SerializeField] float soldier_spawn_upper;
    // current spawn interval
    private float soldier_interval;
    private float tree_interval;
    private float tree_die_interval;
    // time counter
    float wait_soldier = 0;
    float wait_tree_create = 0;
    float wait_tree_die = 0;

    List<GameObject> trees;

    public float total_score = 0;

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
        win_screen.SetActive(false);
        lose_screen.SetActive(false);
        cheater_screen.SetActive(false);
        endless = false;
        trees = new List<GameObject>();

        SoundMaster.Instance.playBackground(bg_music, transform, 1f);

        SpawnMonster(required_rescue);
        soldier_interval = _resetInterval(soldier_spawn_lower, soldier_spawn_upper);
        tree_interval = _resetInterval(tree_spawn_lower, tree_spawn_upper);
        tree_die_interval = _resetInterval(tree_destroy_lower, tree_destroy_upper);

        SpawnTree();
        SpawnTree();
        SpawnTree();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

        if (endless)
        {
            wait_soldier += Time.deltaTime;
            wait_tree_create += Time.deltaTime;
            wait_tree_die += Time.deltaTime;

            // every soldier interval, spawn new monster
            if (wait_soldier > soldier_interval)
            {
                // reset timer
                wait_soldier = 0;
                // randonly spawn 1 to 3 monsters
                SpawnMonster(Random.Range(1, 3));
                //reset new interval
                soldier_interval = _resetInterval(soldier_spawn_lower, soldier_spawn_upper);
            }

            // every tree interval, spawn a tree
            if (wait_tree_create > tree_interval)
            {
                // reset timer
                wait_tree_create = 0;
                // randonly spawn 1 to 3 monsters
                SpawnTree();
                //reset new interval
                tree_interval = _resetInterval(tree_spawn_lower, tree_spawn_upper);
            }

            // every tree interval, destroy a tree
            if (wait_tree_die > tree_die_interval)
            {
                // reset timer
                wait_tree_die = 0;
                // randonly spawn 1 to 3 monsters
                DestroyTree();
                //reset new interval
                tree_die_interval = _resetInterval(tree_destroy_lower, tree_destroy_upper);
            }

        }
    }

    private float _resetInterval(float lower, float upper)
    {
        return Random.Range(lower, upper);
    }   

    public void SpawnMonster(float no_spawn)
    {
        SpriteRenderer spawnarea = spawn_area_soldier.GetComponent<SpriteRenderer>();

        for (int i = 0; i < no_spawn; i++) {
            Vector2 pos = new Vector2( Random.Range(spawnarea.bounds.min.x, spawnarea.bounds.max.x),
                Random.Range(spawnarea.bounds.min.y, spawnarea.bounds.max.y));

            Instantiate(spawn_soldier, pos, Quaternion.identity);
            
        }
    }

    public void SpawnTree()
    {
        SpriteRenderer spawnarea = spawn_area_tree.GetComponent<SpriteRenderer>();
        
        Vector2 pos = new Vector2(Random.Range(spawnarea.bounds.min.x, spawnarea.bounds.max.x),
            Random.Range(spawnarea.bounds.min.y, spawnarea.bounds.max.y));

        GameObject new_tree = Instantiate(spawn_tree, pos, Quaternion.identity);
        trees.Add(new_tree);
    }

    public void DestroyTree()
    {
        int destroyTree = Random.Range(0, trees.Count);
        GameObject token = trees[destroyTree];
        trees.RemoveAt(destroyTree);
        Destroy(token);
    }

    public void TriggerEndless()
    {
        win_screen.SetActive(false);
        endless = true;
        SpawnMonster(Random.Range(1, 3));
    }

    public bool CheckEndless() { return endless; }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SpamPopup(string message, Vector3 pos)
    {
        Debug.Log(pos);
        GameObject popup = Instantiate(popup_obj, pos, new Quaternion());
        popup.GetComponent<popup>().value = message;
        Destroy(popup,1f);
    }

    public bool WinGame(float cur_rescue)
    {
        if (cur_rescue >= required_rescue)
        {
            if (!endless)
            {
                //RestartLevel();
                win_screen.SetActive(true);
            }
            return true;
        }
        else
        {
            required_rescue = required_rescue- cur_rescue;
        }

        total_score += cur_rescue;

        return false;
    }

    public void LoseGame()
    {
        //Debug.Log("You lose");
        lose_screen.SetActive(true);
        return;
    }
    
    public void CheaterDetected()
    {
        //Debug.Log("You lose");
        cheater_screen.SetActive(true);
        return;
    }

    public float getTotalScore() { 
        return total_score;
    }

}
