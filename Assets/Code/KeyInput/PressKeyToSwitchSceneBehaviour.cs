using UnityEngine;
using UnityEngine.SceneManagement;

public class PressKeyToSwitchSceneBehaviour : MonoBehaviour
{
    [SerializeField] private int sceneToSwitchTo = 0;
    [SerializeField] private string keyToPress = "any";

    void Update()
    {
        if (keyToPress == "any" || keyToPress.Length == 0)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene(sceneToSwitchTo);
            }
        }
        else
        {
            if (Input.GetButtonDown(keyToPress))
                SceneManager.LoadScene(sceneToSwitchTo);
        }
    }
}
