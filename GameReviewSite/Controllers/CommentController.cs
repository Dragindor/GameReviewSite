using GameReviewSite.Core.Constants;
using GameReviewSite.Core.Contracts;
using GameReviewSite.Infrastructure.Data;
using GameReviewSite.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Security.Claims;

namespace GameReviewSite.Controllers
{
    public class CommentController : BaseController
    {
        private readonly IReviewService reviewService;
        private readonly ICommentService commentService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentController(IReviewService reviewService, 
            IHttpContextAccessor httpContextAccessor, 
            ICommentService commentService,
            UserManager<ApplicationUser> userManager)
        {
            this.reviewService = reviewService;
            this.commentService = commentService;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> AllComments(string id)
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Review = await reviewService.GetReviewById(id);
            mymodel.Comment = await commentService.GetCommentsByReview(id);

            return View(mymodel);
        }

        [HttpPost]
        public async Task<IActionResult> AllComments(Comment model)
        {
            ClaimsPrincipal? userContext = httpContextAccessor.HttpContext?.User;
            ApplicationUser? user = await userManager.GetUserAsync(userContext);

            model.UserId = user.Id;

            ViewBag.Description = model.Description;

            if (await commentService.AddCommentToReview(model))
            {
                ViewData[MessageConstants.SuccessMessage] = "Успешен запис!";
                return RedirectToAction(nameof(AllComments), new { model.ReviewId });
            }
            else
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
            }

            return RedirectToAction(nameof(AllComments), new { model.ReviewId });
        }
    }
}
