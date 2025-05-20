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
        public Text LabelTitle; //타이틀
        public Text LabelContent; //컨텐츠
        DialogDataQuest Data { get; set; }

        public override void Awake()
        {
            base.Awake();
        }

        public override void Build(DialogData data)
        {
            base.Build(data);

            //데이터 여부 확인
            if (!(data is DialogDataQuest))
            {
                Debug.LogError("Invalid dialog Data!");
                return;
            }
            //메세지 등록
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
            //콜백 호출
            if (Data != null && Data.Callback != null)
            {
                Data.Callback(true);
            }
            //Pop
            DialogManager.Instance.Pop();
        }
        public void OnNoButtonClick()
        {
            //콜백 호출
            if (Data != null && Data.Callback != null)
            {
                Data.Callback(false);
            }
            //Pop
            DialogManager.Instance.Pop();
        }
    }
}