using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Controles : MonoBehaviour
{
    Rigidbody2D Cuerpo;
    public float velocidad = 0f;
    Vector2 direction;
    [SerializeField]
    TextMeshProUGUI labelGasofa;
    [SerializeField]
    TextMeshProUGUI labelNoGasofa;
    [SerializeField]
    TextMeshProUGUI labelVida;
    [SerializeField]
    TextMeshProUGUI labelPersonas;
    [SerializeField]
    GameObject TextoPersonas;
    [SerializeField]
    GameObject TextoGasolina;
    [SerializeField]
    GameObject TextoFin;
    [SerializeField]
    GameObject TextoVida;
    [SerializeField]
    GameObject TextoGanaste;
    [SerializeField]
    GameObject Boton;
    [SerializeField]
    GameObject Particulas;
    [SerializeField]
    GameObject Meteorito;
    [SerializeField]
    GameObject Personas;
    
    float personas = 0f;
    public float vida = 100f;
    float gasofa = 100f;
    //Ponemos los límites de la gasolina para que no sobrepase los 100 y la dejamos editable para un posible nivel con dificultad añadida.
    public float LMax = 100;
    public float LMin = 0;
    //Crear una variable tipo float gasolinactual
    //Inicialmente tendrá 100

    //Update se va a ir gastando
    //Time.deltaTime

    void Start()
    {
        //Desactivamos el texto de "No Queda Combustible" para que no se fusione con el de la cantidad de combustible que hay
        TextoFin.gameObject.SetActive(false);
        TextoGanaste.gameObject.SetActive(false);
        Boton.gameObject.SetActive(false);
        Cuerpo = GetComponent<Rigidbody2D>();


    }

    void Update()
    {
        //Creamos los controles de la nave
        direction.x = Input.GetAxis("Horizontal") * velocidad * Time.deltaTime;
        direction.y = Input.GetAxis("Vertical") * velocidad * Time.deltaTime;
        //Le damos un desgaste con el tiempo a la gasolina
        gasofa = gasofa - 5f * Time.deltaTime;
        //".ToString("??") sirve para delimitar la cantidad de dígitos que aparecen. Se modifica poniendo 0`s
        labelGasofa.text = gasofa.ToString("00.00") + "%";
        labelVida.text = "Tienes " + vida + " de vida";
        labelPersonas.text = "Personas salvadas: " + personas + "/5";
        gasofa = Mathf.Clamp(gasofa, LMin, LMax);
        if (personas == 5)
        {
            GetComponent<Controles>().enabled = false;
            TextoGanaste.gameObject.SetActive(true);
            TextoVida.gameObject.SetActive(false);
            Boton.gameObject.SetActive(true);
            TextoFin.gameObject.SetActive(false);
            TextoPersonas.gameObject.SetActive(false);
            TextoGasolina.gameObject.SetActive(false);
        }
        if (vida == 0f)
        {
            GetComponent<Controles>().enabled = false;
            TextoPersonas.gameObject.SetActive(false);
            TextoFin.gameObject.SetActive(true);
            TextoVida.gameObject.SetActive(false);
            Boton.gameObject.SetActive(true);
            TextoGasolina.gameObject.SetActive(false);
        }
        if (gasofa == 0f)
        {
            GetComponent<Controles>().enabled = false;
            TextoPersonas.gameObject.SetActive(false);
            TextoVida.gameObject.SetActive(false);
            TextoGasolina.gameObject.SetActive(false);
            TextoFin.gameObject.SetActive(true);
            Boton.gameObject.SetActive(true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Meteorito")
        {
            vida = vida - 20f;
            collision.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Personas")
        {
            other.gameObject.SetActive(false);
            personas = personas + 1;
        }
        if (other.tag == "Gasofa")
        {
           
            gasofa = gasofa + 20f;
            other.GetComponent<AudioSource>().Play();
            other.enabled = false;
            Destroy(other.gameObject, 0.5f);
            //Crear Particulas
            
            Instantiate(Particulas, other.transform.position, other.transform.rotation);
        }
    }
    private void FixedUpdate()
    {
        Cuerpo.AddForce(transform.right * direction.x, ForceMode2D.Impulse);
        Cuerpo.AddForce(transform.up * direction.y, ForceMode2D.Impulse);
    }
    //Crear un objeto que al colisionar con el sucedan dos cosas
    //Añadir combustible
    //Destruir el objeto recogido -> destroy // SetActive(false)
    public void ClickEnBoton()
    {
        SceneManager.LoadScene("NV1");
    }
}
