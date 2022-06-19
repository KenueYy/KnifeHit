using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Log : MonoBehaviour, ILog
{
    public Action OnHit;

    [Range(0, 50)][SerializeField] private float difficult;
    [SerializeField] private float maxDifficult;
    [SerializeField] private float rotationSpeed;
    private int maxHits;
    private float angle;

    private LogAudio logAudio;
    void Start()
    {
        logAudio = GetComponent<LogAudio>();
    }
    private void OnEnable()
    {
        StartCoroutine(ChangeRotationAngle());
        OnHit += Hit;
    }
    private void OnDisable()
    {
        StopCoroutine(ChangeRotationAngle());
        OnHit -= Hit;
    }
    void Update()
    {
        transform.Rotate(new Vector3(0,0,angle) * Time.deltaTime);
    }
    public void StartGame()
    {
        angle = rotationSpeed;
    }
    public void StopRotation()
    {
        angle = 0;
    }
    public void SetMaxHits(int value)
    {
        maxHits = value;
        GameManager.instance.OnKnifeChange?.Invoke(maxHits);
    }
    private void Hit()
    {
        logAudio.PlayHitSound();
        maxHits--;
        GameManager.instance.OnKnifeChange?.Invoke(maxHits);
        if (maxHits <= 0)
            GameManager.instance.OnLevelChange?.Invoke();
    }
    private IEnumerator ChangeRotationAngle()
    {
        while (true)
        {
            if(angle < maxDifficult && angle != 0)
                angle += difficult;
            yield return new WaitForSeconds(1);
        }
    }
}
