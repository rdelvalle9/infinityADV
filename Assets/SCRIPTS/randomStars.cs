using UnityEngine;
using System.Collections;

// crea estrellas(no se esta usando)
public class randomStars : MonoBehaviour
{
    public GameObject stars;
    private Vector3 randomVector;
    public float x = -30;
    public float y = 30;
    // Update is called once per frame
    void Update()
    {
        randomVector = new Vector3(Random.Range(x, y), Random.Range(-15, -3), 35);
        Instantiate(stars, randomVector, transform.rotation);
    }
}
