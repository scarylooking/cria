namespace Cria.Models
{
    public class DrawRequest
    {
        public int MaxWinners { get; set; }

        public DrawRequest()
        {
            MaxWinners = int.MaxValue;
        }
    }
}