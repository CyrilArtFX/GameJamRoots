
using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZoneTrigger : MonoBehaviour
{
	[SerializeField]
	public string menuScene;

	void OnTriggerEnter(Collider collider)
	{
		if ( !collider.TryGetComponent(out FirstPersonController controller) ) return;

		controller.enabled = false;
		ReturnToMenu();
	}

	public void ReturnToMenu()
	{
		BlackFade.instance.eventEndOfFadeIn.AddListener(
			() =>
			{
				SceneManager.LoadScene(menuScene);
			}
		);
		BlackFade.instance.StartFade(FadeType.BothFadesWithRestore, Color.white);
	}
}