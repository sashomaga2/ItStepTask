using ItStepTask.Entity;
using ItStepTask.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Services
{
    public interface IShopService
    {
        int GetSelectedCategory();
        IEnumerable<Item> GetItems(int categoryId);
        int GetShoppingCartItemsCount(string userId);
        void SetUserSelectedCategory(string userId, int categoryId);
    }
    public class ShopService : IShopService
    {
        private readonly ICategoryService categoryService;
        private readonly IUserDataService userDataService;
        private readonly IUsersService userService;
        private readonly IShoppingCartService shoppingCardService;
        private readonly IItemsService itemsService;

        public ShopService( ICategoryService categoryService, 
                            IUserDataService userDataService, 
                            IUsersService userService, 
                            IShoppingCartService shoppingCardService,
                            IItemsService itemsService)
        {
            this.categoryService = categoryService;
            this.userDataService = userDataService;
            this.userService = userService;
            this.shoppingCardService = shoppingCardService;
            this.itemsService = itemsService;
        }
        public IEnumerable<Item> GetItems(int categoryId)
        {
            return itemsService.GetAll().Where(i => i.Category.Id == categoryId).ToArray();
        }

        public int GetSelectedCategory()
        {
            


            var firstCategoryId = categoryService.GetAll().First().Id;

            return 0;

            //if (userId == null)
            //{
            //    return firstCategoryId;
            //}

            //var userData = userDataService.GetAll().FirstOrDefault(d => d.User.Id == userId);

            //if(userData == null)
            //{
            //    SetUserSelectedCategory(userId, firstCategoryId);
            //    return firstCategoryId;
            //}

            //return userData.SelectedCategory.Id;
        }

        public void SetUserSelectedCategory(string userId, int categoryId)
        {
            if(userId == null)
            {
                throw new ArgumentException("UserId cant be null");    
            }

            var user = userService.Find(userId);
            var category = categoryService.Find(categoryId);
            
            if(user == null || category == null)
            {
                throw new InvalidOperationException("User or Category does not exists!");
            }

            userDataService.Add(new UserData { SelectedCategory = category, User = user });

        }

        public int GetShoppingCartItemsCount(string userId)
        {
            if(userId == null)
            {
                return 0;
            }

            var data = shoppingCardService.GetAll().Where(d => d.User.Id == userId).ToList();

            return data.Count;
        }
    }
}
