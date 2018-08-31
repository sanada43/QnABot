namespace MultiDialogsBot
{
    using System;
    using Microsoft.Bot.Builder.FormFlow;

    [Serializable]
    public class ClassQuery
    {
        [Prompt("{&}を書いてください")]
        public string ごみの名前 { get; set; }
        
    }
}