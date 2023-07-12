using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public string[] dialogueLines; // 대화 내용을 저장할 문자열 배열
    private int currentLineIndex = 0; // 현재 대화 인덱스
    private bool isDialogueActive = false; // 대화 활성화 여부
    public TextMeshProUGUI dialogueText; // 대화 텍스트를 표시할 TextMeshProUGUI 컴포넌트
    public GameObject operatorDialogButton;
    public GameObject operatorParent;
    public void OnEnable()
    {
        StartDialogue();
    }

    // 대화 시작
    public void StartDialogue()
    {
        isDialogueActive = true;
        currentLineIndex = 0;
        // 대화 출력 함수 호출
        DisplayNextLine();
    }

    // 다음 대화 출력
    public void DisplayNextLine()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            // 대화 출력
            StartCoroutine(TypeText(dialogueLines[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            // 대화 종료
            this.gameObject.SetActive(false);
            operatorParent.SetActive(false);
            EndDialogue();
        }
    }

    // 텍스트를 한 글자씩 출력하는 코루틴
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
            yield return new WaitForSeconds(0.1f); // 글자 간 딜레이 조정 가능
        }
        operatorDialogButton.SetActive(true);
        //DisplayNextLine();
    }

    // 대화 종료
    private void EndDialogue()
    {
        isDialogueActive = false;
        // 대화 종료 시 필요한 동작 수행
        // 예를 들면 로비 화면으로 돌아가는 등의 처리
    }
}
