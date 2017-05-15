using UnityEngine;

public class SpriteBarBehaviour : MonoBehaviour
{
    //[SerializeField] private float tempTimeMultiplier;
    [SerializeField] private GameObject centerSquare;
    [SerializeField] private float maxScale = 10.0f;

    private float currentFraction;

    // Behaviour
    void Start()
    {
        currentFraction = 0.0f;
    }
    void Update()
    {
        //// Add time to show progress as a debug measure
        //currentFraction += Time.deltaTime * tempTimeMultiplier;
        //currentFraction = Mathf.Clamp01(currentFraction);

        // Change the scale of the smaller object to give the illusion of a progress bar filling
        float currentScale = Mathf.Lerp(0.0f, maxScale, currentFraction);
        centerSquare.transform.localScale = new Vector3(currentScale, 1.0f, 1.0f);

        // BTW, if the bar fills from middle out, set the center sprite's origin to one of the left ones and place it to the left of the sprite
    }

    // Getters / Setters
    public void SetCurrentFraction(float newCurrentFraction)
    {
        currentFraction = Mathf.Clamp01(newCurrentFraction);
    }
}
