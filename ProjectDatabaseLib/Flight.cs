//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectDatabaseLib
{
    using System;
    using System.Collections.Generic;
    
    public partial class Flight
    {
        public int F_Id { get; set; }
        public string F_Name { get; set; }
        public string Start_From { get; set; }
        public string Destination { get; set; }
        public string Departure_Date { get; set; }
        public string Departure_Time { get; set; }
        public Nullable<int> LocationId { get; set; }
    
        public virtual Location Location { get; set; }
    }
}
