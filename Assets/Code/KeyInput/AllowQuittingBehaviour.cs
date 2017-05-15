using UnityEngine;

public class AllowQuittingBehaviour : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Quit"))
        {
            Application.Quit();
        }
    }
}
