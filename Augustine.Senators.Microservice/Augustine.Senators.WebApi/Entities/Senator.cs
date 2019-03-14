using System;

namespace Augustine.Senators.WebApi.Entities
{
    public class Senator
    {
        //Name, District, Political Party, Term Start Date, Term End Date
        public int Id { get; set; }
        public string Name { get; set; }
        public string District { get; set; }
        public string Party { get; set; }
        public DateTime? TermStartDate{ get; set; }
        public DateTime? TermEndDate { get; set; }
    }
}