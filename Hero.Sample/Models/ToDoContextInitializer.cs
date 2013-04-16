using System.Data.Entity;

namespace Hero.Sample.Models
{
    public class ToDoContextInitializer : CreateDatabaseIfNotExists<ToDoContext>
    {
        protected override void Seed(ToDoContext context)
        {
            ToDo todo1 = new ToDo
                {
                    Name = "Todo1",
                    UserName = "todoadminuser",
                    Description = "Todo1 Description"
                };

            ToDo todo2 = new ToDo
                {
                    Name = "Todo2",
                    UserName = "todoadminuser",
                    Description = "Todo2 Description"
                };

            ToDo todo3 = new ToDo
                {
                    Name = "TodoBasic1",
                    UserName = "todobasicuser",
                    Description = "TodoBasic1 Description"
                };

            context.Items.Add(todo1);
            context.Items.Add(todo2);
            context.Items.Add(todo3);
        }
    }
}