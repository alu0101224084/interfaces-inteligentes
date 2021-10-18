using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int counter;
    public int id;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name);
        Debug.Log(id);
    }

    // Update is called once per frame
    void Update()
    {   
        counter++;
        Debug.Log(counter);
        
    }
}
