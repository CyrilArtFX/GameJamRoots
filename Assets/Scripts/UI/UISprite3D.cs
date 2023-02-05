using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
	public class UISprite3D : UI3D
	{
		protected SpriteRenderer renderer;

		protected override void Awake()
		{
			base.Awake();

			renderer = GetComponent<SpriteRenderer>();
            defaultColor = renderer.color;

            Color color = renderer.color;
            color.a = 0.0f;
            renderer.color = color;
		}

		public override void SetColor( Color color )
		{
            renderer.color = color;
		}
		public override Color GetColor() => renderer.color;
	}
}