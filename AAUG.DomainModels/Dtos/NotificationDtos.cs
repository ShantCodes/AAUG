using AAUG.DomainModels.ViewModels.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAUG.DomainModels.Dtos
{
    public class NotificationInsertDto
    {
        public string Endpoint { get; set; }
        public string P256dh { get; set; }
        public string Auth { get; set; }
        public bool IsActive { get; set; }
    }
}
