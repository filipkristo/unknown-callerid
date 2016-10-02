using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallerIdLib.DataModel
{
    public class CallHistory
    {
        public Guid Id { get; set; }
        public DateTime CreatedUtc { get; set; }

        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        public bool InContacts { get; set; }

        [MaxLength(255)]
        public string ContactId { get; set; }

        public bool Resolved { get; set; }

        public CallHistory()
        {
            Id = Guid.NewGuid();
        }
    }
}
