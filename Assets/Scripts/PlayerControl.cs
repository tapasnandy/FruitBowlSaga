using TMPro;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Player Control")]
    public float moveSpeed = 5f; 
    public float smoothFactor = 0.1f; 
    private float targetPositionX; 
    private float targetPositionmobileX; 
    public float minX = -8f; 
    public float maxX = 8f;

    

    

    void Update()
    {

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            float mobileAccelerationInput = Input.acceleration.x;

            targetPositionmobileX += mobileAccelerationInput * moveSpeed * Time.deltaTime;
            targetPositionmobileX = Mathf.Clamp(targetPositionmobileX, minX, maxX);

            float smoothPositionMobileX = Mathf.Lerp(transform.position.x, targetPositionmobileX, smoothFactor);
            transform.position = new Vector3(smoothPositionMobileX, transform.position.y, transform.position.z);
        }

        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            targetPositionX += horizontalInput * moveSpeed * Time.deltaTime;
            targetPositionX = Mathf.Clamp(targetPositionX, minX, maxX);

            float smoothPositionX = Mathf.Lerp(transform.position.x, targetPositionX, smoothFactor);
            transform.position = new Vector3(smoothPositionX, transform.position.y, transform.position.z);
        }
        
    }


    

}
