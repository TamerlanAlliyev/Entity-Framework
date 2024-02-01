using ConsoleApp1.Models;
using ConsoleApp1.ShopContext;



ShopContext ShopContext = new ShopContext();

Menu();
int request = int.Parse(Console.ReadLine());

while (request != 0)
{
    switch (request)
    {
        case 1:
            Create();
            break;
        case 2:
            GetAll();
            break;
        case 3:
            GetById();
            break;
        case 4:
            Update();
            break;
        case 5:
            Delete();
            break;
        default:
            break;
    }
    Menu();
    request = int.Parse(Console.ReadLine());
}







void Menu()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Add a request");

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("1.Create Category");
    Console.WriteLine("2.Get All Category");
    Console.WriteLine("3.Get By Id Category");
    Console.WriteLine("4.Update Category");
    Console.WriteLine("5.Delete Category");
    Console.WriteLine("0.Close");
    Console.ResetColor();
}






void GetAll()
{
    List<Category> categories = ShopContext.Category.Where(ct => ct.IsDeleted == false).ToList();

    foreach (var category in categories)
    {
        Console.WriteLine($"Id: {category.Id} , Name: {category.Name} , IsDeleted: {category.IsDeleted}");
    }
}



void GetById()
{
    Console.WriteLine("Add to Id");
    int.TryParse(Console.ReadLine(), out int id);
    Category category = ShopContext.Category.Where(ct => ct.Id == id && ct.IsDeleted == false).FirstOrDefault();

    if (category != null)
    {
        Console.WriteLine($"Id: {category.Id}, Name: {category.Name}, IsDeleted: {category.IsDeleted}");
    }
    else
    {
        Console.WriteLine("Category not found");
    }
}

void Create()
{
    Category category = new Category();

    Console.WriteLine("Add Name");
    string name = Console.ReadLine();

    if (string.IsNullOrEmpty(name))
    {
        Console.WriteLine("Category name cannot be empty. Operation canceled.");
        return; 
    }

    category.Name = name;
    category.IsDeleted = false;

    ShopContext.Category.Add(category);
    ShopContext.SaveChanges();

    Console.WriteLine("Category created successfully.");
}

void Delete()
{
    Console.WriteLine("Delete Id");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        Category categoryToDelete = ShopContext.Category.FirstOrDefault(ct => ct.Id == id);

        if (categoryToDelete != null)
        {
            categoryToDelete.IsDeleted = true;

            if (ShopContext.ChangeTracker.HasChanges())
            {
                ShopContext.SaveChanges();
                Console.WriteLine("Category deleted successfully.");
            }
            else
            {
                Console.WriteLine("No changes to save. Deletion failed.");
            }
        }
        else
        {
            Console.WriteLine("Category not found. Deletion failed.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input for Id. Deletion failed.");
    }
}


void Update()
{

    Console.WriteLine("Select the id of the category you want to change");
    int.TryParse(Console.ReadLine(), out int id);

    Console.WriteLine("Select a new category");
    string categoryName = Console.ReadLine();

    Category category = ShopContext.Category.Where(ct => ct.Id == id && ct.IsDeleted == false).FirstOrDefault();

    category.Name = categoryName;
    ShopContext.SaveChanges();

}