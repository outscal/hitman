using UnityEngine;
using UnityEngine.UI;

namespace UIservice
{
    public class CardControllerView : MonoBehaviour
    {
        public Text cardName;
        public GameObject showTrue, showFalse;
        private void OnEnable() {
          Debug.Log("card Enabled");  
        }
        private void OnDestroy() {
            Debug.Log("card Destroyed");
        }

        public void setCardName(string name)
        {
            cardName.text = name;
        }
        public void SetAchievement(bool set)
        {
            showTrue.SetActive(set);
            showFalse.SetActive(!set);
        }
    }
}