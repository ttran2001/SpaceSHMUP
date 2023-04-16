using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero  S; //Singleton 

    [Header("Set in Inspector")]
    public float speed = 30; 
    public float rollMult = -45; 
    public float pitchMult = 30; 
    public GameObject projectilePrefab;
    public float porjectileSpeed = 40f;

    [Header("Set Dynamically")]
    public float shieldLevel = 1;
    
    void Awake()
    {
        if (S == null){
            S = this; // Set the singleton 
        } else {
            UnityEngine.Debug.LogError("Hero.Awake()");
        }
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(yAxis*pitchMult,xAxis*rollMult,0);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void Fire(){
        GameObject projGo = Instantiate<GameObject>(projectilePrefab);
        projGo.transform.position = transform.position; 
        Rigidbody rb = projGo.GetComponent<Rigidbody>(); 
        rb.velocity = Vector3.up * porjectileSpeed;
    }

    void OnTriggerEnter(Collider other){
        Transform rootT = other.gameObject.transform.root; 
        GameObject go = rootT.gameObject; 

        Destroy(go);
        shieldLevel--;
        if(shieldLevel < 0){
            Main.HERO_DIED();
            Destroy(this.gameObject);
        }
    }

    
}
