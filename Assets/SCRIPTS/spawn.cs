using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour {

    public GameObject bichoCono;

	// Use this for initialization
	void Start () {
        Vector3 rotacion = new Vector3(-70.144f, 58.76f, -6.45f);
	}

    // Update is called once per frame
    void Update()
    {
        //GetKeyUp dice que cuando suelte la tecla ejecuta la accion
        //GetKey dice que mientras toque la tecla ejecuta la accion
        if (Input.GetKeyUp(KeyCode.Space))
        {   
           // transform.rotation = Quaternion.AngleAxis(0, new Vector3(-20.144f, 58.76f, -6.45f));
            Instantiate(bichoCono,new Vector3 (), transform.rotation);
        }
    }

}
