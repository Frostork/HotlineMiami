using UnityEngine;

public class Cursor : MonoBehaviour
{
    public float speed = 10.0f;

    private void Update()
    {
        Vector3 mousePosScreen = Input.mousePosition;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
        transform.position = Vector3.Lerp(transform.position, mousePosWorld, speed * Time.deltaTime);
    }
}