﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }

        public Invoice(int id, string invoiceNumber, DateTime issueDate, DateTime dueDate, List<InvoiceItem> invoiceItems)
        {
            Id = id;
            InvoiceNumber = invoiceNumber;
            IssueDate = issueDate;
            DueDate = dueDate;
            InvoiceItems = invoiceItems;
        }
    }
}
