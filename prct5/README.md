# Práctica 5: Reconocimiento de Voz

La escena implementada consiste en un auto-cine con un aparcamiento en el que controlamos un coche. Frente a nosotros, hay un montón de coches aparcados, dejando 2 huecos libres entre ellos.

Al aparcar el coche en el hueco de la izquierda, se activará el KeywordRecognizer. En la pantalla del cine aparecerá un texto indicándonos qué palabras se pueden reconocer, entre las cuales se encuentran los colores: rojo, azul, verde y amarillo. Al decir uno de estos 4 colores por el micrófono, el coche cambiará su color al indicado mediante la voz.

En cambio, si aparcamos el coche a la derecha, comenzará el DictationRecognizer, lo que hará que se vayan imprimiendo en la pantalla del cine las frases que vayamos diciendo.

# Scripts

```c#
public class Keywords : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer;
    string[] keyWords = {"hola", "autocine", "pato", "rojo", "azul", "verde", "amarillo"};
    TextMesh text;
    MeshRenderer renderer;
    Material[] materials;
    const int PRIMARY_BODY_COLOR = 5;
    const int SECONDARY_BODY_COLOR = 4; 

    void Awake()
    {
        renderer = GameObject.FindGameObjectWithTag("Body").GetComponent<MeshRenderer>();
        materials = renderer.materials;
    }

    void Start()
    {
        GameManager.eventRecognizeKeywordEnter += startRecognition;
        GameManager.eventRecognizeKeywordExit += stopRecognition;
        keywordRecognizer = new KeywordRecognizer(keyWords);
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        text = GameObject.FindGameObjectWithTag("Pantalla").GetComponent<TextMesh>();
    }

    void OnDestroy()
    {
        keywordRecognizer.Dispose();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (args.text == "rojo")
        {
            materials[PRIMARY_BODY_COLOR].color = new Color(150, 0, 0);
            materials[SECONDARY_BODY_COLOR].color = new Color(150, 0, 0);
        }
        if (args.text == "azul")
        {
            materials[PRIMARY_BODY_COLOR].color = new Color(0, 0, 255);
            materials[SECONDARY_BODY_COLOR].color = new Color(0, 0, 255);
        }
        if (args.text == "verde")
        {
            materials[PRIMARY_BODY_COLOR].color = new Color(0, 255, 0);
            materials[SECONDARY_BODY_COLOR].color = new Color(0, 255, 0);
        }
        if (args.text == "amarillo")
        {
            materials[PRIMARY_BODY_COLOR].color = new Color(255, 255, 0);
            materials[SECONDARY_BODY_COLOR].color = new Color(255, 255, 0);
        }
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
        builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
        builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
        Debug.Log(builder.ToString());
        text.text += "- " + args.text + " (" + args.confidence + ")\n";
    }

    void startRecognition()
    {
        Debug.Log("KeywordsRecognizer ha comenzado");
        text.text = "Palabras a reconocer:\n";
        for (int i = 0; i < keyWords.Length; i++)
        {
            text.text += keyWords[i] + " "; 
        }
        text.text += "\n";
        keywordRecognizer.Start();
    }

    void stopRecognition()
    {   
        Debug.Log("KeywordsRecognizer ha parado");
        keywordRecognizer.Stop();
        PhraseRecognitionSystem.Shutdown();
    }
}
```

Primero se inicia el KeywordRecognizer y se definen las palabras que queremos que reconozca. Mediante el uso de delegados, se activa el KeywordRecognizer al entrar en una determinada zona, y se desactiva al salir de ella. Después se añade la función OnPhraseRecognized al KeywordRecognizer, función en la que definiremos el comportamiento del programa en función de las palabras que son reconocidas (escribirla en la pantalla del autocine y, además, cambiar el material del coche en caso de que la palabra sea un color.

````c#
public class Dictation : MonoBehaviour
{
    [SerializeField]
    private TextMesh m_Hypotheses;

    [SerializeField]
    private TextMesh m_Recognitions;

    private DictationRecognizer m_DictationRecognizer;

    void Start()
    {
        GameManager.eventDictationEnter += startRecognizing;
        GameManager.eventDictationExit += stopRecognizing;
        m_Hypotheses = GameObject.FindGameObjectWithTag("Pantalla").GetComponent<TextMesh>();
        m_Recognitions = m_Hypotheses;
    }

    void OnDestroy()
    {
        m_DictationRecognizer.Dispose();
    }

    void startRecognizing()
    {
        Debug.Log("DictationRecognizer ha comenzado");
        createRecognizer();
        m_DictationRecognizer.Start();
        m_Hypotheses.text = "Dictation recognizer activado\n";
    }

    void stopRecognizing()
    {
        Debug.Log("DictationRecognizer ha parado");
        m_DictationRecognizer.Stop();
        m_DictationRecognizer.Dispose();
    }

    void createRecognizer()
    {
        m_DictationRecognizer = new DictationRecognizer();

        m_DictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
            m_Recognitions.text += "Resultado: " + text + "\n";
        };

        m_DictationRecognizer.DictationHypothesis += (text) =>
        {
            Debug.LogFormat("Dictation hypothesis: {0}", text);
            m_Hypotheses.text = text + "\n";
        };

        m_DictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
            m_Hypotheses.text = "";
        };

        m_DictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };
    }
}
```


