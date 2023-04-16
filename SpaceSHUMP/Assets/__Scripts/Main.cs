using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static private Main S;
    [Header("Inscribed")]
    public GameObject[] prefabEnemies; 
    public float enemySpawnPerSecond = 0.5f;
    public float enemyInsetDefault = 1.5f; 
    public float gameRestartDelay = 2f; 

    private BoundsCheck bndCheck;
    private bool heroIsAlive = true;

    void Awake()
    {
        S = this; 
        bndCheck = GetComponent<BoundsCheck>();

        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond); 
    }

    public void SpawnEnemy()
    {
        if(!heroIsAlive){
            return;
        }

        int ndx = UnityEngine.Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);
        float enemyInset = enemyInsetDefault; 
        if(go.GetComponent<BoundsCheck>() != null)
        {
            enemyInset = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }
        Vector3 pos = Vector3.zero; 
        float xMin = -bndCheck.camWidth + enemyInset;
        float xMax = bndCheck.camWidth - enemyInset;
        pos.x = UnityEngine.Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyInset;
        go.transform.position = pos;

        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void HERO_DIED(){
        S.heroIsAlive = false;
        S.Invoke(nameof(Restart), S.gameRestartDelay);
    }

    void Restart(){
        SceneManager.LoadScene("__Scene_0");
    }
}
