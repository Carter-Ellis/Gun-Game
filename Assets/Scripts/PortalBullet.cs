using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBullet : MonoBehaviour
{
    public GameObject Portal;
    public bool isBlue;
    Shoot shoot;


    LayerMask layerMask;
    void Awake()
    {
        shoot = GameObject.FindObjectOfType<Shoot>();
        Destroy(gameObject, 3f);
        layerMask = LayerMask.NameToLayer("Ground");
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Destroy(gameObject);
        if (coll.collider.gameObject.layer != layerMask)
        {
            return;
        }   
        if (coll.collider == null)
        {
            return;
        }
        ContactPoint2D hitPoint = coll.GetContact(0);
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, hitPoint.normal);
        GameObject portalIns = Instantiate(Portal, hitPoint.point, rot);
        Vector3 portalPos = portalIns.transform.position;
        portalPos.z = coll.collider.transform.position.z - .1f;
        portalIns.transform.position = portalPos;
        Portal portal = portalIns.GetComponent<Portal>();
        portal.isBlue = isBlue;

        if (isBlue)
        {
            if (shoot.bluePortal != null)
            {
                Destroy(shoot.bluePortal);

            }
            shoot.bluePortal = portalIns;
        }
        else
        {
            if (shoot.orangePortal != null)
            {
                Destroy(shoot.orangePortal);

            }
            shoot.orangePortal = portalIns;
        }
    }
    
}
