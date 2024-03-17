using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject Projectile;
    public Transform ProjectileTransform;
    public bool canFire;
    private float timer;
    public float FireRate;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x)*Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0,0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > FireRate)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            Instantiate(Projectile, ProjectileTransform.position, Quaternion.identity);
        }
    }
}
