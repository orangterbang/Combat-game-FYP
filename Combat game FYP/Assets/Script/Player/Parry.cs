using UnityEngine;

public class Parry : MonoBehaviour, IAction
{
    public void ExecuteAction()
    {
        Debug.Log("Parry");
    }
}
