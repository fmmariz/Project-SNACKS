using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnim : MonoBehaviour
{

    [SerializeField] private AnimationCurve _animCurve;
    [SerializeField] private CinemachineVirtualCamera _deathCam;
    [SerializeField] private CinemachineVirtualCamera _playerCam;
    private SpriteRenderer _sr;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDeathAnim()
    {
        GameController.Instance.charController.SetDead(true);
        ActivateDeathCam();
        StartCoroutine(deathAnimation());
    }

    IEnumerator deathAnimation()
    {
        float initialY = transform.position.y;
        float finalY = transform.position.y - 4;
        float finalX = transform.position.x;
        for(float i = 0; i < 1; i += 0.008f)
        {
            transform.position = new Vector3(finalX, (initialY-4f) + (4 * _animCurve.Evaluate(i))); 
            yield return new WaitForEndOfFrame();
        }
        DeactivateDeathCam();
        GameController.Instance.charController.Reset();
        GameController.Instance.charController.SetDead(false);

        yield return null;
    }

    private void ActivateDeathCam()
    {
        _deathCam.gameObject.SetActive(true);
    }

    private void DeactivateDeathCam()
    {
        _deathCam.gameObject.SetActive(false);
    }
}
