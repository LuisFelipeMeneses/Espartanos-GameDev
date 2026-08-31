using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace EspartanosGameDev.Lessons.Coroutine
{
public class PlayerScript : MonoBehaviour
{
    IEnumerator moveCoroutine;
    WaitForSeconds oneSecond = new(1f);

    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.z = 0f;

            if(moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine); 
            }   
            transform.localScale = new Vector3(1f, 1f, 1f);
            moveCoroutine = MoveCoroutine(mousePos);
            StartCoroutine(moveCoroutine);
        }
    }

    IEnumerator MoveToMousePosition(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                5f * Time.deltaTime
            );

            if (transform.localScale != Vector3.one)
            {
                transform.localScale = Vector3.MoveTowards(
                    transform.localScale,
                    Vector3.one,
                    2f * Time.deltaTime
                );
            }

            yield return null;
        }
    }

    IEnumerator ScaleChangeCoroutine(Vector3 targetScale)
    {
        while(Vector3.Distance(transform.localScale, targetScale) > 0.01f)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, 2f * Time.deltaTime);
            yield return null;
        }
        transform.localScale = targetScale;
    }

    IEnumerator MoveCoroutine(Vector3 targetPos)
    {
        yield return MoveToMousePosition(targetPos);
        yield return oneSecond;
        yield return ScaleChangeCoroutine(new Vector3(2f, 2f, 2f));
        yield return ScaleChangeCoroutine(new Vector3(1f, 1f, 1f));
    }
}
}