# Ejercicio 3

## 1. Se deben incluir varios cilindros sobre la escena. Cada vez que el objeto jugador colisione con alguno de ellos, deben aumentar su tamaño y el jugador aumentar puntuación.

![2021-10-28 17-19-08](https://user-images.githubusercontent.com/72868069/139296065-b8e71e3c-701d-48a3-8e9d-59bc148a5fab.gif)

```c#
public class ControllerAndScore : MonoBehaviour
{
    int score = 0;
    private Rigidbody rb;
    public float speed = 10f;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }
 
    void FixedUpdate()
    {   
        Vector3 m_input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 translation = m_input * speed * Time.fixedDeltaTime;
        Vector3 translateVector = translation;
        rb.MovePosition(rb.position + translateVector);
    }

    void OnCollisionEnter(Collision col)
    {   
        if (col.gameObject.tag == "Cilindro") score++;
    }
}
```

```c#
public class CylinderColision : MonoBehaviour
{
    Vector3 scaleChange = new Vector3(1,1,1);
 
    void OnCollisionEnter(Collision col)
    {   
        if (col.gameObject.name == "Player") transform.localScale += scaleChange;
    }
}
```

## 2. Agregar cilindros de tipo A, en los que además, si el jugador pulsa la barra espaciadora lo mueve hacia fuera de él.

![2021-10-28 18-15-27](https://user-images.githubusercontent.com/72868069/139303816-b0606652-a304-44c0-8d69-c918916f128a.gif)

```c#
public class CylinderA : MonoBehaviour
{
    private Rigidbody rb;
    private Rigidbody player_rb;
    private bool repel;

    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        player_rb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    void Update() 
    {
        if ( Input.GetKeyDown( KeyCode.Space ) )
        {
            repel = true;
        }
    }
    
    void FixedUpdate ()
    {
        if ( repel )
        {
            Vector3 direction = transform.position - player_rb.transform.position;  
            rb.AddForce(direction * 20 * Time.fixedTime);
            repel = false;
        }
    }
}
```

## 3. Se deben incluir cilindros que se alejen del jugador cuando esté próximo.

![2021-10-28 18-25-27](https://user-images.githubusercontent.com/72868069/139305240-1ddf314c-1b11-47e6-9b36-ae898acf18ad.gif)

```c#
public class CylinderB : MonoBehaviour
{
    private Rigidbody rb;
    private Rigidbody player_rb;

    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        player_rb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        Vector3 direction = transform.position - player_rb.transform.position;
        float distance = Vector3.Distance (transform.position, player_rb.transform.position);
        if (distance < 4) {
            rb.AddForce(direction * 20 * Time.fixedTime);
        }
        
    }
}
```

## 4. Ubicar un tercer objeto que sea capaz de detectar colisiones y que se mueva con las teclas: I, L, J, M

![2021-10-28 19-34-40](https://user-images.githubusercontent.com/72868069/139315921-c8a1c36b-709f-4243-b0fb-458d0708b8aa.gif)

```c#
public class CharacterController2 : MonoBehaviour
{  
    private Rigidbody rb;
    public float speed = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 m_input = new Vector3(Input.GetAxis("Horizontal2"), 0, Input.GetAxis("Vertical2"));
        Vector3 translation = m_input * speed * Time.fixedDeltaTime;
        Vector3 translateVector = translation;
        rb.MovePosition(rb.position + translateVector);
    }

    void OnCollisionEnter(Collision col)
    {   
        Debug.Log("me has dao " + col.gameObject.name);
    }
}
```

## 5. Debes ubicar cubos que aumentan de tamaño cuando se le acerca una esfera y que disminuye cuando se le acerca el jugador.

![2021-10-28 20-19-15](https://user-images.githubusercontent.com/72868069/139321621-e6192798-e2f5-4b19-bc94-feeb393539dc.gif)

```c#
public class CubeModifier : MonoBehaviour
{
    private Rigidbody rb;
    private Rigidbody sphere_rb;
    private Rigidbody player_rb;
  
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sphere_rb = GameObject.Find("Ball").GetComponent<Rigidbody>();
        player_rb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float distanceSphere = Vector3.Distance (transform.position, sphere_rb.transform.position);
        float distancePlayer = Vector3.Distance (transform.position, player_rb.transform.position);
        if (distanceSphere < 5) {
            transform.localScale *= 1.1f;
        }
        if (distancePlayer < 5) {
            transform.localScale *= .9f;
        }
    }
}
```
