using UnityEngine;

namespace UIservice
{
    public class LevelFinishedUIView : MonoBehaviour,IUIView
    {
        public MenuUIControllerView view;
        private void OnEnable() {
            //view=GameObject.FindObjectOfType<MenuUIControllerView>();
        }
        public void DestroyUI()
        {
            view.gameObject.SetActive(false);
            view.SetLevelFinishedMenu();
        }
        public void DisplayUI()
        {
            view.gameObject.SetActive(true);
        }
    }
}