using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallerIdLib.DataModel
{
    [Table("CallHistory")]
    public class CallHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime CreatedUtc { get; set; }
                
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        public bool InContacts { get; set; }

        [MaxLength(255)]
        public string ContactId { get; set; }

        public bool Resolved { get; set; }

        public CallHistory()
        {            
        }
    }
}
