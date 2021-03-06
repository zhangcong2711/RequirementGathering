﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using RequirementGathering.Attributes;
using RequirementGathering.Reousrces;

namespace RequirementGathering.Models
{
    public class Evaluation
    {
        public int Id { get; set; }

        [Display(Name = "VersionDisplay", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldRequired")]
        public string Version { get; set; }

        [Display(Name = "DescriptionDisplay", ResourceType = typeof(Resources))]
        [Translatable]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Display(Name = "StepsDisplay", ResourceType = typeof(Resources))]
        public int Steps { get; set; }

        [Display(Name = "IsActiveDisplay", ResourceType = typeof(Resources))]
        public bool IsActive { get; set; }

        public Evaluation()
        {
            IsActive = true;
        }

        #region Navigation Fields

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldRequired")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public virtual ICollection<Attribute> Attributes { get; set; }
        public virtual ICollection<EvaluationUser> EvaluationUsers { get; set; }

        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }

        #endregion

        public ICollection<User> Users()
        {
            return EvaluationUsers.Select(eu => eu.User)
                                  .ToList();
        }
    }
}
