//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookReview
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comments
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public System.DateTime PostedOn { get; set; }
        public int BooksId { get; set; }
        public string AspNetUsersId { get; set; }
    
        public virtual Books Books { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
    }

}
