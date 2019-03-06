using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.UI;

namespace UIservice
{
    public class LobbyStarController : MonoBehaviour
    {

        [SerializeField]
        private Image starImg;

        public void SetStarColor(Color color)
        {
            starImg.color = color;
        }
    }
}
