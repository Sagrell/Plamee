using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("References:")]
    public GameObject blow;
    public SpriteRenderer sprite;

    [Space]
    [Header("Jump Settings:")]
    [Range(0, 20f)]
    public float height;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
  

    //Private
    Rigidbody2D rb;
    Vector2 gravity;
    Vector2 currVelocity;
    bool onGround;
    bool isAlive;
    Character character;
    int currentColor;


    void Start () {
        //Инициализация переменных
        rb = GetComponent<Rigidbody2D>();
        onGround = true;
        currVelocity = Vector2.zero;
        gravity = Physics2D.gravity;
        isAlive = true;
        character = CharacterManager.currentCharacter;
        currentColor = 0;
        //Изменяется персонаж
        sprite.sprite = character.sprite;
        sprite.color = character.colors[currentColor];
        //Подписывается на событие смерти персонажа
        EventManager.StartListening("Die", Die);
    }

    private void FixedUpdate()
    {
        //Определяет находится ли игрок на земле
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    void Die()
    {
        //Активируется анимация смерти и уничтожается через 1 сек
        GameObject blowObj = Instantiate(blow,transform.position,transform.rotation,transform.parent);
        ParticleSystem.MainModule module = blowObj.GetComponent<ParticleSystem>().main;
        module.startColor = sprite.color;
        Destroy(blowObj, 1f);
        //Уничтожаем персонажа
        isAlive = false;
        Destroy(gameObject);
    }

    void Update () {
        //Текущая скорость
        currVelocity = rb.velocity;
        currVelocity.x = 0;

#if UNITY_ANDROID && !UNITY_EDITOR
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
#else
        if (Input.GetKeyDown("space"))
#endif
        {
            if (onGround)
            {
                //Задаем начальную скорость по y
                currVelocity.y = height;
                //Изменяем цвет
                currentColor = (int)((currentColor + 1) % character.verticesCount);
                sprite.color = character.colors[currentColor];
            }
        }
        rb.velocity = currVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Если персонаж наткнулся на врага
        if(collision.CompareTag("Trap"))
        {
            //Активирует ивент смерти
            EventManager.TriggerEvent("Die");
        }
    }
}
