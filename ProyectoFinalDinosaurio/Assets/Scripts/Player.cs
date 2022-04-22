using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Player : MonoBehaviour
{

    public int contador = 0;
    public int countLevel = 6;
    public int countTime1 = 0;
    public int countTime2 = 0;
    public bool swt = true;
    public Text score;
    public Text level;
    public Text time;


    Rigidbody2D rb;
    Animator anim;
    public float Jump;
    public bool isJumping = false;
    public GameObject RestartBO;
    public GameObject ViewPanel;
    public GameObject MenuPanel;
    public GameObject BtnPause;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        countTime1++;
        score.text = "Score: " + contador.ToString();
        
        print("Contadir " + contador);
        if (Input.GetKeyDown(KeyCode.Mouse0)&&isJumping==true)
        {
            rb.AddForce(Vector2.up*Jump);
            isJumping = false;
            anim.SetBool("IsJumping",true);
            contador = contador + 1;
            print("Contadir " + contador);
           
            if (contador % 3 == 0)
            {
                countLevel = countLevel + 1;
                level.text = "Level : " + countLevel.ToString();
                print("Subio de nivel " + countLevel);
               
            }           
        }

        if (swt == true)
        {
            countTime1++;

            if (countTime1 % 10 == 0)
            {
                countTime2++;
                time.text = "Time : " + countTime2.ToString();
            }
        }
        


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Ground"&&isJumping==false)
        {
             isJumping = true;
            anim.SetBool("IsJumping", false);
            
        }
        if (collision.gameObject.tag=="Enemy")
        {
            anim.SetBool("Dead",true);
            Time.timeScale=0;
            RestartBO.SetActive(true);
            swt = false;

        }
        

    }
    public void Restart()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1;
        print("Terminó partida!!!!");
        contador = 0;
        countLevel = 1;
        countTime1 = 0;
        MenuPanel.SetActive(false);
        swt = true;
    }

    public void Resume()
    {
        swt = true;
        Time.timeScale = 1f;
        MenuPanel.SetActive(false);
        

    }

    public void Exit()
    {
        print("Salir del juego");
        Application.Quit();
        ViewPanel.SetActive(true);
        MenuPanel.SetActive(false);
        swt = false;
    }

    public void showMenu()
    {
        Time.timeScale = 0f;
        print("Se nyestra el Menu");       
        MenuPanel.SetActive(true);
        swt = false;
    }
}
