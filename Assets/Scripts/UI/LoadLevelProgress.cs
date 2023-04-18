using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelProgress : MonoBehaviour
{
    private static LoadLevelProgress _instance;
    [SerializeField] private Animator _anim;
    private AsyncOperation _loadSceneOperation;
    [SerializeField] private Image _progressImage;
    private static bool _playOpeningAnimation = false; // Проигрывать ли анимацию открытия сцены (черный экран становится прозрачным)

    private void Start()
    {
        _instance = this;
        if (_playOpeningAnimation)
        {
            _anim.SetTrigger("SceneOpen");
        }
    }

    private void Update()
    {
        if (_loadSceneOperation != null)
        {
            _progressImage.fillAmount = _loadSceneOperation.progress;
        }
    }

    /// <summary>
    /// Вызывается при нажатии на кнопку Start game. Загружается игровая сцена со слайдером загрузки 
    /// </summary>
    /// <param name="sceneIndex">Индекс сцены</param>
    public void SwitchToScene(int sceneIndex)
    {
        _anim.SetTrigger("SceneClose");
        _loadSceneOperation = SceneManager.LoadSceneAsync(sceneIndex);
        _loadSceneOperation.allowSceneActivation = false;
        StartCoroutine(Delay());
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        _loadSceneOperation.allowSceneActivation = true;
        _playOpeningAnimation = true;
    }
}
