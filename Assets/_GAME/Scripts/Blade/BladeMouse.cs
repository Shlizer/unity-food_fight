using UnityEngine;

namespace FoodFight
{
    public class BladeMouse : BladeBase
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
                StartSlicing();
            else if (Input.GetMouseButtonUp(0))
                StopSlicing();
            else if (slicing)
                ContinueSlicing();
        }

        private void ContinueSlicing()
        {
            var lastPosition = transform.position;
            SetNewPosition();

            direction = transform.position - lastPosition;

            float velocity = direction.magnitude / Time.deltaTime;
            bladeCollider.enabled = velocity > gameManager.minSliceVelocity;
        }

        protected override void SetNewPosition()
        {
            Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0f;

            transform.position = newPosition;
        }
    }
}
