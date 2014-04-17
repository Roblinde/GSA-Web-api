using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GSA.Search.WebApi.Models
{
    /// <summary>
    /// Defines a user that could be persisted to the Iokr database
    /// </summary>
    public class SimpleUser
    {
        /// <summary>
        /// The firstname of the user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The lastname of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The UserName of this users boss.
        /// </summary>
        public string BossId { get; set; }

        /// <summary>
        /// The email of the user
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// The title of the user as specified in the ActiveDirectory
        /// </summary>
        public string AdTitle { get; set; }

        /// <summary>
        /// The users title as set by the user
        /// </summary>
        public string OwnTitle { get; set; }

        /// <summary>
        /// The seat of the user
        /// </summary>
        public string Seat { get; set; }

        /// <summary>
        /// The UserName as set in the AD
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// The users phone number
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The users mobile phone number
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// The users office
        /// </summary>
        public string Office { get; set; }

        /// <summary>
        /// The users company
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// The personal information as specified by the user
        /// </summary>
        public string PersonalInfo { get; set; }

        /// <summary>
        /// Whether the user is active or not
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// The cost centre of the user
        /// </summary>
        public int CostCentre { get; set; }
    }
}