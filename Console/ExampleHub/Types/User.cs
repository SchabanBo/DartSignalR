namespace ConsoleApp.ExampleHub.Types {
    public class User {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<Address> Addresses { get; set; }
    }

    public class AddUser {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<Address> Addresses { get; set; }
    }

    public class UpdateUser {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public List<Address> Addresses { get; set; }
    }

    public class DeleteUser {
        public int Id { get; set; }
    }

    public class Address {
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
    }
}
