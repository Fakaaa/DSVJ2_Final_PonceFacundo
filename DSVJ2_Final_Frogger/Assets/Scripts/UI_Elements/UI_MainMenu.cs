using UnityEngine;
using TMPro;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI versionText;
    [SerializeField] Animator animSplahs;

    void Start()
    {
        versionText.text = "v" + Application.version;
        animSplahs.SetBool("Splash1",true);
    }

}
