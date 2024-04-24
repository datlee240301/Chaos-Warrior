using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject item;
    
    public void Spawn() {
        Instantiate(item,new Vector2(transform.position.x, transform.position.y - 1.25f),transform.rotation);
    }
}
