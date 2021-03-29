using TelegramBot.Dto.TelegramApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace TelegramBot
{
    class Asker
    {
        private readonly List<IBotAnswer> answers;

        public Asker(List<IBotAnswer> answers)
        {
            this.answers = answers;


        }
        public void Ask(string param, Message id)
        {
            var PossibleResponses = answers.Where(w =>
            {
                return w.CanWork(param);
            });
            foreach (var r in PossibleResponses)
            {
                 r.Answer(id);
            }
        }
    }
}
