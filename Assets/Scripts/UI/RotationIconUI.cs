using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class RotationIconUI : IconUI
	{
		public static RotationIconUI Instance { get; private set; }

		public float RotatingDirection { get; set; }

		[SerializeField]
		private Image leftIcon, rightIcon;
		[SerializeField]
		private float turnSpeed = 90.0f, animSpeed = 5.0f;

		void Awake()
		{
			Instance = this;
		}

		protected override void Update()
		{
			base.Update();

			UpdateIcon(leftIcon.rectTransform, -1.0f);
			UpdateIcon(rightIcon.rectTransform, 1.0f);
		}

		private void UpdateIcon(RectTransform rect_transform, float wanted_dir)
		{
			Vector3 angles = rect_transform.eulerAngles;
			if (RotatingDirection == 0.0f || Mathf.Sign(RotatingDirection) != Mathf.Sign(wanted_dir))
			{
				angles.z = Mathf.Lerp(angles.z, 0.0f, Time.deltaTime * animSpeed);
			}
			else
			{
				angles.z += -RotatingDirection * Time.deltaTime * turnSpeed;
			}
			
			rect_transform.eulerAngles = angles;
		} 
	}
}