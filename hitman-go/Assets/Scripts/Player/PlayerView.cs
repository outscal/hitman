using UnityEngine;
using Enemy;
using System.Collections;

namespace Player
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public void DisablePlayer()
        {
            gameObject.SetActive(false);
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        public void MoveToLocation(Vector3 _location)
        {
            this.transform.LookAt(_location);
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, _location, 1f);
        }

        public void Reset()
        {
            Destroy(gameObject);
        }
    }
}