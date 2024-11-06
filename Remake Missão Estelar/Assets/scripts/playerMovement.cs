using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class playerMovement : MonoBehaviour
{

    public float throttleIncrement = 0.02f;
    public float maxThrust = 550f;
    public float responsiveness = 75f;
    public float boostedMaxThrust = 1000f;
    public float throttleDecayRate = 10f;

    [SerializeField] public AudioSource audio;
    public AudioClip sTurbo;
    
    public AudioClip cTurbo;


    private float throttle;
    private float roll;
    private float pitch;
    private float yaw;

    private bool podeMover = true;


    public GameObject fire;
    public GameObject turbo;

    public GameObject sol;
    private bool colisaoSol = false;
    public GameObject lua;
    private bool colisaoLua = false;
    public GameObject mercurio;
    private bool colisaoMercurio = false;
    public GameObject venus;
    private bool colisaoVenus = false;
    public GameObject terra;
    private bool colisaoTerra = false;
    public GameObject marte;
    private bool colisaoMarte = false;
    public GameObject jupiter;
    private bool colisaoJupiter = false;
    public GameObject saturno;
    private bool colisaoSaturno = false;
    public GameObject urano;
    private bool colisaoUrano = false;
    public GameObject netuno;
    private bool colisaoNetuno = false;


    public int contadorPlaneta = 0;
    public int contadorSatelite = 0;
    public int contadorEstrela = 0;

    public TextMeshProUGUI contadorTp;
    public TextMeshProUGUI contadorTe;
    public TextMeshProUGUI contadorTs;


   

    Rigidbody rb;
    [SerializeField] TextMeshProUGUI hud;

    TextAsset textAsset;
    private float responseModifier
    {
        get
        {
            return (rb.mass / 5) * responsiveness;
        }
    }

    private void HandleInputs()
    {
        // valores de rotação
        roll = Input.GetAxisRaw("Roll");
        yaw = Input.GetAxisRaw("Pitch");
        pitch = Input.GetAxisRaw("Yaw");

        // Handle throttle valores, 0 to 100
        if (Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.T))
            {
                throttleIncrement = 0.05f;
                throttle += throttleIncrement;
                maxThrust = 600f;
                fire.SetActive(true);
                turbo.SetActive(true);
                throttle = Mathf.Clamp(throttle, 0, 170f);

                audio.clip = cTurbo;
                audio.enabled = true;
            } else
            {

                throttleIncrement = 0.04f;
                throttle += throttleIncrement;
                fire.SetActive(true);
                turbo.SetActive(false);
                maxThrust = 550f;
                throttle = Mathf.Clamp(throttle, 0, 100f);
                audio.clip = sTurbo;
                audio.enabled = true;
            }


            
        }
        else if (Input.GetKey(KeyCode.T))
            {
                throttleIncrement = 0.05f;
                throttle += throttleIncrement;
                maxThrust = 600f;
                fire.SetActive(true);
                turbo.SetActive(true);
                throttle = Mathf.Clamp(throttle, 0, 170f);

                audio.clip = cTurbo;
                audio.enabled = true;

            if (Input.GetKey(KeyCode.Space))
            {
                throttleIncrement = 0.05f;
                throttle += throttleIncrement;
                maxThrust = 600f;
                fire.SetActive(true);
                turbo.SetActive(false);
                throttle = Mathf.Clamp(throttle, 0, 170f);
                audio.clip = cTurbo;
                audio.enabled = true;
            }
        }       
        else
        {
            throttleIncrement = 0.02f;
            maxThrust = 550f;
            throttle = throttleDecayRate * Time.deltaTime;
            fire.SetActive(false);
            turbo.SetActive(false);
            throttle = Mathf.Clamp(throttle, 0, 100f);
            audio.enabled = false;
        }

        
    }

    
    void Start()
    {
        textAsset = Resources.Load<TextAsset>("Perguntas");
        Debug.Log(textAsset);
        turbo.SetActive(false);
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

        
            // Sync the frame rate to the screen's refresh rate
            
        
    }

    // Update is called once per frame
    void Update()
    {
        

        HandleInputs();
        updateHud();
        
        
    }
    

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * maxThrust * throttle);
        rb.AddTorque(transform.up * yaw * responseModifier);
        rb.AddTorque(transform.right * pitch * responseModifier);
        rb.AddTorque(transform.forward * roll * responseModifier);
    }

    

    private void updateHud()
    {
        hud.text = "" + throttle.ToString("F0") + "%\n";
        //hud.text += "" + (rb.velocity.magnitude * 3.6f).ToString("F0") + "km/h\n";

    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name == "sol" && !colisaoSol )
        {
            
            contadorEstrela++;

            colisaoSol = true;

            contadorTe.text = "Estrela: " + contadorEstrela;
        }

        if (collision.gameObject.name == "lua" && !colisaoLua)
        {

            contadorSatelite++;

            colisaoLua = true;

            contadorTs.text = "Satélite: " + contadorSatelite;
        }

        if (collision.gameObject.name == "mercurio" && !colisaoMercurio)
        {

            contadorPlaneta++;

            colisaoMercurio = true;

            contadorTp.text = "Planeta(s): " + contadorPlaneta;
        }


        if (collision.gameObject.name == "venus" && !colisaoVenus)
        {

            contadorPlaneta++;

            colisaoVenus = true;
            contadorTp.text = "Planeta(s): " + contadorPlaneta;
        }


        if (collision.gameObject.name == "terra" && !colisaoTerra)
        {

            contadorPlaneta++;

            colisaoTerra = true;
            contadorTp.text = "Planeta(s): " + contadorPlaneta;
        }


        if (collision.gameObject.name == "marte" && !colisaoMarte)
        {

            contadorPlaneta++;

            colisaoMarte = true;
            contadorTp.text = "Planeta(s): " + contadorPlaneta;
        }



        if (collision.gameObject.name == "jupiter" && !colisaoJupiter)
        {

            contadorPlaneta++;

            colisaoJupiter = true;
            contadorTp.text = "Planeta(s): " + contadorPlaneta;
        }



        if (collision.gameObject.name == "saturno" && !colisaoSaturno)
        {

            contadorPlaneta++;

            colisaoSaturno = true;
            contadorTp.text = "Planeta(s): " + contadorPlaneta;
        }


        if (collision.gameObject.name == "urano" && !colisaoUrano)
        {

            contadorPlaneta++;

            colisaoUrano = true;
            contadorTp.text = "Planeta(s): " + contadorPlaneta;
        }



        if (collision.gameObject.name == "netuno" && !colisaoNetuno)
        {

            contadorPlaneta++;

            colisaoNetuno = true;
            contadorTp.text = "Planeta(s): " + contadorPlaneta;
        }

    }


}
