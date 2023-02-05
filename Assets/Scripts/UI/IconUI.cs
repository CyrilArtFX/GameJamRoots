using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class IconUI : MonoBehaviour
	{
		public bool IsShown
		{
			get => gameObject.activeSelf;
			set => gameObject.SetActive(value);
		}

		public Transform Target;

		void Start()
		{
			IsShown = false;
		}

		protected virtual void Update()
		{
			if (Target == null) return;

			Camera camera = Camera.main;
			Vector2 viewport_size = camera.pixelRect.size;
			Vector2 pos = camera.WorldToScreenPoint( Target.position );

			transform.position = new(
				Mathf.Clamp(pos.x, 0.0f, viewport_size.x),
				Mathf.Clamp(pos.y, 0.0f, viewport_size.y)
			);
		}
	}
}
