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
            
        }
        public void DisplayUI()
        {
            view.gameObject.SetActive(true);
            view.SetLevelFinishedMenu();
        }
    }
}