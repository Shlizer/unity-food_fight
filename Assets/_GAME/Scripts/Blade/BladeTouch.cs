using UnityEngine;

public class BladeTouch : BladeBase
{
    private Touch touch;

    public void SetTouch(Touch touch) => this.touch = touch;

    void Update()
    {
        if (slicing) ContinueSlicing();
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
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(touch.position);
        newPosition.z = 0f;

        transform.position = newPosition;
    }
}
