using System;
using System.Reflection.Metadata;
using System.Text;

namespace Task_MethodOverloading
{
    public class Person
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public int Age { get; private set; }
        public decimal Weight { get; private set; }
        public decimal Height { get; private set; }

        public override string ToString()
        {
            return $"Full name: {Name} {Surname}\n" +
                $"Age: {Age}\n" +
                $"Weight: {Weight} kg\n" +
                $"Height: {Height} cm";
        }

        public Person()
        { 
            Name = "[undefined name]";
            Surname = "[undefined surname]";
            Age = default(int);
            Weight = default(decimal);
            Height = default(decimal);
        }

        public Person(string name, string surname, int age, decimal weight, decimal height)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Weight = weight;
            Height = height;
        }
    }

    public abstract class Card
    {
        private string cardNumber;
        private string cvvCode;
        private decimal balance;

        public string CardNumber
        {
            get
            {
                return cardNumber;
            }

            protected set
            {
                if (value.Length < 16) throw new ArgumentException();
                else if (value.Length == 16)
                {
                    bool success = Int64.TryParse(value, out long result);
                    if (!success) throw new ArgumentException();

                    cardNumber = value;
                }

                else
                {
                    string[] numberParts = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (numberParts.Length != 4) throw new ArgumentException();

                    bool success; StringBuilder sb = new StringBuilder();

                    for (int i = 0; i < 4; i++)
                    {
                        success = Int32.TryParse(numberParts[i], out int param);
                        if (!success || numberParts[i].Length != 4) throw new ArgumentException();

                        sb.Append(numberParts[i]);
                    }

                    cardNumber = sb.ToString();
                }
            }
        }
        public string CVVCode
        {
            get { return cvvCode; }
            protected set
            {
                if (value.Length != 3) throw new ArgumentException();

                else if (!Int32.TryParse(value, out int param)) throw new ArgumentException();

                else cvvCode = value;
            }
        }
        public decimal Balance
        {
            get { return balance; }
            protected set
            {
                if (value < 0) throw new ArgumentException();

                balance = value;
            }
        }

        public Card() { }

        public Card(string _cardNumber, string _cvvCode, decimal _balance)
        {
            this.CardNumber = _cardNumber;
            this.CVVCode = _cvvCode;
            this.Balance = _balance;
        }

        public abstract void Deposit(decimal amount);
        public abstract void Withdraw(decimal amount);

        public override string ToString()
        {
            return $"Card Number: {CardNumber}\n" +
                $"CVV Code: {CVVCode}\n" +
                $"Balance: {Balance}";
        }
    }

    public class Unibank : Card
    {
        private const decimal WithdrawComission = 1.5m;
        private const decimal DepositComission = 0m;

        public Unibank() : base() { }
        public Unibank(string _cardNumber, string _cvvCode, decimal _balance) : base(_cardNumber, _cvvCode, _balance) { }
        
        public override void Deposit(decimal amount)
        {
            if (amount < 0) throw new ArgumentException(
                $"Invalid amount value entered -> amount:{amount} cannot be less than zor equal to zero.");

            Balance += amount * (1 - DepositComission / 100m);
        }
        public override void Withdraw(decimal amount)
        {
            if (amount < 0) throw new ArgumentException(
                $"Invalid amount value entered -> amount:{amount} cannot be less than zor equal to zero.");

            else if (Balance < amount * (1 + WithdrawComission))
            {
                Console.WriteLine($"Cannot withdraw an amount: {amount}" +
                    $"with the withdrawal commision percentage: {WithdrawComission} from balance: {Balance}");
                return;
            }

            Balance -= amount * (1 + WithdrawComission / 100m);
        }
    }

    public class AccessBank : Card
    {
        private const decimal WithdrawComission = 1.6m;
        private const decimal DepositComission = 0.3m;

        public AccessBank() : base() { }
        public AccessBank(string _cardNumber, string _cvvCode, decimal _balance) : base(_cardNumber, _cvvCode, _balance) { }

        public override void Deposit(decimal amount)
        {
            if (amount < 0) throw new ArgumentException(
                $"Invalid amount value entered -> amount:{amount} cannot be less than zor equal to zero.");

            Balance += amount * (1 - DepositComission / 100m);
        }
        public override void Withdraw(decimal amount)
        {
            if (amount < 0) throw new ArgumentException(
                $"Invalid amount value entered -> amount:{amount} cannot be less than zor equal to zero.");

            else if (Balance < amount * (1 + WithdrawComission))
            {
                Console.WriteLine($"Cannot withdraw an amount: {amount}" +
                    $"with the withdrawal commision percentage: {WithdrawComission} from balance: {Balance}");
                return;
            }

            Balance -= amount * (1 + WithdrawComission / 100m);
        }
    }

    public class PashaBank : Card
    {
        private const decimal WithdrawComission = 1.1m;
        private const decimal DepositComission = 0.6m;

        public PashaBank() : base() { }
        public PashaBank(string _cardNumber, string _cvvCode, decimal _balance) : base(_cardNumber, _cvvCode, _balance) { }

        public override void Deposit(decimal amount)
        {
            if (amount < 0) throw new ArgumentException(
                $"Invalid amount value entered -> amount:{amount} cannot be less than zor equal to zero.");

            Balance += amount * (1 - DepositComission / 100m);
        }
        public override void Withdraw(decimal amount)
        {
            if (amount < 0) throw new ArgumentException(
                $"Invalid amount value entered -> amount:{amount} cannot be less than zor equal to zero.");

            else if (Balance < amount * (1 + WithdrawComission))
            {
                Console.WriteLine($"Cannot withdraw an amount: {amount}" +
                    $"with the withdrawal commision percentage: {WithdrawComission} from balance: {Balance}");
                return;
            }

            Balance -= amount * (1 + WithdrawComission / 100m);
        }
    }

    public class LeoBank : Card
    {
        private const decimal WithdrawComission = 0m;
        private const decimal DepositComission = 0m;

        public LeoBank() : base() { }
        public LeoBank(string _cardNumber, string _cvvCode, decimal _balance) : base(_cardNumber, _cvvCode, _balance) { }

        public override void Deposit(decimal amount)
        {
            if (amount < 0) throw new ArgumentException(
                $"Invalid amount value entered -> amount:{amount} cannot be less than zor equal to zero.");

            Balance += amount * (1 - DepositComission / 100m);
        }
        public override void Withdraw(decimal amount)
        {
            if (amount < 0) throw new ArgumentException(
                $"Invalid amount value entered -> amount:{amount} cannot be less than zor equal to zero.");

            else if (Balance < amount * (1 + WithdrawComission))
            {
                Console.WriteLine($"Cannot withdraw an amount: {amount}" +
                    $"with the withdrawal commision percentage: {WithdrawComission} from balance: {Balance}");
                return;
            }

            Balance -= amount * (1 + WithdrawComission / 100m);
        }
    }

    public class MathHelper
    {
        public static int PrintValue(int x, int y)
        {
            return x + y;
        }

        public static int PrintValue(bool parameter)
        {
            return (parameter) ? 1 : 0;
        }

        public static void PrintValue(string s1, string s2, bool newLine = true)
        {
            string newline = (newLine) ? "\n" : null;
            Console.Write(s1 + s2 + newline);
        }

        public static int PrintValue(int a, int b, int c)
        {
            return a + b + c;
        }
    }

    internal class Program
    {
        static void Task1_Person()
        {
            Person pr = new Person(
                name: "Aliheydar",
                surname: "Heydarov",
                age: 21,
                weight: 67,
                height: 175);

            Console.WriteLine(pr);
            // pr.Name = "Fehruz"; Compile error
        }

        static void Task2_Card()
        {
            Unibank uc = new Unibank(
                _cardNumber: "4127 3722 1233 3221",
                _cvvCode: "422",
                _balance: 510m);

            Console.Write("Unibank card details:");
            Console.WriteLine(uc);
            Console.WriteLine();

            AccessBank ac = new AccessBank(
                _cardNumber: "4012 3381 3123 3142",
                _cvvCode: "844",
                _balance: 450m);

            Console.Write("AccessBank card details:");
            Console.WriteLine(ac);
            Console.WriteLine();

            PashaBank pc = new PashaBank(
                _cardNumber: "4012 3341 3323 7142",
                _cvvCode: "501",
                _balance: 440m);

            Console.Write("PashaBank card details:");
            Console.WriteLine(pc);
            Console.WriteLine();

            LeoBank lc = new LeoBank(
                _cardNumber: "4522 7781 6723 2192",
                _cvvCode: "844",
                _balance: 450m);

            Console.Write("LeoBank card details:");
            Console.WriteLine(ac);
            Console.WriteLine();
        }

        static void Task3_Math()
        {
            // 1:
            Console.WriteLine(MathHelper.PrintValue(5, 7));

            // 2:
            Console.WriteLine(MathHelper.PrintValue(true));

            // 3:
            MathHelper.PrintValue("Ali", "heydar", newLine: true);

            // 4:
            Console.WriteLine(MathHelper.PrintValue(1, 2, 3));
        }

        static void Main(string[] args)
        {
            Task2_Card();
        }
    }
}
