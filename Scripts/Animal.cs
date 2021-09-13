using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{


    bool Power = true;

    [SerializeField] float speed;
    [SerializeField] bool movingRight;
    [SerializeField] int puntosDeVida;
    [SerializeField] GameManager gm;

    float minX, maxX;


    int Contador = 0;
    int RespaldoC;
    int RpuntosV;
    float time = 0;
    float LimitTime = 5;


    // Start is called before the first frame update
    void Start()
    {
        Vector2 esquinaInfDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
        Vector2 esquinaInfIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        maxX = esquinaInfDer.x;
        minX = esquinaInfIzq.x;

        RespaldoC = Contador;

        RpuntosV = puntosDeVida;



    }

    // Update is called once per frame
    void Update()
    {

        if (Power == true)

            PowerUp();




        if (movingRight)
        {
            Vector2 movimiento = new Vector2(speed * Time.deltaTime, 0);
            transform.Translate(movimiento);
        }
        else
        {
            Vector2 movimiento = new Vector2(-speed * Time.deltaTime, 0);
            transform.Translate(movimiento);
        }
        

        if(transform.position.x >= maxX)
        {
            movingRight = false;
        }
        else if(transform.position.x <= minX)
        {
            movingRight = true;
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if(collision.gameObject.CompareTag("Disparo") )
        {   
            int puntos = collision.gameObject.GetComponent<Bullet>().Dano();
            puntosDeVida = puntosDeVida - puntos;

            if(puntosDeVida <= 0)
            {
                gm.ReducirNumEnemigos();
                Destroy(this.gameObject);
            }
       
        }
    }


    void PowerUp()
    {

        if (Input.GetKeyDown(KeyCode.E) && Time.unscaledTime >= time) 
        {
            puntosDeVida = 1;
            Time.timeScale = 0.5f;

            time = Time.unscaledTime + LimitTime;

            RespaldoC++;
        }

        if(time <= Time.unscaledTime )
        {
            Time.timeScale = 1f;
            puntosDeVida = RpuntosV;

            if(RespaldoC == 3 )
            {
                Power = false;
            }
        }


    }

}