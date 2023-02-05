using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
	[RequireComponent(typeof(BoxCollider))]
	public class UI3D : MonoBehaviour
	{
		public bool IsHovered { get; set; }
		public float TargetAlpha { get; set; }

		private const float HOVER_COLOR_OFFSET = 0.5f;

		[SerializeField]
		private float smoothSpeed = 2.0f;

		protected Color defaultColor;

		protected virtual void Awake()
		{
			TargetAlpha = 1.0f;
		}

		protected virtual void Update()
		{
			Color color = defaultColor;
			color.a = Mathf.Lerp(GetColor().a, TargetAlpha, Time.deltaTime * smoothSpeed);
			ApplyColor(color);
		}

		public virtual void DoClick() {}

		public virtual void ApplyColor(Color color)
		{
			if (IsHovered)
			{
				color = new Color(color.r + HOVER_COLOR_OFFSET, color.g + HOVER_COLOR_OFFSET, color.b + HOVER_COLOR_OFFSET, color.a);
			}

			SetColor(color);
		}

		public virtual void SetColor(Color color) {}
		public virtual Color GetColor() => Color.white;
	}
}