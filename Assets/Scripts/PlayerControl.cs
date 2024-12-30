using TMPro;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Player Control")]
    public float moveSpeed = 5f; 
    public float smoothFactor = 0.1f; 
    private float targetPositionX; 
    public float minX = -8f; 
    public float maxX = 8f;

    

    

    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");


        targetPositionX += horizontalInput * moveSpeed * Time.deltaTime;

        targetPositionX = Mathf.Clamp(targetPositionX, minX, maxX);


        // Smoothly move towards the target position using Lerp
        float smoothPositionX = Mathf.Lerp(transform.position.x, targetPositionX, smoothFactor);

        // Apply the smooth movement to the GameObject
        transform.position = new Vector3(smoothPositionX, transform.position.y, transform.position.z);
    }


    

}
