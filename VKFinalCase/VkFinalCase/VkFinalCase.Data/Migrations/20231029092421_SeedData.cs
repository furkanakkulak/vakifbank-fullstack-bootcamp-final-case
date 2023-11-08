using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VkFinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
   migrationBuilder.Sql(@"
               
INSERT INTO [dbo].[User]([Username],[Password] ,[Role] ,[Email] ,[CreatedDate] ,[UpdatedDate] ,[IsActive])
VALUES ('admin' ,'3173784ba37c4575c6a26bd23f62a45d' ,'admin' ,'admin@admin.com' ,'2023-07-07' ,'2023-07-07', 1)

INSERT INTO [dbo].[User]([Username],[Password] ,[Role] ,[Email] ,[CreatedDate] ,[UpdatedDate] ,[IsActive])
VALUES ('dealer1' ,'3173784ba37c4575c6a26bd23f62a45d' ,'dealer' ,'dealer1@dealer.com' ,'2023-07-07' ,'2023-07-07', 1)

INSERT INTO [dbo].[User]([Username],[Password] ,[Role] ,[Email] ,[CreatedDate] ,[UpdatedDate] ,[IsActive])
VALUES ('dealer2' ,'3173784ba37c4575c6a26bd23f62a45d' ,'dealer' ,'dealer2@dealer.com' ,'2023-07-07' ,'2023-07-07', 1)



INSERT INTO [dbo].[Dealer]([UserId],[Address] ,[TaxNumber] ,[CreditLimit] ,[Margin], [CreatedDate] ,[UpdatedDate] ,[IsActive])
VALUES ('2' ,'Dealer1 Street, No:1/1 Dealer1/Company' ,1000000001 ,10000,20 ,'2023-07-07' ,'2023-07-07', 1)

INSERT INTO [dbo].[Dealer]([UserId],[Address] ,[TaxNumber] ,[CreditLimit] ,[Margin], [CreatedDate] ,[UpdatedDate] ,[IsActive])
VALUES ('3' ,'Dealer2 Street, No:1/2 Dealer2/Company' ,1000000002 ,100000,30 ,'2023-07-07' ,'2023-07-07', 1)



INSERT INTO [dbo].[PaymentMethod]([Name],[CreatedDate] ,[UpdatedDate] ,[IsActive])
VALUES ('Bank Transfer' ,'2023-07-07' ,'2023-07-07', 1)

INSERT INTO [dbo].[PaymentMethod]([Name],[CreatedDate] ,[UpdatedDate] ,[IsActive])
VALUES ('Credit Card' ,'2023-07-07' ,'2023-07-07', 1)



INSERT INTO [dbo].[Product]([Name],[Price],[StockQuantity],[MinStockQuantity],[CreatedDate] ,[UpdatedDate] ,[IsActive])
VALUES ('Apple Macbook Air M1',1199.99,3532,10 ,'2023-07-07' ,'2023-07-07', 1)

INSERT INTO [dbo].[Product]([Name],[Price],[StockQuantity],[MinStockQuantity],[CreatedDate] ,[UpdatedDate] ,[IsActive])
VALUES ('Apple Macbook Pro M1',1699.99,2319,4 ,'2023-07-07' ,'2023-07-07', 1)

INSERT INTO [dbo].[Product]([Name],[Price],[StockQuantity],[MinStockQuantity],[CreatedDate] ,[UpdatedDate] ,[IsActive])
VALUES ('Apple Macbook Air M2',1599.99,1892,6 ,'2023-07-07' ,'2023-07-07', 1)

INSERT INTO [dbo].[Product]([Name],[Price],[StockQuantity],[MinStockQuantity],[CreatedDate] ,[UpdatedDate] ,[IsActive])
VALUES ('Apple Macbook Pro M2',1999.99,872,3 ,'2023-07-07' ,'2023-07-07', 1)

");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
