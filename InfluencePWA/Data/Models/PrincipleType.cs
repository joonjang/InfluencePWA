using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InfluencePWA.Data.Models
{
    public class PrincipleType
    {
        #region Constructor
        public PrincipleType()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// The unique id and primary key for this Country
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// PrincipleType name
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region Client-side properties
        /// <summary>
        /// The number of Principles related to this PrincipleType.
        /// </summary>
        [NotMapped]
        public int TotPrinciple
        {
            get
            {
                return (Principles != null) ? Principles.Count : _TotPrinciples;
            }
            set { _TotPrinciples = value; }
        }

        private int _TotPrinciples = 0;
        #endregion

        #region Navigation Properties
        /// <summary>
        /// A list containing all the principles related to this type.
        /// </summary>
        [JsonIgnore]
        public virtual List<Principle> Principles { get; set; }
        #endregion
    }
}
