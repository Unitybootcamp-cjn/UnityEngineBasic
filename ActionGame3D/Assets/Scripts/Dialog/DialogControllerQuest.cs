using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.Dialog
{

    class DialogControllerQuest : DialogController
    {
        public Text LabelTitle; //Ÿ��Ʋ
        public Text LabelContent; //������
        DialogDataQuest Data { get; set; }

        public override void Awake()
        {
            base.Awake();
        }

        public override void Build(DialogData data)
        {
            base.Build(data);

            //������ ���� Ȯ��
            if (!(data is DialogDataQuest))
            {
                Debug.LogError("Invalid dialog Data!");
                return;
            }
            //�޼��� ���
            Data = data as DialogDataQuest;
            LabelTitle.text = Data.Title;
            LabelContent.text = Data.Message;
        }

        public override void Start()
        {
            base.Start();
            DialogManager.Instance.Regist(DialogType.Quest, this);
        }

        public void OnYesButtonClick()
        {
            //�ݹ� ȣ��
            if (Data != null && Data.Callback != null)
            {
                Data.Callback(true);
            }
            //Pop
            DialogManager.Instance.Pop();
        }
        public void OnNoButtonClick()
        {
            //�ݹ� ȣ��
            if (Data != null && Data.Callback != null)
            {
                Data.Callback(false);
            }
            //Pop
            DialogManager.Instance.Pop();
        }
    }
}