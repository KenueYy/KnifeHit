using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Knife : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 direction;
    void Update()
    {
        transform.Translate(speed * direction * Time.deltaTime);
    }
    private void OnEnable()
    {
        Throw();
    }
    private void OnDisable()
    {
        direction = Vector2.zero;
    }
    public void Throw()
    {
        direction = Vector2.up;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent(out Log log))
        {
            FixedKnife(log.gameObject);
            log.OnHit?.Invoke();
            GameManager.instance.OnCoinAdd?.Invoke(Property.instance.COINS_PER_HIT_LOG);
        }
        if (collision.collider.TryGetComponent(out Apple apple))
        {
            GameManager.instance.OnCoinAdd?.Invoke(Property.instance.COINS_PER_HIT_APPLE);
        }
        if(collision.collider.tag == "Handle")
        {
            GameManager.instance.OnRestartGame?.Invoke();
        }
    }
    private void FixedKnife(GameObject gameObject)
    {
        transform.parent = gameObject.transform;
        direction = Vector2.zero;
    }
}
