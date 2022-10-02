using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PisaBulletManager : MonoBehaviour
{
    [SerializeField]
    private Transform _bossObjectTrm = null;
    [SerializeField]
    private List<PisaBulletTypeData> bulletTypeDatas = new List<PisaBulletTypeData>();

    private PisaBulletTypeData GetBulletTypeData(PisaBulletType bulletType)
    {
        return bulletTypeDatas[(int)bulletType];
    }

    public void SpawnBulletByCircle(PisaBulletType bulletType, Vector3 pos, int count, float speed, float vol = 1f)
    {
        PisaBulletTypeData target = GetBulletTypeData(bulletType);
        CameraManager.instance.CameraShake(10f, 4f, 0.2f);
        AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        au.Play(target.bulletSound, vol);
        for (int i = 0; i <= count; i++)
        {
            Barrage s = PoolManager.Instance.Pop("Barrage") as Barrage;
            s.transform.SetParent(_bossObjectTrm);
            Quaternion rot = Quaternion.AngleAxis(i * 10f, Vector3.forward);
            s.transform.SetPositionAndRotation(pos, rot);
            s.SetBarrage(speed, target.size, target.offset, target.bulletSprite);
            s.transform.localScale = target.localScale;
        }
    }

    public void SpawnBulletLookTarget(PisaBulletType bulletType, Transform targetTrm, Vector3 pos, int count, float delay, float speed, float vol = 1f)
    {
        PisaBulletTypeData target = GetBulletTypeData(bulletType);
        StartCoroutine(LookTargetBulletCoroutine(target, targetTrm, pos, count, delay, speed, vol));
    }

    public void SpawnBulletLookTarget(PisaBulletType bulletType, Transform targetTrm, Transform startTrm, int count, float delay, float speed, float vol = 1f)
    {
        PisaBulletTypeData target = GetBulletTypeData(bulletType);
        StartCoroutine(LookTargetBulletCoroutineByTransform(target, targetTrm, startTrm, count, delay, speed, vol));
    }

    private IEnumerator LookTargetBulletCoroutine(PisaBulletTypeData target, Transform targetTrm, Vector3 pos, int count, float delay, float speed, float vol = 1f)
    {
        for (int i = 0; i <= count; i++)
        {
            CameraManager.instance.CameraShake(5f, 4f, 0.2f);
            AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
            au.Play(target.bulletSound, vol);

            Barrage s = PoolManager.Instance.Pop("Barrage") as Barrage;
            s.transform.SetParent(_bossObjectTrm);

            Vector3 dir = (targetTrm.position - pos);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            s.transform.SetPositionAndRotation(pos, rot);
            s.SetBarrage(speed, target.size, target.offset, target.bulletSprite);
            s.transform.localScale = target.localScale;

            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator LookTargetBulletCoroutineByTransform(PisaBulletTypeData target, Transform targetTrm, Transform startTrm, int count, float delay, float speed, float vol = 1f)
    {
        for (int i = 0; i <= count; i++)
        {
            CameraManager.instance.CameraShake(5f, 4f, 0.2f);
            AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
            au.Play(target.bulletSound, vol);

            Barrage s = PoolManager.Instance.Pop("Barrage") as Barrage;
            s.transform.SetParent(_bossObjectTrm);

            Vector3 dir = (targetTrm.position - startTrm.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            s.transform.SetPositionAndRotation(startTrm.position, rot);
            s.SetBarrage(speed, target.size, target.offset, target.bulletSprite);
            s.transform.localScale = target.localScale;

            yield return new WaitForSeconds(delay);
        }
    }

    public void SpawnBulletLookTargetBetween(PisaBulletType bulletType, Vector3 targetposition, Vector3 startPos, float angle, int count, float delay, float speed, float vol = 1f, bool isNotAngle = false)
    {
        PisaBulletTypeData target = GetBulletTypeData(bulletType);
        StartCoroutine(SpawnBulletLookTargetBetweenCoroutine(target, targetposition, startPos, angle, count, delay, speed, vol, isNotAngle));
    }

    private IEnumerator SpawnBulletLookTargetBetweenCoroutine(PisaBulletTypeData target, Vector3 targetposition, Vector3 startPos, float angle, int count, float delay, float speed, float vol = 1f, bool isNotAngle = false)
    {
        float plusAngle = 0f;
        for (int i = 0; i <= count; i++)
        {
            CameraManager.instance.CameraShake(2f, 4f, 0.1f);
            AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
            au.Play(target.bulletSound, vol);

            Barrage leftBarrage = PoolManager.Instance.Pop("Barrage") as Barrage;
            leftBarrage.transform.SetParent(_bossObjectTrm);
            Barrage rightBarrage = PoolManager.Instance.Pop("Barrage") as Barrage;
            rightBarrage.transform.SetParent(_bossObjectTrm);

            Vector3 dir = (targetposition - startPos);
            float distanceAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion leftRot = Quaternion.AngleAxis(distanceAngle - 90f - angle / 2 + plusAngle, Vector3.forward);
            Quaternion rightRot = Quaternion.AngleAxis(distanceAngle - 90f + angle / 2 + plusAngle, Vector3.forward);

            leftBarrage.transform.SetPositionAndRotation(startPos, leftRot);
            leftBarrage.SetBarrage(speed, target.size, target.offset, target.bulletSprite);
            leftBarrage.transform.localScale = target.localScale;

            rightBarrage.transform.SetPositionAndRotation(startPos, rightRot);
            rightBarrage.SetBarrage(speed, target.size, target.offset, target.bulletSprite);
            rightBarrage.transform.localScale = target.localScale;

            if(isNotAngle == false)
            {
                if (i < count / 2)
                    plusAngle -= 0.5f;
                else
                    plusAngle += 0.5f;
            }
            yield return new WaitForSeconds(delay);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}

[System.Serializable]
public struct PisaBulletTypeData
{
    public PisaBulletType bulletType;
    public Sprite bulletSprite;
    public Vector3 localScale;
    public Vector2 size;
    public Vector2 offset;
    public AudioClip bulletSound;
}

[System.Serializable]
public enum PisaBulletType
{
    BIG,
    SMALL,
    MIDDLE
}
