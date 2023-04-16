using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHero : MonoBehaviour
{

    private BoundsCheck bndCheck;

    void Awake(){
        bndCheck = GetComponent<BoundsCheck>(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float posy = transform.position.y; 
        if(posy - bndCheck.radius > bndCheck.camHeight){
            Destroy(gameObject);
        }
    }
}
