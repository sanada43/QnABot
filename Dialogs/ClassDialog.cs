﻿namespace MultiDialogsBot.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.FormFlow;

    [Serializable]
    public class ClassDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)//ダイアログで一番初めに動く
        {
            await context.PostAsync("ここではゴミの分類と捨て方を調べることができます。");

            var hotelsFormDialog = FormDialog.FromForm(this.BuildHotelsForm, FormOptions.PromptInStart);

            context.Call(hotelsFormDialog, this.ResumeAfterHotelsFormDialog);//クエリが完成したらアフターダイアログに飛ぶ
        }

        private IForm<ClassQuery> BuildHotelsForm()//クエリの中の情報が満たされるまで繰り返す
        {
            OnCompletionAsyncDelegate<ClassQuery> processHotelsSearch = async (context, state) =>
            {
                await context.PostAsync($"{state.ごみの名前}の分類は...");
            };

            return new FormBuilder<ClassQuery>()
                .Field(nameof(ClassQuery.ごみの名前))
                .Message("{ごみの名前}を調べてみます...")
                .AddRemainingFields()
                .OnCompletion(processHotelsSearch)
                .Build();
        }

        private async Task ResumeAfterHotelsFormDialog(IDialogContext context, IAwaitable<ClassQuery> result)//
        {
            try
            {
                var searchQuery = await result;
                

                var hotels = await this.GetHotelsAsync(searchQuery);
                foreach (var hotel in hotels)
                {
                    await context.PostAsync($"{searchQuery.ごみの名前}の分類は{hotel.Location} です。");
                }
                /*
                var resultMessage = context.MakeMessage();
                resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                resultMessage.Attachments = new List<Attachment>();

                foreach (var hotel in hotels)
                {
                    HeroCard heroCard = new HeroCard()
                    {
                        Title = hotel.Name,
                        Subtitle = $"{hotel.Rating} starts. {hotel.NumberOfReviews} reviews. From ${hotel.PriceStarting} per night.",
                        Images = new List<CardImage>()
                        {
                            new CardImage() { Url = hotel.Image }
                        },
                        Buttons = new List<CardAction>()
                        {
                            new CardAction()
                            {
                                Title = "More details",
                                Type = ActionTypes.OpenUrl,
                                Value = $"https://www.bing.com/search?q=hotels+in+" + HttpUtility.UrlEncode(hotel.Location)
                            }
                        }
                    };

                    resultMessage.Attachments.Add(heroCard.ToAttachment());
                }

                await context.PostAsync(resultMessage);*/
            }
            catch (FormCanceledException ex)
            {
                string reply;

                if (ex.InnerException == null)
                {
                    reply = "You have canceled the operation. Quitting from the HotelsDialog";
                }
                else
                {
                    reply = $"Oops! Something went wrong :( Technical Details: {ex.InnerException.Message}";
                }

                await context.PostAsync(reply);
            }
            finally
            {
                context.Done<object>(null);
            }
        }

        private async Task<IEnumerable<Hotel>> GetHotelsAsync(ClassQuery searchQuery)//ホテルデータランダム生成
        {
            var hotels = new List<Hotel>();
            /*
            // Filling the hotels results manually just for demo purposes
            for (int i = 1; i <= 5; i++)
            {
                var random = new Random(i);
                Hotel hotel = new Hotel()
                {
                    Name = $"{searchQuery.ごみの名前} Hotel {i}",
                    Location = searchQuery.ごみの名前,
                    Rating = random.Next(1, 5),
                    NumberOfReviews = random.Next(0, 5000),
                    PriceStarting = random.Next(80, 450),
                    Image = $"https://placeholdit.imgix.net/~text?txtsize=35&txt=Hotel+{i}&w=500&h=260"
                };

                hotels.Add(hotel);
            }

            hotels.Sort((h1, h2) => h1.PriceStarting.CompareTo(h2.PriceStarting));*/


            string data = new QnAMaker.Program(searchQuery.ごみの名前);
            var random = new Random();
            Hotel hotel = new Hotel()
            {
                Name = $"{searchQuery.ごみの名前}",
                Location = data,
                Rating = random.Next(1, 5),
                NumberOfReviews = random.Next(0, 5000),
                PriceStarting = random.Next(80, 450)
            };

            hotels.Add(hotel);


            return hotels;
        }
    }
}