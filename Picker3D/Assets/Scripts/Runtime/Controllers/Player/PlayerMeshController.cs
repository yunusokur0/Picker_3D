using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerMeshController : MonoBehaviour
{
    [SerializeField] private PlayerManager manager;
    [SerializeField] private new Renderer renderer;
    [SerializeField] private TextMeshPro scaleText;
    [SerializeField] private ParticleSystem confettiParticle;

    private PlayerMeshData _data;

    private void Awake()
    {
        scaleText.gameObject.SetActive(false);
    }

    internal void SetData(PlayerMeshData scaleData)
    {
        _data = scaleData;
    }

    internal void ScaleUpPlayer()
    {
        renderer.gameObject.transform.DOScaleX(_data.ScaleCounter, 1).SetEase(Ease.Flash);
    }

    internal void ShowUpText()
    {
        scaleText.gameObject.SetActive(true);
        scaleText.DOFade(1, 0f).SetEase(Ease.Flash).OnComplete(() => scaleText.DOFade(0, 0).SetDelay(.65f));
        scaleText.rectTransform.DOAnchorPosY(.85f, .65f).SetRelative(true).SetEase(Ease.OutBounce).OnComplete(() =>
            scaleText.rectTransform.DOAnchorPosY(-.85f, .65f).SetRelative(true));
    }

    internal void PlayConfetiParticle()
    {
        confettiParticle.Play();
    }

    internal void OnReset()
    {
        renderer.gameObject.transform.DOScaleX(1, 1).SetEase(Ease.Linear);
    }
}

