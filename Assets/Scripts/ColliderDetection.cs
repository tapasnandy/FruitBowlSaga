using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ColliderDetection : MonoBehaviour
{
    private float moveSpeed = 10f;
    private float scaleUpFactor = 1.3f;  
    private float scaleDuration = 0.1f;

    //[Header("Particle system")]
    //[SerializeField] ParticleSystem bombParticle;
    

    private void Update()
    {
        if(transform.position.y <= -5.5)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.name == "Player")
        {
            
            ScaleUpAndDown(collision.gameObject);
            Destroy(this.gameObject);
        }

        
        if (collision.name == "ed1" || collision.name == "ed")
        {
            StartCoroutine(MoveToPlayerCenter(GameObject.Find("Player").transform));
        }
    }




    private void ScaleUpAndDown(GameObject objectToScale)
    {
        // Store the original scale of the object
        Vector3 originalScale = objectToScale.transform.localScale;

        // Scale the object up and back to the original scale using DOTween
        objectToScale.transform.DOScale(originalScale * scaleUpFactor, scaleDuration)  // Scale up
            .OnComplete(() =>
            {
                // After scaling up, scale it back to the original scale
                objectToScale.transform.DOScale(originalScale, scaleDuration);
            });
    }


    IEnumerator MoveToPlayerCenter(Transform playerTransform)
    {
        Vector3 startPosition = transform.position; // Starting position of this GameObject
        Vector3 targetPosition = playerTransform.position; // Initial target is the Player's position

        // Continue moving while the GameObject hasn't reached the player
        while (Mathf.Abs(transform.position.x - targetPosition.x) > 0.01f) // Only check x-axis distance
        {
            // Update the target position to the current position of the Player on x-axis
            targetPosition = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);

            // Smoothly move this GameObject towards the Player's position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Wait for the next frame before continuing the movement
            yield return null;
        }

        // Ensure the GameObject reaches the exact target position (Player's center on x-axis)
        transform.position = targetPosition;

        yield return new WaitForSeconds(0.1f);

        // Destroy this GameObject (optional, if needed after movement)
        
        ScaleUpAndDown(GameObject.Find("Player"));
        Destroy(this.gameObject);
    }
}
