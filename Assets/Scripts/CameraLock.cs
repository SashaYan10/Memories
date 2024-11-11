using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    // ���� ��� ��'����, �� ���� ���� �������� ������
    [SerializeField]
    private Transform targetPlayer; // ����� �� ������ ������ ��'��� � ���������

    [SerializeField]
    private float leftLimit;
    [SerializeField]
    private float rightLimit;
    [SerializeField]
    private float bottomLimit;
    [SerializeField]
    private float upperLimit;

    void Start()
    {
        // ���� targetPlayer �� ���������� � ���������, �������� ��'��� �� ����� "Player"
        if (targetPlayer == null)
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void LateUpdate()
    {
        // ����������, �� targetPlayer ����������
        if (targetPlayer != null)
        {
            Vector3 temp = transform.position;
            temp.x = targetPlayer.position.x;
            temp.y = targetPlayer.position.y;

            transform.position = temp;

            // �������� ������� ������
            transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                Mathf.Clamp(transform.position.y, bottomLimit, upperLimit),
                transform.position.z
            );
        }
    }

    // ����� ��� ���� ��'����, �� ���� ���� ������
    public void ChangeTarget(Transform newTarget)
    {
        targetPlayer = newTarget;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, upperLimit), new Vector2(rightLimit, upperLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(rightLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, upperLimit), new Vector2(leftLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, upperLimit), new Vector2(rightLimit, bottomLimit));
    }
}
