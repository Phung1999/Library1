//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryDatabase.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Deparment
    {
        public int DeparmentID { get; set; }
        public string Name { get; set; }
        public Nullable<int> IdParent { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> LastEdit { get; set; }
        public Nullable<int> IDGroupUser { get; set; }
    }
}
