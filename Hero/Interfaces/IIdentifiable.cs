namespace Hero.Interfaces
{
    interface IIdentifiable<out T>
    {
        T Id { get;}
    }
}