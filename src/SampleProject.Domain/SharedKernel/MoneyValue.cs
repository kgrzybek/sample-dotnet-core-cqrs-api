namespace SampleProject.Domain.SharedKernel
{
    public class MoneyValue
    {
        public decimal Value { get; }

        public string Currency { get; }

        public MoneyValue(decimal value, string currency)
        {
            this.Value = value;
            this.Currency = currency;
        }
    }
}