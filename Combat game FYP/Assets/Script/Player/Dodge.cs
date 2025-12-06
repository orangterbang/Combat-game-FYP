using UnityEngine;

public class Dodge : MonoBehaviour, IAction
{
    public float dodgeSpeed;
    public Vector3 moveDirection{ private get; set;}

    public void ExecuteAction()
    {
        transform.position += moveDirection * dodgeSpeed * Time.deltaTime;

        dodgeSpeed -= dodgeSpeed * 10f * Time.deltaTime;
        /*if(dodgeSpeed < 3f)
        {
            dodgeSpeed = 0;
        }*/
    }
}
