using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Transform Origin;
    Animator m_Animator;
    public Potion potion;
    private bool _triggered;

    void Start()
    {
        //Get the Animator attached to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player" || _triggered)
            return;

        _triggered = true;
        FindObjectOfType<AudioManager>().Play("open-chest");
        m_Animator.SetTrigger("ChestOpen");
        
        Invoke("open", 2);
    }

    private void open(){
        Destroy(gameObject);
        Instantiate(potion, Origin.position, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
}
