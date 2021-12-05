using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviourSingleton<SceneLoader>
{
    [SerializeField] private GameObject loaderCanvas;
    [SerializeField] private Image progressBar;
    [SerializeField] private float delayLoadFake;
    [SerializeField] private int waitForActiveScene;
    private float target;
    private float maximumLoadOperation = 0.9f;

    void Start()
    {
        loaderCanvas.SetActive(false);
        target = 0;
        progressBar.fillAmount = 0;
    }

    void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, delayLoadFake * Time.deltaTime);
    }

    public async void LoadSceneAsyncWithProgressBar(string sceneName)
    {
        target = 0;
        progressBar.fillAmount = 0;

        var loadOperation = SceneManager.LoadSceneAsync(sceneName);
        loadOperation.allowSceneActivation = false;
        loaderCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);
            target = loadOperation.progress;
        } while (loadOperation.progress < maximumLoadOperation);

        await Task.Delay(waitForActiveScene);

        loadOperation.allowSceneActivation = true;
        loaderCanvas.SetActive(false);
    }

    public void ReloadGameplay(int inSeconds)
    {
        IEnumerator WaitForLoadScene()
        {
            float t = 0;

            while (t < inSeconds)
            {
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            SceneManager.LoadScene("Gameplay");
            yield break;
        }

        StartCoroutine(WaitForLoadScene());
    }

    public void QuitApplication()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
