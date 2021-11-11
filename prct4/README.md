# Práctica: Micrófono y cámara

La escena implementada consiste en un cubo controlable con un componente AudioSource el cual puede grabar clips de audio de 1 segundo al pulsar la tecla R y reproducirlos al pulsar la tecla P. También existe en la escena un Cubo que tiene como textura la entrada de la WebCam. Si se reproduce un clip de audio de volumen alto cerca del Cubo, este explotará y se dividirá en cientos de cubos que mantendrán la textura de la cámara. La comunicación entre clases se realiza mediante delegados, de forma que cuando el volumen del clip es alto, se activa el delegado que ejecuta la función de explosión del Cubo.

# Scripts

## Cámara

```c#
public class CameraTexture : MonoBehaviour
{
    public float cubeSize = 0.2f;
    public int cubesInRow = 5;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    WebCamTexture webcamTexture;
    // Start is called before the first frame update
    void Start()
    {
        MicrophoneControl.eventBigVolumeWithCamera += Explode;
        webcamTexture = new WebCamTexture();
        WebCamDevice[] devices = WebCamTexture.devices;
        webcamTexture.deviceName = devices[2].name;
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        //use this value to create pivot vector)
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode() {
        //make object disappear
        gameObject.SetActive(false);

        //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < cubesInRow; x++) {
            for (int y = 0; y < cubesInRow; y++) {
                for (int z = 0; z < cubesInRow; z++) {
                    createPiece(x, y, z);
                }
            }
        }

        //get explosion position
        Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders) {
            //get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) {
                //add explosion force to this body with given parameters
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }

    }

    void createPiece(int x, int y, int z) {

        //create piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer renderer = piece.GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;

        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
    }
}
```

En este script, se crea una nueva textura que consiste en la imagen que recibe nuestra cámara y se guarda en una variable global. La cámara se enciende nada más iniciar la escena. La función Explode elimina el cubos y crea 125 cubos pequeños que salen disparados para simular una explosión. Cada uno de ellos toma como textura la entrada de la cámara.

## Micrófono

```c#
public class MicrophoneControl : MonoBehaviour
{
    public delegate void MethodDelegateBigVolumeWithCamera();
    public static event MethodDelegateBigVolumeWithCamera eventBigVolumeWithCamera;
    Microphone microphone = new Microphone();
    AudioSource source;
    string[] devices;
    private float clipLoudness;
    private float[] clipSampleData;

    void Awake()
    {      
        devices = Microphone.devices;
        source = GetComponent<AudioSource>();
        clipSampleData = new float[1024];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            source.clip = Microphone.Start(devices[0], false, 1, 44100);
            
        }
        if (Input.GetKey(KeyCode.P))
        {
            source.Play();
            source.clip.GetData(clipSampleData, source.timeSamples);
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= 1024;
            Debug.Log(clipLoudness);
            if (clipLoudness > 0.3f)
            {
                eventBigVolumeWithCamera();
            }
        }
    }
}```

El micrófono graba al pulsar la tecla R (de record) y se reproduce el audio al pulsar la tecla P (de play). A continuación, se obtienen 1024 muestras del clip (que se guardan en clipSampleData) y calculamos la media de los volumenes de cada muestra. Si la media es superior a cierto valor, significa que el volumen es alto, por tanto activamos el evento de la explosión del cubo con la cámara.
