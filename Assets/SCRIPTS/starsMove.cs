using UnityEngine;
using System.Collections;

public class starsMove : MonoBehaviour {
    private float zMovement;
    private float timeDestroy = 0.0f;

    void Start()
    {
        zMovement = Random.Range(-5, -20)*Time.deltaTime;//rango para q se muevan a diferente velocidad
    }

    public void Update()
    {
        gameObject.transform.Translate(0, 0, zMovement);
        timeDestroy += Time.deltaTime;
        if (timeDestroy > 5) Destroy(this.gameObject);
    }
}
