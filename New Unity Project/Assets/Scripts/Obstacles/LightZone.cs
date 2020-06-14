using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightZone : MonoBehaviour
{
    public float zoneRadius, bulletChangedSpeed, reduceZoneSpeed;
    private UnityEngine.Experimental.Rendering.Universal.Light2D lightController;
    [HideInInspector]
    public bool isDead = false;
    void Start()
    {
        SetZone();
    }

    private void FixedUpdate()
    {
        if (isDead && lightController.pointLightOuterRadius > 0)
            Shrink();
        else if (lightController.pointLightOuterRadius <= 0)
            GetComponent<CircleCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 10) // 10 is bullet
        {
            col.GetComponent<bullet>().SlowBullet(bulletChangedSpeed);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 10) // 10 is bullet
        {
            col.GetComponent<bullet>().SetSpeedNormal();
        }
    }

    private void Shrink()
    {
        lightController.pointLightOuterRadius -= Time.deltaTime * reduceZoneSpeed;
    }

    public void SetZone()
    {
        isDead = false;
        lightController = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        lightController.pointLightOuterRadius = zoneRadius;
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<CircleCollider2D>().radius = zoneRadius;
    }
}
