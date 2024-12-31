using DG.Tweening;
using UnityEngine;

public class FloatingTxt : MonoBehaviour
{
    [SerializeField] float floatingDuration;
    [SerializeField] float floatArea;
    [SerializeField] float floatingSpeed;
    [SerializeField] float destroyTime;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition = new Vector3(0, 4f, 0);
        StartFloating();

    }


    void StartFloating()
    {
        
        transform.DOLocalMoveY(floatArea, floatingDuration / 2) 
            .SetEase(Ease.InOutSine) 
            .SetRelative(true);

        transform.DOScale(Vector2.zero, floatingDuration - 1);
    }
}
