using FluentNHibernate.Conventions.Inspections;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstApp.Models.Entities
{
    public class Edge
    {
        public Guid Id { get; set; }

        public Guid StartNodeId { get; set; }
        public Guid EndNodeId { get; set; }

        [ForeignKey("StartNodeId")]
        public Node StartNode { get; set; }

        [ForeignKey("EndNodeId")]
        public Node EndNode { get; set; }
        public double EdgeCost { get; set; }
        public List<Node> IntermediateNodes { get; set; } = new List<Node>();
        [NotMapped]
        public List<string> Tag { get; set; } = new List<string>();
    }
}
