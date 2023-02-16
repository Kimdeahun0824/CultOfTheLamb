using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseBullet : MonoBehaviour
{
    public Sprite m_BulletSprite;
    public float m_Speed;
    public float m_Damage;

    private Rigidbody m_Rigidbody = default;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        gameObject.SetImageSprite(m_BulletSprite);
    }

    private void OnEnable()
    {

    }

    private void Update()
    {

    }

}
