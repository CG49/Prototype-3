using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float leftBound = -57f;

    private void OnEnable()
    {
        PlayerController.OnGameOver += StopMoving;
    }

    private void OnDisable()
    {
        PlayerController.OnGameOver -= StopMoving;
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.left, Space.World);

        if (gameObject.CompareTag("Obstacle") && transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }

    private void StopMoving()
    {
        enabled = false;
    }
}
