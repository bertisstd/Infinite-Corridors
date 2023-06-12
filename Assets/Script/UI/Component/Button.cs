
namespace Bertis.UI
{
	using UnityEngine;
	using UnityEngine.EventSystems;
	using Bertis.Runtime;

	public class Button : UnityEngine.UI.Button, IPointerClickHandler
	{
		static private AudioClip s_ClickSfx;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			s_ClickSfx = ConfigProvider.GetSfx(-2105337166); /*ButtonClick*/
		}

		public new void OnPointerClick(PointerEventData eventData)
		{
			AudioHandler.Play(s_ClickSfx);
			base.OnPointerClick(eventData);
		}

	}
}