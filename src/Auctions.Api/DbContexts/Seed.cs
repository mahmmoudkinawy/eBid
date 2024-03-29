﻿namespace Auctions.Api.DbContexts;
public static class Seed
{
    public static async Task SeedAsync(AuctionsDbContext context)
    {
        ArgumentNullException.ThrowIfNull(nameof(SeedAsync));

        if (await context.Auctions.AnyAsync())
        {
            Console.WriteLine("Auctions already exist - no need to seed");
            return;
        }

        var auctions = new List<AuctionEntity>
        {
            // 1 Ford GT
            new AuctionEntity
            {
                Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
                Status = StatusEnum.Live,
                ReservePrice = 20000,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(10),
                Item = new ItemEntity
                {
                    Make = "Ford",
                    Model = "GT",
                    Color = "White",
                    Mileage = 50000,
                    Year = 2020,
                    ImageUrl = "https://cdn.pixabay.com/photo/2016/05/06/16/32/car-1376190_960_720.jpg"
                }
            },
            // 2 Bugatti Veyron
            new AuctionEntity
            {
                Id = Guid.Parse("c8c3ec17-01bf-49db-82aa-1ef80b833a9f"),
                Status = StatusEnum.Live,
                ReservePrice = 90000,
                Seller = "alice",
                AuctionEnd = DateTime.UtcNow.AddDays(60),
                Item = new ItemEntity
                {
                    Make = "Bugatti",
                    Model = "Veyron",
                    Color = "Black",
                    Mileage = 15035,
                    Year = 2018,
                    ImageUrl = "https://cdn.pixabay.com/photo/2012/05/29/00/43/car-49278_960_720.jpg"
                }
            },
            // 3 Ford mustang
            new AuctionEntity
            {
                Id = Guid.Parse("bbab4d5a-8565-48b1-9450-5ac2a5c4a654"),
                Status = StatusEnum.Live,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(4),
                Item = new ItemEntity
                {
                    Make = "Ford",
                    Model = "Mustang",
                    Color = "Black",
                    Mileage = 65125,
                    Year = 2023,
                    ImageUrl = "https://cdn.pixabay.com/photo/2012/11/02/13/02/car-63930_960_720.jpg"
                }
            },
            // 4 Mercedes SLK
            new AuctionEntity
            {
                Id = Guid.Parse("155225c1-4448-4066-9886-6786536e05ea"),
                Status = StatusEnum.ReserveNotMet,
                ReservePrice = 50000,
                Seller = "tom",
                AuctionEnd = DateTime.UtcNow.AddDays(-10),
                Item = new ItemEntity
                {
                    Make = "Mercedes",
                    Model = "SLK",
                    Color = "Silver",
                    Mileage = 15001,
                    Year = 2020,
                    ImageUrl = "https://cdn.pixabay.com/photo/2016/04/17/22/10/mercedes-benz-1335674_960_720.png"
                }
            },
            // 5 BMW X1
            new AuctionEntity
            {
                Id = Guid.Parse("466e4744-4dc5-4987-aae0-b621acfc5e39"),
                Status = StatusEnum.Live,
                ReservePrice = 20000,
                Seller = "alice",
                AuctionEnd = DateTime.UtcNow.AddDays(30),
                Item = new ItemEntity
                {
                    Make = "BMW",
                    Model = "X1",
                    Color = "White",
                    Mileage = 90000,
                    Year = 2017,
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/08/31/05/47/bmw-2699538_960_720.jpg"
                }
            },
            // 6 Ferrari spider
            new AuctionEntity
            {
                Id = Guid.Parse("dc1e4071-d19d-459b-b848-b5c3cd3d151f"),
                Status = StatusEnum.Live,
                ReservePrice = 20000,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(45),
                Item = new ItemEntity
                {
                    Make = "Ferrari",
                    Model = "Spider",
                    Color = "Red",
                    Mileage = 50000,
                    Year = 2015,
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/11/09/01/49/ferrari-458-spider-2932191_960_720.jpg"
                }
            },
            // 7 Ferrari F-430
            new AuctionEntity
            {
                Id = Guid.Parse("47111973-d176-4feb-848d-0ea22641c31a"),
                Status = StatusEnum.Live,
                ReservePrice = 150000,
                Seller = "alice",
                AuctionEnd = DateTime.UtcNow.AddDays(13),
                Item = new ItemEntity
                {
                    Make = "Ferrari",
                    Model = "F-430",
                    Color = "Red",
                    Mileage = 5000,
                    Year = 2022,
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/11/08/14/39/ferrari-f430-2930661_960_720.jpg"
                }
            },
            // 8 Audi R8
            new AuctionEntity
            {
                Id = Guid.Parse("6a5011a1-fe1f-47df-9a32-b5346b289391"),
                Status = StatusEnum.Live,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(19),
                Item = new ItemEntity
                {
                    Make = "Audi",
                    Model = "R8",
                    Color = "White",
                    Mileage = 10050,
                    Year = 2021,
                    ImageUrl = "https://cdn.pixabay.com/photo/2019/12/26/20/50/audi-r8-4721217_960_720.jpg"
                }
            },
            // 9 Audi TT
            new AuctionEntity
            {
                Id = Guid.Parse("40490065-dac7-46b6-acc4-df507e0d6570"),
                Status = StatusEnum.Live,
                ReservePrice = 20000,
                Seller = "tom",
                AuctionEnd = DateTime.UtcNow.AddDays(20),
                Item = new ItemEntity
                {
                    Make = "Audi",
                    Model = "TT",
                    Color = "Black",
                    Mileage = 25400,
                    Year = 2020,
                    ImageUrl = "https://cdn.pixabay.com/photo/2016/09/01/15/06/audi-1636320_960_720.jpg"
                }
            },
            // 10 Ford Model T
            new AuctionEntity
            {
                Id = Guid.Parse("3659ac24-29dd-407a-81f5-ecfe6f924b9b"),
                Status = StatusEnum.Live,
                ReservePrice = 20000,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(48),
                Item = new ItemEntity
                {
                    Make = "Ford",
                    Model = "Model T",
                    Color = "Rust",
                    Mileage = 150150,
                    Year = 1938,
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/08/02/19/47/vintage-2573090_960_720.jpg"
                }
            }
        };

        context.Auctions.AddRange(auctions);
        await context.SaveChangesAsync();
    }
}
