using UnityEngine;

namespace UIservice
{
    public class GameOverUIView : MonoBehaviour, IUIView
    {
        public void DestroyUI()
        {
            gameObject.SetActive(false);
        }

        public void DisplayUI()
        {
            gameObject.SetActive(true);
        }
    }
}