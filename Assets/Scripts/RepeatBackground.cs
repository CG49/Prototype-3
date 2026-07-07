using UnityEditor.PackageManager.UI;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RepeatBackground : MonoBehaviour
{
    private float repeatWidth;
    private Vector3 startPos;

    void Awake()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    void OnEnable()
    {
        PlayerController.OnGameOver += StopBackground;
    }

    void OnDisable()
    {
        PlayerController.OnGameOver -= StopBackground;
    }

    void Update()
    {
        if (transform.position.x < (startPos.x - repeatWidth))
        {
            transform.position = startPos;
        }
    }

    private void StopBackground()
    {
        enabled = false;
    }
}
