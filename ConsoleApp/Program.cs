
using Domain.Models;
using Infrastructure.Repositories;

var categoryRepo = new CategoryRepository();
var productRepo = new ProductRepository();

while (true)
{
    Console.WriteLine("===============================");
    Console.WriteLine(@"1 - Show all categories.
2 - Show category.
3 - Add new category.
4 - Update category.
5 - Delete category.
6 - Show all products.
7 - Show product.
8 - Add new product.
9 - Update product.
10 - Delete product.
11 - Show products by category.
12 - Amount of products.
13 - Amount of categories.
14 - Amount of products by category.
15 - Minimum price by category.
16 - Maximum price by category.
17 - Average price by category.
18 - Total price of products.
0 - Exit.");

    Console.Write("Choose: ");
    var a = Console.ReadLine();

    Console.WriteLine("===============================");

    switch (a)
    {
        case "1":
            var allCategories = categoryRepo.GetCategories();
            foreach (var item in allCategories)
            {
                Console.WriteLine(item.Id + " " + item.Name);
            }
            break;

        case "2":
            Console.Write("Enter id of category: ");
            var categoryIdToShow = Convert.ToInt32(Console.ReadLine());
            var categoryToShow = categoryRepo.GetCategory(categoryIdToShow);
            Console.WriteLine(categoryToShow.Id + " " + categoryToShow.Name);
            break;

        case "3":
            Console.Write("Enter name: ");
            var categoryName = Console.ReadLine()!;
            categoryRepo.AddCategory(new Category { Name = categoryName });
            break;

        case "4":
            Console.Write("Enter id: ");
            var categoryIdToUpdate = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter new name: ");
            var newCategoryName = Console.ReadLine()!;
            categoryRepo.UpdateCategory(new Category
            {
                Id = categoryIdToUpdate,
                Name = newCategoryName
            });
            break;

        case "5":
            Console.Write("Enter id: ");
            var categoryIdToDelete = Convert.ToInt32(Console.ReadLine());
            categoryRepo.DeleteCategory(categoryIdToDelete);
            break;

        case "6":
            var allProducts = productRepo.GetProducts();
            foreach (var item in allProducts)
            {
                Console.WriteLine(item.Id + " " + item.Name + " " + item.Price + " " + item.CategoryId);
            }
            break;

        case "7":
            Console.Write("Enter product id: ");
            var productIdToShow = Convert.ToInt32(Console.ReadLine());
            var productToShow = productRepo.GetProduct(productIdToShow);
            Console.WriteLine(productToShow.Id + " " + productToShow.Name + " " + productToShow.Price + " " + productToShow.CategoryId);
            break;

        case "8":
            Console.Write("Enter name: ");
            var productName = Console.ReadLine()!;
            Console.Write("Enter price: ");
            var productPrice = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter category id: ");
            var productCategoryId = Convert.ToInt32(Console.ReadLine());

            productRepo.AddProduct(new Product
            {
                Name = productName,
                Price = productPrice,
                CategoryId = productCategoryId
            });
            break;

        case "9":
            Console.Write("Enter product id: ");
            var productIdToUpdate = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter new name: ");
            var updatedProductName = Console.ReadLine()!;
            Console.Write("Enter new price: ");
            var updatedProductPrice = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter new category id: ");
            var updatedCategoryId = Convert.ToInt32(Console.ReadLine());

            productRepo.UpdateProduct(new Product
            {
                Id = productIdToUpdate,
                Name = updatedProductName,
                Price = updatedProductPrice,
                CategoryId = updatedCategoryId
            });
            break;

        case "10":
            Console.Write("Enter product id: ");
            var productIdToDelete = Convert.ToInt32(Console.ReadLine());
            productRepo.DeleteProduct(productIdToDelete);
            break;

        case "11":
            Console.Write("Enter category id: ");
            var searchCategoryId = Convert.ToInt32(Console.ReadLine());
            var productsByCategory = productRepo.GetProductsByCategory(searchCategoryId);
            foreach (var item in productsByCategory)
            {
                Console.WriteLine(item.Id + " " + item.Name + " " + item.Price);
            }
            break;

        case "12":
            Console.WriteLine("Amount of products: " + productRepo.GetAmountOfProducts());
            break;

        case "13":
            Console.WriteLine("Amount of categories: " + productRepo.GetAmountOfCategories());
            break;

        case "14":
            Console.Write("Enter category id: ");
            var countCategoryId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Amount: " + productRepo.GetAmountOfProductsByCategory(countCategoryId));
            break;

        case "15":
            Console.Write("Enter category id: ");
            var minCategoryId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Minimum price: " + productRepo.GetMinPriceOfProductByCategory(minCategoryId));
            break;

        case "16":
            Console.Write("Enter category id: ");
            var maxCategoryId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Maximum price: " + productRepo.GetMaxPriceOfProductByCategory(maxCategoryId));
            break;

        case "17":
            Console.Write("Enter category id: ");
            var avgCategoryId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Average price: " + productRepo.GetAveragePriceOfProductByCategory(avgCategoryId));
            break;

   case "18":
    Console.WriteLine("Total price of products: " + productRepo.GetTotalPriceOfProducts());
    break;

        case "0":
            Console.WriteLine("Exiting...");
            return;
    }
}
