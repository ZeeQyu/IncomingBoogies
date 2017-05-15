using UnityEngine;
using System.Collections;

public class Rotating_background : MonoBehaviour

//Listar ut hur jag kan få bakgrunden att rotera
{
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 0.03f));
    }
}
