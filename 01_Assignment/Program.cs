using _01_Assignment_.Services;

IMenuManager menu = new MenuManager();

do // gör något minst en gång och gör det sedan igen tills tillståndet inte är sann
{
    Console.Clear();

    menu.ViewMenuOptions();
    Console.ReadKey();

}while (true);

