using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Entities.Models
{
    partial class Person
    {
       // public string FullName => $"{this.FirstName} {this.FatherName } {this.LastName }";
        public string FullNameThree => $"{this.FirstName} {this.FatherName} {this.GrandFatherName}" ;
        public string FullName => $"{(!string.IsNullOrEmpty(this.FirstName) ? this.FirstName.Trim() : "")}{(!string.IsNullOrEmpty(this.FatherName) ? " " + this.FatherName.Trim() : "")}{(!string.IsNullOrEmpty(this.GrandFatherName) ? " " + this.GrandFatherName.Trim() : "")}{(!string.IsNullOrEmpty(this.GreatGrandFatherName) ? " " + this.GreatGrandFatherName.Trim() : "")} {(!string.IsNullOrEmpty(this.LastName) ? this.LastName.Trim() : "")}";


    }
}
