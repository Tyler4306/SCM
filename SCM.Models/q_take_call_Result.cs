//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCM
{
    using System;
    
    public partial class q_take_call_Result
    {
        public int Id { get; set; }
        public string TDate { get; set; }
        public string TTime { get; set; }
        public string TExtCode { get; set; }
        public string TLineNo { get; set; }
        public string THeader { get; set; }
        public string TNo { get; set; }
        public string TDur { get; set; }
        public bool IsTaken { get; set; }
        public string TakenByExt { get; set; }
        public string TakenBy { get; set; }
        public Nullable<System.DateTime> TakenOn { get; set; }
        public System.DateTime LogTime { get; set; }
        public Nullable<int> LinkedTo { get; set; }
    }
}
