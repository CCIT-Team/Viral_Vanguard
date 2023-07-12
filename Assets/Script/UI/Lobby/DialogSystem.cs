using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public string[] dialogueLines; // ��ȭ ������ ������ ���ڿ� �迭
    private int currentLineIndex = 0; // ���� ��ȭ �ε���
    private bool isDialogueActive = false; // ��ȭ Ȱ��ȭ ����
    public TextMeshProUGUI dialogueText; // ��ȭ �ؽ�Ʈ�� ǥ���� TextMeshProUGUI ������Ʈ
    public GameObject operatorDialogButton;
    public GameObject operatorParent;
    public void OnEnable()
    {
        StartDialogue();
    }

    // ��ȭ ����
    public void StartDialogue()
    {
        isDialogueActive = true;
        currentLineIndex = 0;
        // ��ȭ ��� �Լ� ȣ��
        DisplayNextLine();
    }

    // ���� ��ȭ ���
    public void DisplayNextLine()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            // ��ȭ ���
            StartCoroutine(TypeText(dialogueLines[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            // ��ȭ ����
            this.gameObject.SetActive(false);
            operatorParent.SetActive(false);
            EndDialogue();
        }
    }

    // �ؽ�Ʈ�� �� ���ھ� ����ϴ� �ڷ�ƾ
    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char c in text)
        {
            if (c == 'n')
            {
                dialogueText.text += System.Environment.NewLine;
            }
            else
            {
                dialogueText.text += c;
            }
            yield return new WaitForSeconds(0.1f); // ���� �� ������ ���� ����
        }
        operatorDialogButton.SetActive(true);
        //DisplayNextLine();
    }

    // ��ȭ ����
    private void EndDialogue()
    {
        isDialogueActive = false;
        // ��ȭ ���� �� �ʿ��� ���� ����
        // ���� ��� �κ� ȭ������ ���ư��� ���� ó��
    }
}
