﻿using System.ComponentModel.DataAnnotations;
using UserAppService.Models;

namespace UserAppService.ViewModels
{
    public class SelectRoleEditorViewModel
    {
        public SelectRoleEditorViewModel() { }

        // Update this to accept an argument of type ApplicationRole:
        public SelectRoleEditorViewModel(ApplicationRole role)
        {
            this.RoleName = role.Name;

            // Assign the new Descrption property:
        }

        public bool Selected { get; set; }

        [Required]
        public string RoleName { get; set; }

        // Add the new Description property:
        public string Description { get; set; }
    }

    public class EditRoleViewModel
    {
        public string OriginalRoleName { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

        public EditRoleViewModel() { }
        public EditRoleViewModel(ApplicationRole role)
        {
            this.OriginalRoleName = role.Name;
            this.RoleName = role.Name;
        }
    }
}