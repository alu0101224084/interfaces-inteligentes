## 1. Cuando el jugador colisiona con un objeto de tipo B, el objeto A mostrará un texto en una UI de Unity. Cuando toca el objeto A se incrementará la fuerza del objeto B.

![2021-11-06 19-54-52](https://user-images.githubusercontent.com/72868069/140622244-b25478d4-50fd-437d-9004-546f076792c4.gif)

## 2. Cuando el jugador se aproxima a los cilindros de tipo A, los cilindros de tipo B cambian su color y las esferas se orientan hacia un objetivo ubicado en la escena con ese propósito.

![2021-11-06 20-23-11](https://user-images.githubusercontent.com/72868069/140622860-22faebb9-3e12-4af4-8845-7d0a20f53e5f.gif)

## 3. Implementar un controlador que mueva el objeto con wasd

Ya implementado y mostrado en los gifs anteriores.

## Scripts:

```c#
public class ObjectA : MonoBehaviour
{
    public delegate void MethodDelegateNearbyPlayerWithA();
    public static event MethodDelegateNearbyPlayerWithA eventNearbyPlayerWithA;
    private Rigidbody rb;
    private Rigidbody player_rb;
    private bool show = false;
    private int strength = 0;

    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        player_rb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    void Start() 
    {
        ObjectB.eventCollisionPlayerWithB += AddStrength;
        ObjectB.eventCollisionPlayerWithB += ShowMessage;
    }

    void FixedUpdate()
    {
        Vector3 direction = rb.transform.position - player_rb.transform.position;
        float distance = Vector3.Distance (rb.transform.position, player_rb.transform.position);
        if (distance < 4)
        {
            eventNearbyPlayerWithA();
        }
    }

    void AddStrength()
    {
        strength++;
    }

    void ShowMessage()
    {
        show = true;
    }
    
    void OnGUI()
    {
        if (show)
            GUI.Label(new Rect(10, 10, 500, 50), "Golpeado");
    }
}
```

```c#
public class ObjectB : MonoBehaviour
{
    public delegate void MethodDelegateCollisionPlayerWithB();
    public static event MethodDelegateCollisionPlayerWithB eventCollisionPlayerWithB;
    private Rigidbody rb;
    private Rigidbody player_rb;

    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        player_rb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    void Start() {
        ObjectA.eventNearbyPlayerWithA += ChangeColour;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player")
        {
            eventCollisionPlayerWithB();
        }
    }

    void ChangeColour()
    {
        Color newColour = new Color(
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f));
        gameObject.GetComponent<Renderer>().material.color = newColour;
    }
    
}
```

```c#
public class SphereRotation : MonoBehaviour
{

    private Rigidbody rb;
    private Transform tree_transform;
    // Start is called before the first frame update
    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        tree_transform = GameObject.Find("Tree").GetComponent<Transform>();
    }

    void Start()
    {
        ObjectA.eventNearbyPlayerWithA += RotationToTree;
    }

    void RotationToTree()
    {
        rb.transform.LookAt(tree_transform);
    }
}

```

```c#
public class Controller : MonoBehaviour
{
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
}

```
