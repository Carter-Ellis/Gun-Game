using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    Shoot shoot;
    public bool isBlue;
    void Awake()
    {
        shoot = GameObject.FindObjectOfType<Shoot>();
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.enabled)
        {
            if (coll.tag == "Item")
            {
                return;
            }
            GameObject obj = coll.gameObject;
            GameObject exitPortal = isBlue ? shoot.orangePortal : shoot.bluePortal;
            if (exitPortal == null) 
            {
                return;
            }
            Vector3 exitDir = exitPortal.transform.up.normalized / 2;
            Vector3 newPos = exitPortal.transform.position + exitDir * shoot.portalSpawnDist;
            newPos.z = obj.transform.position.z;
            obj.transform.position = newPos;
            Rigidbody2D rigidbody = obj.GetComponent<Rigidbody2D>();
            Vector3 enterDir = this.transform.up.normalized;
            Vector3 velo = rigidbody.velocity;
            float deltaVelo = Vector3.Dot(velo, enterDir);
            velo -= deltaVelo * enterDir;
            velo -= deltaVelo * exitDir;
            rigidbody.velocity = velo;

        }
    }



}
