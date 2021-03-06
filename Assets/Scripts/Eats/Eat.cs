﻿using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 捕食者にアタッチするスクリプト
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Eat : MonoBehaviour
{
    [SerializeField]
    float Ratio;
    [SerializeField]
    Ease ease;
    [SerializeField]
    float Time;

    private Transform m_Trans;
    private SpriteRenderer m_Sprite;
    private float m_Size => m_Sprite.bounds.size.x * m_Sprite.bounds.size.y;
    void Start()
    {
        m_Trans = transform;
        m_Sprite = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryEat(collision);
    }
    /// <summary>
    /// 当たったオブジェクトを食べようとするメソッド
    /// </summary>
    /// <param name="collision"></param>
    public void TryEat(Collision2D collision)
    {
        var hitObj = collision.gameObject.GetComponent<IEatable>();
        if (hitObj == null) return;
        if (m_Size < hitObj.ObjSize) return;

        var hitObjSize = hitObj.ObjSize;

        var playerScale = m_Trans.localScale;
        playerScale += Vector3.one * (hitObjSize / Ratio);

        m_Trans.DOScale(playerScale, Time).SetEase(ease);

        Destroy(collision.gameObject);
    }
}
