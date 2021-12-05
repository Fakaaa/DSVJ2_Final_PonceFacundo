using UnityEngine;
using TMPro;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI versionText;   

    void Start()
    {
        versionText.text = "v" + Application.version;
    }

    void Update()
    {
        
    }
}
