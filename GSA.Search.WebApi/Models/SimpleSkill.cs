using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSA.Search.WebApi.Models
{
    /// <summary>
    /// Defines a skill that could be persisted to the Iokr database
    /// </summary>
    public class SimpleSkill
    {
        public int SkillId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}