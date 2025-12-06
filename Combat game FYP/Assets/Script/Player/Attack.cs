using UnityEngine;

public class Attack : MonoBehaviour, IAction
{
    public void ExecuteAction()
    {
        Debug.Log("Attack");
    }
}
