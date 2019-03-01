using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        [SerializeField]
        private SpriteRenderer alertSprite;

        public void AlertEnemyView()
        {
            alertSprite.enabled = true;

        }
        public void DisableAlertView()
        {
            alertSprite.enabled = false;
        }


        public void DisableEnemy()
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
        private void Start()
        {
            alertSprite.enabled = false;
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        async public void MoveToLocation(Vector3 location)
        {
            iTween.MoveTo(gameObject, location, 0.5f);
            await new WaitForSeconds(0.5f);
        }

        public void Reset()
        {
            Destroy(this.gameObject);
        }

        async public void RotateEnemy(Vector3 newRotation)
        {
            iTween.RotateTo(gameObject, newRotation, 0.5f);
            await new WaitForSeconds(0.5f);
        }

        public void SetPosition(Vector3 pos)
        {           
            transform.position = pos;
        }

        public void SetRotation(Vector3 pos)
        {
            this.gameObject.transform.Rotate(pos);
        }
    }
}