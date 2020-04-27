using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaPersonagem : MonoBehaviour
{
    public CharacterController controle;
    public float velocidade = 6f;
    public float alturaPulo = 3f;
    public float gravidade = -20f;

    public Transform checkChao;
    public float raioEsfera= 0.4f;
    public LayerMask chaoMask;
    public bool estaNoChao;
    
    private Vector3 velocidadeAoCair;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializar o controler
        controle = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se esta no chao
        estaNoChao = Physics.CheckSphere(checkChao.position, raioEsfera, chaoMask);

        move();
        jump();

    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(checkChao.position, raioEsfera);
    }

    void move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        controle.Move(move * velocidade * Time.deltaTime);
    }

    void jump() {
        // Pulo
        if(Input.GetButtonDown("Jump") && estaNoChao) {
            velocidadeAoCair.y = Mathf.Sqrt(alturaPulo * -2f * gravidade);
        }

        if(estaNoChao && velocidadeAoCair.y < 0) {
            velocidadeAoCair.y = -2f;
        }

        velocidadeAoCair.y += gravidade * Time.deltaTime;
        controle.Move(velocidadeAoCair * Time.deltaTime);
    }
}
