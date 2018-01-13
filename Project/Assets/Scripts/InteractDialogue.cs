using UnityEngine;

public class InteractDialogue : Interactable{

    public Dialogue dialogue;

    public override void Interact()
    {
        base.Interact();
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
