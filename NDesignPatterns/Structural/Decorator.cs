using System;

namespace NDesignPatterns.Structural
{
    public interface IEmailSender
    {
        void SendEmail(string content);
    }

    public class EmailSender : IEmailSender
    {
        public void SendEmail(string content)
        {
            Console.WriteLine($"Sending email with content {content}");
        }
    }

    public class TranslatedEmailSender : IEmailSender
    {
        private readonly IEmailSender _emailSender;
        private readonly Translator _translator;

        public TranslatedEmailSender(IEmailSender emailSender, Translator translator)
        {
            _emailSender = emailSender;
            _translator = translator;
        }

        public void SendEmail(string content)
        {
            var translatedContent = _translator.Translate(content);
            _emailSender.SendEmail(translatedContent);
        }
    }

    public class Translator
    {
        public string Translate(string text)
        {
            Console.WriteLine($"Translating text {text}...");
            return text;
        }
    }
}
