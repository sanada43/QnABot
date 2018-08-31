namespace MultiDialogsBot.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private const string ClassOption = "ごみの分別";
        private const string MethodOption = "ごみの捨て方";
        private const string AreaOption = "ごみ捨て地区";
        private const string DocumentOption = "資料ください";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync($"こんにちは、こちらは北海道森町のごみ捨てボットです。");
            //context.Wait(this.MessageReceivedAsync);//何かメッセージが来るまで待つ
        }
        
        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (message.Text.ToLower().Contains("help") || message.Text.ToLower().Contains("support") || message.Text.ToLower().Contains("problem"))
            {
                //await context.Forward(new SupportDialog(), this.ResumeAfterSupportDialog, message, CancellationToken.None);
            }
            else
            {
                this.ShowOptions(context);
            }
        }

        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnOptionSelected, new List<string>() { ClassOption, MethodOption, AreaOption, DocumentOption }, "何をいたしますか？", "4");
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    case ClassOption:
                        context.Call(new AreaDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case MethodOption:
                        context.Call(new MethodDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case AreaOption:
                        context.Call(new AreaDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case DocumentOption:
                        context.Call(new DocumentDialog(), this.ResumeAfterSupportDialog);
                        break;
                }
            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync($"おや、ちゃんと入力ができていないみたいです");

                context.Wait(this.MessageReceivedAsync);
            }
        }
        
        private async Task ResumeAfterSupportDialog(IDialogContext context, IAwaitable<int> result)
        {
            var ticketNumber = await result;

            await context.PostAsync($"Thanks for contacting our support team. Your ticket number is {ticketNumber}.");
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}
