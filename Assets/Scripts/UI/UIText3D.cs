using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
	public class UIText3D : UI3D
	{
		protected TextMeshPro textMesh;

		protected override void Awake()
		{
			base.Awake();

			textMesh = GetComponent<TextMeshPro>();
            defaultColor = textMesh.color;

            Color color = textMesh.color;
            color.a = 0.0f;
            textMesh.color = color;
		}

		public override void SetColor( Color color )
		{
            textMesh.color = color;
		}
		public override Color GetColor() => textMesh.color;
	}
}