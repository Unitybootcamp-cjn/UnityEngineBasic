using UnityEngine;
using System.Collections.Generic;
using System;


namespace Assets.Scripts.Dialog
{
    public enum DialogType
    {
        Alert, Confirm, Quest
    }
    public sealed class DialogManager : MonoBehaviour
    {
        List<DialogData> _dialogList;                       //���̾�α� ������ ����Ʈ
        Dictionary<DialogType, DialogController> _dialogdict; //���̾�α� Ÿ�Կ� �´� ��Ʈ�ѷ�(��ųʸ�)
        DialogController _currentDialogController;          //������ ���̾�α� ��Ʈ�ѷ�

        private static DialogManager instance = new DialogManager();

        public static DialogManager Instance
        {
            get { return instance; }
        }
        //Ŭ������ �ν��Ͻ��� ������ ��, ����Ʈ�� ��ųʸ��� �ʱ�ȭ�մϴ�.
        public DialogManager()
        {
            _dialogList = new List<DialogData>();
            _dialogdict = new Dictionary<DialogType, DialogController>();
        }

        public void Regist(DialogType type, DialogController controller)
        {
            _dialogdict[type] = controller;
        }

        public void Push(DialogData data)
        {
            _dialogList.Add(data);

            if (_currentDialogController == null)
            {
                ShowNext();
            }
        }
        public void Pop()
        {
            //���� �����ִ� ��ȭâ�� �ݰ�, �����ִ� ���� ��ȭâ�� �����ִ� ����
            if (_currentDialogController != null)
            {
                _currentDialogController.Close(
                            delegate
                            {
                                _currentDialogController = null;

                                if (_dialogList.Count > 0)
                                {
                                    ShowNext();
                                }
                            });
            }
        }


        private void ShowNext()
        {
            DialogData next = _dialogList[0];
            //����Ʈ�� ù��° �� ����

            //�ش� ���� ��ųʸ��� Ű�ν� ������, ��Ʈ�ѷ��� ��ȸ�մϴ�.
            DialogController controller = _dialogdict[next.Type].GetComponent<DialogController>();

            _currentDialogController = controller;

            _currentDialogController.Build(next);

            _currentDialogController.Show(() => { }); //��ȭâ�� ���� ���� ��ó���� ���� �ʴ´�.

            _dialogList.RemoveAt(0); //����Ʈ���� ù��° �� ����
        }

        public bool IsShowing()
        {
            return _currentDialogController != null;
        }
    }
}