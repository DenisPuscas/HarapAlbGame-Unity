using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EquippableItem : MonoBehaviour
{
    
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !SelectionManager.Instance.canTakeTheItem && !DialogSystem.Instance.dialogUIActive)
        {
            animator.SetTrigger("hit");
        }
    }
}
