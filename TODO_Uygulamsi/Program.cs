using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Dictionary<int, string> teamMembers = new Dictionary<int, string>()
    {
        { 1, "Ali" },
        { 2, "Ayşe" },
        { 3, "Mehmet" }
    };

    static List<Card> todo = new List<Card>();
    static List<Card> inProgress = new List<Card>();
    static List<Card> done = new List<Card>();

    static void Main()
    {
        // Varsayılan Kartlar
        todo.Add(new Card("Görev 1", "İçerik 1", 1, "M"));
        inProgress.Add(new Card("Görev 2", "İçerik 2", 2, "L"));
        done.Add(new Card("Görev 3", "İçerik 3", 3, "S"));

        while (true)
        {
            Console.WriteLine("\nYapmak istediğiniz işlemi seçiniz:");
            Console.WriteLine("1. Kart Ekle");
            Console.WriteLine("2. Kart Güncelle");
            Console.WriteLine("3. Kart Sil");
            Console.WriteLine("4. Kart Taşı");
            Console.WriteLine("5. Board Listeleme");
            Console.WriteLine("0. Çıkış");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddCard();
                    break;
                case 2:
                    UpdateCard();
                    break;
                case 3:
                    DeleteCard();
                    break;
                case 4:
                    MoveCard();
                    break;
                case 5:
                    ListBoard();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim, lütfen tekrar deneyin.");
                    break;
            }
        }
    }

    static void AddCard()
    {
        Console.WriteLine("Başlık: ");
        string title = Console.ReadLine();

        Console.WriteLine("İçerik: ");
        string content = Console.ReadLine();

        Console.WriteLine("Atanacak kişi ID (1: Ali, 2: Ayşe, 3: Mehmet): ");
        int personId = Convert.ToInt32(Console.ReadLine());

        if (!teamMembers.ContainsKey(personId))
        {
            Console.WriteLine("Geçersiz kişi ID.");
            return;
        }

        Console.WriteLine("Büyüklük (XS, S, M, L, XL): ");
        string size = Console.ReadLine();

        todo.Add(new Card(title, content, personId, size));
    }

    static void UpdateCard()
    {
        Console.WriteLine("Güncellenecek kartın başlığını giriniz: ");
        string title = Console.ReadLine();

        Card card = FindCard(title);
        if (card == null)
        {
            Console.WriteLine("Kart bulunamadı.");
            return;
        }

        Console.WriteLine("Yeni Başlık: ");
        card.Title = Console.ReadLine();

        Console.WriteLine("Yeni İçerik: ");
        card.Content = Console.ReadLine();

        Console.WriteLine("Yeni Atanacak kişi ID (1: Ali, 2: Ayşe, 3: Mehmet): ");
        int personId = Convert.ToInt32(Console.ReadLine());

        if (!teamMembers.ContainsKey(personId))
        {
            Console.WriteLine("Geçersiz kişi ID.");
            return;
        }
        card.AssignedPersonId = personId;

        Console.WriteLine("Yeni Büyüklük (XS, S, M, L, XL): ");
        card.Size = Console.ReadLine();
    }

    static void DeleteCard()
    {
        Console.WriteLine("Silinecek kartın başlığını giriniz: ");
        string title = Console.ReadLine();

        Card card = FindCard(title);
        if (card == null)
        {
            Console.WriteLine("Kart bulunamadı.");
            return;
        }

        if (todo.Contains(card))
        {
            todo.Remove(card);
        }
        else if (inProgress.Contains(card))
        {
            inProgress.Remove(card);
        }
        else if (done.Contains(card))
        {
            done.Remove(card);
        }

        Console.WriteLine("Kart silindi.");
    }

    static void MoveCard()
    {
        Console.WriteLine("Taşınacak kartın başlığını giriniz: ");
        string title = Console.ReadLine();

        Card card = FindCard(title);
        if (card == null)
        {
            Console.WriteLine("Kart bulunamadı.");
            return;
        }

        Console.WriteLine("Taşınacak yeni konumu seçiniz (1: TODO, 2: IN PROGRESS, 3: DONE): ");
        int newLocation = Convert.ToInt32(Console.ReadLine());

        if (todo.Contains(card))
        {
            todo.Remove(card);
        }
        else if (inProgress.Contains(card))
        {
            inProgress.Remove(card);
        }
        else if (done.Contains(card))
        {
            done.Remove(card);
        }

        switch (newLocation)
        {
            case 1:
                todo.Add(card);
                break;
            case 2:
                inProgress.Add(card);
                break;
            case 3:
                done.Add(card);
                break;
            default:
                Console.WriteLine("Geçersiz konum.");
                break;
        }
    }

    static void ListBoard()
    {
        Console.WriteLine("\nTODO Line:");
        foreach (var card in todo)
        {
            PrintCard(card);
        }

        Console.WriteLine("\nIN PROGRESS Line:");
        foreach (var card in inProgress)
        {
            PrintCard(card);
        }

        Console.WriteLine("\nDONE Line:");
        foreach (var card in done)
        {
            PrintCard(card);
        }
    }

    static void PrintCard(Card card)
    {
        Console.WriteLine($"Başlık: {card.Title}");
        Console.WriteLine($"İçerik: {card.Content}");
        Console.WriteLine($"Atanan Kişi: {teamMembers[card.AssignedPersonId]}");
        Console.WriteLine($"Büyüklük: {card.Size}");
        Console.WriteLine("---------------------------");
    }

    static Card FindCard(string title)
    {
        return todo.FirstOrDefault(c => c.Title == title) ??
               inProgress.FirstOrDefault(c => c.Title == title) ??
               done.FirstOrDefault(c => c.Title == title);
    }
}

public class Card
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int AssignedPersonId { get; set; }
    public string Size { get; set; }

    public Card(string title, string content, int assignedPersonId, string size)
    {
        Title = title;
        Content = content;
        AssignedPersonId = assignedPersonId;
        Size = size;
    }
}


