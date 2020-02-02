namespace SampleProject.Application.Configuration.Emails
{
    public struct EmailMessage
    {
        public string From { get; }

        public string To { get; }

        public string Content { get; }

        public EmailMessage(
            string from, 
            string to,
            string content)
        {
            this.From = from;
            this.To = to;
            this.Content = content;
        }
    }
}