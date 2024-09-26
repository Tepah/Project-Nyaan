using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    public float bottomBound = -6f;

    void Update()
    {
        if (transform.position.y < bottomBound)
        {
            Destroy(gameObject);
        }
    }
}