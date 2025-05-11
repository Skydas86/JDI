using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float start,end;
    [SerializeField] private GameObject player;
    void Start()
    {
        
    }
    void Update()
    {
        var playerX = player.transform.position.x;
        var camX = transform.position.x;
        var camY = transform.position.y;
        var camZ = transform.position.z;

        if (playerX > start && playerX < end)
        {
            camX = playerX;
        }
        else
        {
            if (playerX < start)
            {
                camX = start;
                Debug.Log("da qua");

            }
            else if (playerX > end)
            {
                camX = end;
            }
        }
        transform.position = new Vector3(camX, camY, camZ);
    }
    
}
