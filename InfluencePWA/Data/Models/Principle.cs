using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfluencePWA.Data.Models
{
    
    public class Principle
    {
        #region Constructor
        public Principle()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// The unique id and primary key for the principles
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Law count
        /// </summary>
        public string Law { get; set; }

        /// <summary>
        /// Law title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Law description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// PrincipleType of principle (foreign key)
        /// </summary>
        [ForeignKey("PrincipleType")]
        public int PrincipleTypeId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// The typer related to the principle.
        /// </summary>
        public virtual PrincipleType PrincipleType { get; set; }
        #endregion
    }



}
