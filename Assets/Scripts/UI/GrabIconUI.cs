using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class GrabIconUI : IconUI
	{
		public static GrabIconUI Instance { get; private set; }

		public Image Image { get; set; }

		[SerializeField]
		private Sprite grabSprite, noGrabSprite;

		void Awake()
		{
			Image = GetComponent<Image>();
			Instance = this;
		}

		public void SetGrabbing(bool is_grabbing)
		{
			Image.sprite = is_grabbing ? grabSprite : noGrabSprite;
		}
	}
}