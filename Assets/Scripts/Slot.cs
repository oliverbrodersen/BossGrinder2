using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Sprite item;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("Item").GetComponent<Image>().sprite = item;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
