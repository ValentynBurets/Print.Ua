using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interface.Models
{
    public class CommentViewModel
    {
        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
