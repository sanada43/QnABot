namespace MultiDialogsBot
{
    using System;
    using Microsoft.Bot.Builder.FormFlow;

    [Serializable]
    public class AreaQuery
    {
        [Prompt("{&}を書いてください")]
        public string 住所 { get; set; }


    }
}