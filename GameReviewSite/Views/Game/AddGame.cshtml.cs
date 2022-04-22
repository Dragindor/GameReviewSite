// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;
using GameReviewSite.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;

namespace GameReviewSite.Views.Game
{
    public class AddGameModel : PageModel
    {
        private readonly IGameService gameService;
        private readonly ApplicationDbContext data;
        public AddGameModel(IGameService _gameService,
            ApplicationDbContext _data)
        {
            gameService = _gameService;
            data = _data;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "GamePicture")]
            public string GamePicture { get; set; }

            [Required]
            [StringLength(1000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [Display(Name = "Description")]
            public string Description { get; set; }
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(999, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
            [Display(Name = "Price")]
            public double Price { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [Display(Name = "Developer")]
            public string Developer { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [Display(Name = "publisher")]
            public string Publisher { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [Display(Name = "ReleaseDate")]
            public string ReleaseDate { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [Display(Name = "SystemRequirements")]            
            public string SystemRequirements { get; set; }

            [Display(Name = "Tags")]
            public ICollection<Tag> Tags { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var Game = new AddGameViewModel
                {
                    Name=Input.Name,
                    GamePicture=Input.GamePicture,
                    Price=Input.Price,
                    Description = Input.Description,
                    Developer = Input.Developer,
                    Publisher = Input.Publisher,
                    ReleaseDate = Input.ReleaseDate,
                    SystemRequirements = Input.SystemRequirements,
                    Tags = Input.Tags
                    
                };
                var result = gameService.CreateGame(Game);
                if (result.IsCompleted)
                {
                    await data.AddAsync(Game);
                    await data.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }       
    }
}
