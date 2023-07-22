using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    SpriteRenderer sprite;
    AudioSource source;
    public AudioClip pushButton, releaseButton;
    public Sprite pressedButton;
    public Sprite Button;
    void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        source = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        sprite.sprite = pressedButton;
        source.clip = pushButton;
        source.Play();
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        sprite.sprite = Button;
        source.clip = releaseButton;
        source.Play();
    }

}
