using UnityEngine;

public class food : MonoBehaviour
{
    // Start is called before the first frame update
    public string foodType;
    
    private void onTriggerEnter2D(Collider2D other)
    {
        Debug.Log("food collided with " + other.tag);
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
