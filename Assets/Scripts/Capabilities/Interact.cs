using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Interact : MonoBehaviour
{
    private Controller _controller;

    private void Awake()
    {
        _controller = GetComponent<Controller>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Interactable")) return;

        collision.gameObject.GetComponent<Interactable>().interactableObject.InteractionStart();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Interactable")) return;
        bool isInteractKeyDown = _controller.input.RetrieveInteractInput();
        collision.gameObject.GetComponent<Interactable>().interactableObject.InteractionContinues(isInteractKeyDown);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Interactable")) return;

        collision.gameObject.GetComponent<Interactable>().interactableObject.InteractionEnd();
    }



}