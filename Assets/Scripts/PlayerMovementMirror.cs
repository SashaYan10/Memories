using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovementMirror : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed = 5f;
    [SerializeField] private float _verticalSpeed = 3f;

    private ActionObject obj;
    private Vector2 _movement;

    private Rigidbody2D _rb;
    private Animator _animator;

    private Transform _playerTransform;
    private float _previousPlayerX;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";

    public string TagPlayer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        // Знаходимо об'єкт з тегом "Player"
        GameObject player = GameObject.FindGameObjectWithTag(TagPlayer);
        if (player != null)
        {
            _playerTransform = player.transform;
            _previousPlayerX = _playerTransform.position.x;
        }
    }

    private void Update()
    {
        if (!inDialogue())
        {
            // Перевіряємо, чи змінилася позиція по X у основного персонажа
            bool isXChanged = !Mathf.Approximately(_playerTransform.position.x, _previousPlayerX);

            if (!isXChanged)
            {
                // Якщо позиція по X у основного персонажа не змінилася,
                // відзеркалений персонаж не рухається по осі X
                _movement.Set(0, -InputManager.Movement.y * _verticalSpeed);

                // Якщо натиснуті клавіші вліво або вправо, запускаємо анімацію руху
                if (InputManager.Movement.x != 0)
                {
                    _animator.SetFloat(_horizontal, InputManager.Movement.x);
                }
                else
                {
                    _animator.SetFloat(_horizontal, 0); // Зупинка анімації руху
                }
            }
            else
            {
                // Якщо основний персонаж рухається по осі X, відзеркалений персонаж теж рухається
                _movement.Set(InputManager.Movement.x * _horizontalSpeed, -InputManager.Movement.y * _verticalSpeed);

                // Оновлюємо анімацію руху
                _animator.SetFloat(_horizontal, _movement.x);
            }

            // Оновлюємо попередню позицію по X
            _previousPlayerX = _playerTransform.position.x;

            // Оновлюємо анімацію для вертикального руху
            _animator.SetFloat(_vertical, _movement.y / _verticalSpeed);

            if (_movement != Vector2.zero)
            {
                _animator.SetFloat(_lastHorizontal, _movement.x);
                _animator.SetFloat(_lastVertical, _movement.y / _verticalSpeed);
            }

            // Застосовуємо рух персонажа
            _rb.velocity = _movement;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    private bool inDialogue()
    {
        return DialogueSystem.DialogueHolder.IsDialogueActive;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ActObj")
        {
            obj = collision.gameObject.GetComponent<ActionObject>();

            if (Input.GetKey(KeyCode.Return))
                obj.ActivateDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        obj = null;
    }
}
