using System;

namespace ToleLoB.Tests.Sql
{
    public class Customer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Disable { get; set; }
        public long FriendCustomerId { get; set; }
    }
    public class Invoice
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Serie { get; set; }
        public DateTime Date { get; set; }
        public long CustomerId { get; set; }
        public decimal Total { get; set; }
    }
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    public class InvoiceDetail
    {
        public long Id { get; set; }
        public long InvoiceId { get; set; }
        public long ProductId { get; set; }
        public string Subject { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public decimal Count { get; set; }
        public decimal Total { get; set; }
    }
}