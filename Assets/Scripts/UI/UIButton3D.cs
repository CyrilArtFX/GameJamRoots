using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
	public class UIButton3D : UIText3D
	{
        public UnityEvent OnClickEvent;

        public override void DoClick()
        {
            if (OnClickEvent == null) return;
         
            OnClickEvent.Invoke();
        }
	}
}