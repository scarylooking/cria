using System;

namespace Cria.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Prize { get; set; }

        public Ticket()
        {
            Id = Guid.NewGuid();
        }
    }
}
