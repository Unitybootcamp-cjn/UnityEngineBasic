using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Dialog
{
    class DialogDataQuest : DialogData
    {
        public string Title { get; set; }
        public string Message { get; private set; }
        public Action<bool> Callback { get; private set; }

        public QuestData Quest { get; private set; }

        public DialogDataQuest(string title, string message, QuestData quest, Action<bool> callback
            ) : base(DialogType.Quest)
        {
            Title = title;
            Message = message;  
            Callback = callback;
            Quest = quest;
        }
    }
}