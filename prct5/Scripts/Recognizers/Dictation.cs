using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

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
