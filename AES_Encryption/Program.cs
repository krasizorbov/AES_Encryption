using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AES_Encryption;

ExampleDbContext db = new();

Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
{
    return assembly
        .GetTypes()
        .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
        .ToArray();
}

Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "AES_Encryption");

List<Type> enumList = new List<Type>();

for (int i = 0; i < typelist.Length; i++)
{
    if (typelist[i].Name.StartsWith("Enum"))
    {
        var current = typelist[i].Name.Substring(4);

        for (int j = 0; j < typelist.Length; j++)
        {
            if (current == typelist[j].Name)
            {
                enumList.Add(typelist[i]);
            }
        }
    }
}


var enumFields = enumList[0].GetFields();

//for (int i = 0; i < enumFields.Length; i++)
//{
//    Console.WriteLine(enumFields[i].Name);
//}



//User newUser = new()
//{
//    FirstName = "Krasi",
//    LastName = "Zorbov",
//    Email = "krasizorbov@abv.bg",
//    IdentityNumber = "1234567890",
//    PetName =  "Michi"
//};
//db.Users.Add(newUser);
//db.SaveChanges();
//Console.WriteLine("user added");
//Console.WriteLine("List all users");

//List<User> users = db.Users.ToList();
//foreach (User user in users)
//{
//    Console.WriteLine(user.ID);
//    Console.WriteLine(user.FirstName);
//    Console.WriteLine(user.LastName);
//    Console.WriteLine(user.Email);
//    Console.WriteLine(user.IdentityNumber);
//    Console.WriteLine(user.PetName);
//}


