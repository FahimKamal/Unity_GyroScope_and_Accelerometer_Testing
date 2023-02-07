using UnityEngine;

public class RotateCube : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        var deviceRotation = DeviceRotation.Get();

        // var rotation = new Quaternion(0, 0, deviceRotation.z, 0);
        transform.rotation = Quaternion.Euler(0, 0, deviceRotation.eulerAngles.z); 
    }
}
