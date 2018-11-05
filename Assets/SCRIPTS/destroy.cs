using UnityEngine;
using System.Collections;

public class destroy : MonoBehaviour
{
    public bool destruir = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (destruir == true)
        {
            Destroy(gameObject);
        }
    }
}
