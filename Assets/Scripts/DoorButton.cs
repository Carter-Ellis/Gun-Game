using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    SpriteRenderer sprite;
    AudioSource source;
    public GameObject door;
    public AudioClip pushButton, releaseButton;
    public Sprite pressedButton;
    public Sprite Button;
    public Sprite openDoor;
    Sprite closeDoor;
    bool isPressed = false;

    void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        source = gameObject.GetComponent<AudioSource>();
        if (door != null) 
        {
            closeDoor = door.GetComponent<SpriteRenderer>().sprite;
        }
        
        
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!isPressed)
        {
            sprite.sprite = pressedButton;
            source.clip = pushButton;
            source.Play();
            isPressed = true;
            if (door != null)
            {
                door.GetComponent<BoxCollider2D>().enabled = false;
                door.GetComponent<SpriteRenderer>().sprite = openDoor;
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (isPressed) 
        {
            sprite.sprite = Button;
            source.clip = releaseButton;
            source.Play();
            isPressed = false;
            if (door != null)
            {
                door.GetComponent<BoxCollider2D>().enabled = true;
                door.GetComponent<SpriteRenderer>().sprite = closeDoor;
            }
        }
        
    }

}
