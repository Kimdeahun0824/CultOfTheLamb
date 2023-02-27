using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [Header("Follow Player Position")]
    public Transform player;
    public float cameraMoveSpeed;
    public Vector3 offset;

    private void Start()
    {
        offset = new Vector3(0f, 20f, -20f);

    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, cameraMoveSpeed * Time.deltaTime);

    }
}
