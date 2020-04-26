using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;



namespace HurtowniaReptiGood.Models.Services
{
    public class CartService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MyContex _myContex;
        public CartService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            MyContex myContex)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _myContex = myContex;
        }
        public int CreateNewCartOrder(CustomerEntity loggedUser, ItemCartViewModel itemCart)
        {
            OrderEntity order = new OrderEntity()
            {
                CustomerId = loggedUser.CustomerId,
                StateOrder = "cart",
                DateOrder = DateTime.Now,
                StatusOrder = "W realizacji",
            };
            _myContex.Orders.Add(order);
            _myContex.SaveChanges();

            int orderId = _myContex.Orders.OrderByDescending(s => s.OrderId)
                                 .Where(o => o.CustomerId == loggedUser.CustomerId)
                                 .Where(o => o.StateOrder == "cart")
                                 .FirstOrDefault().OrderId;

            // create object with order details and save in database
            OrderDetailEntity orderDetail = new OrderDetailEntity()
            {
                ProductId = itemCart.ProductId,
                ProductSymbol = itemCart.ProductSymbol,
                ProductName = itemCart.ProductName,
                OrderId = orderId,
                Quantity = itemCart.Quantity,
                Price = itemCart.Price,
            };
            _myContex.OrderDetails.Add(orderDetail);
            _myContex.SaveChanges();

            return orderId;
        }
        public int AddItemToExistCart(CustomerEntity loggedUser, ItemCartViewModel itemCart)
        {
            // finding last order ID
            int orderId = _myContex.Orders.OrderByDescending(s => s.OrderId)
                                             .Where(o => o.CustomerId == loggedUser.CustomerId)
                                             .Where(o => o.StateOrder == "cart")
                                             .FirstOrDefault().OrderId;

            //checking it is this item in cart if yes increase quantity if no, add new position OrderDetail
            OrderDetailEntity orderDetailExist = _myContex.OrderDetails.Where(a => a.OrderId == orderId)
                                                                  .Where(a => a.ProductSymbol == itemCart.ProductSymbol)
                                                                  .FirstOrDefault();
            if (orderDetailExist == null)
            {
                // create object with order details and save in database
                OrderDetailEntity orderDetail = new OrderDetailEntity()
                {
                    ProductId= itemCart.ProductId,
                    ProductSymbol = itemCart.ProductSymbol,
                    ProductName = itemCart.ProductName,
                    OrderId = orderId,
                    Quantity = itemCart.Quantity,
                    Price = itemCart.Price,
                };
                _myContex.OrderDetails.Add(orderDetail);
                _myContex.SaveChanges();
            }
            else
            {
                orderDetailExist.Quantity += itemCart.Quantity;
                _myContex.SaveChanges();
            }

            //receive items in cart
            var itemsInCart = _myContex.OrderDetails.Where(c => c.OrderId == orderId).ToList();
            return orderId;
        }

        public OrderDetailListViewModel GetCartDetailList(int orderId)
        {
            OrderDetailListViewModel cartDetails = new OrderDetailListViewModel();
            cartDetails.OrderDetailList = _myContex.OrderDetails
                .Where(c => c.OrderId == orderId)
                .Select(x => new OrderDetailViewModel
                {
                    OrderDetailId=x.OrderDetailId,
                    OrderId=x.OrderId,
                    ProductName=x.ProductName,
                    ProductSymbol=x.ProductSymbol,
                    Price=x.Price,
                    Quantity=x.Quantity                
                }).ToList();

            return cartDetails;
        }

        public ShippingAddressViewModel GetShippingAddress(int orderId)
        {
            var customerId = _myContex.Orders.Find(orderId).CustomerId;
            int shippingAddressId = _myContex.Customers.Find(customerId).ShippingAddressId;
            var shippingAddress = _myContex.ShippingAddresses
                .Where(c => c.ShippingAddressId == shippingAddressId)
                .Select(x => new ShippingAddressViewModel
                {                    
                    Street = x.Street,
                    StreetNumber = x.StreetNumber,
                    ZipCode = x.ZipCode,
                    City = x.City,
                    Phone = x.Phone,
                    CompanyName = x.CompanyName,
                    CustomerName = x.CustomerName,
                    CustomerSurname = x.CustomerSurname,
                    Email = x.Email,
                }).FirstOrDefault();

            return shippingAddress;
        }

        public InvoiceAddressViewModel GetInvoiceAddress(int orderId)
        {
            var customerId = _myContex.Orders.Find(orderId).CustomerId;
            int invoiceAddressId = _myContex.Customers.Find(customerId).InvoiceAddressId;
            var invoiceAddress = _myContex.InvoiceAddresses
                .Where(c => c.InvoiceAddressId == invoiceAddressId)
                .Select(x => new InvoiceAddressViewModel
                {
                    Street = x.Street,
                    StreetNumber = x.StreetNumber,
                    ZipCode = x.ZipCode,
                    City = x.City,
                    CompanyName = x.CompanyName,
                    CustomerName = x.CustomerName,
                    CustomerSurname = x.CustomerSurname,
                    Phone=x.Phone,
                    NIP=x.NIP                    
                }).FirstOrDefault();

            return invoiceAddress;
        }

        public void RemoveItemFromCart(int orderDetailId)
        {
            var orderDetailToRemove = _myContex.OrderDetails.Find(orderDetailId);
            _myContex.OrderDetails.Remove(orderDetailToRemove);
            _myContex.SaveChanges();
        }

        public void UpdateQuantityItemInCart(int orderDetailId, int quantity)
        {           
            var orderDetailExist = _myContex.OrderDetails.Find(orderDetailId);
            orderDetailExist.Quantity = quantity;
            _myContex.SaveChanges();
        }   
        
        public void SaveNewOrder(int orderId, double valueOrder)
        {
            var orderUpdate = _myContex.Orders.Find(orderId);
            orderUpdate.DateOrder = DateTime.Now;
            orderUpdate.StateOrder = "bought";
            orderUpdate.ValueOrder = valueOrder;
            orderUpdate.StatusOrder = "W realizacji";
            _myContex.SaveChanges();
        }

        public void CreatePdfAttachmentWithOrder(int orderId)
        {
            var orderExist = _myContex.Orders.Find(orderId);
            var orderExistDetailList = _myContex.OrderDetails.Where(x => x.OrderId == orderId).ToList();

            FontFactory.RegisterDirectory("C:WINDOWSFonts"); //add polish signs
            var titleFont18 = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED, 18, Font.BOLD);
            var titleFont14 = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED, 14, Font.BOLD);
            var textFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED, 12);
            Font link = FontFactory.GetFont("Arial", 12, Font.UNDERLINE);

            FileStream fs = new FileStream("PDF/Order" + orderId + ".pdf", FileMode.Create);
            Document orderPdf = new Document(PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(orderPdf, fs);
            orderPdf.Open();

            Anchor orderLink = new Anchor("Link do panelu klienta http://www.reptigood.pl/zamowienia/index \n", link);
            orderPdf.Add(new Paragraph(orderLink));
            String line = "Zamówienie nr " + orderId;
            orderPdf.Add(new Paragraph(line + "\n\n", titleFont14));
            //orderLink.Reference = "http://www.reptigood.pl/zamowienia/index";           

            PdfPTable table = new PdfPTable(2);
            table.AddCell(new Phrase("Numer dokumentu", textFont));
            table.AddCell(orderId.ToString());
            table.AddCell(new Phrase("Data i godzina przyjęcia zamówienia", textFont));
            table.AddCell(orderExist.DateOrder.ToString());
            table.AddCell(new Phrase("Wartość zamówienia brutto", textFont));
            table.AddCell(new Phrase(orderExist.ValueOrder.ToString() + "zł", textFont));

            PdfPTable table2 = new PdfPTable(3);
            table2.SetWidths(new int[] { 7, 1, 1 });
            PdfPCell cell2 = new PdfPCell(new Phrase("\n\nZawartość zamówienia:\n", titleFont14));
            cell2.Colspan = 3;
            cell2.Border = 0;
            cell2.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right  
            table2.AddCell(cell2);

            PdfPCell cell3 = new PdfPCell(new Phrase("Nazwa", textFont));
            cell3.HorizontalAlignment = 1;
            table2.AddCell(cell3);
            PdfPCell cell4 = new PdfPCell(new Phrase("Ilość", textFont));
            cell3.HorizontalAlignment = 1;
            table2.AddCell(cell4);
            PdfPCell cell5 = new PdfPCell(new Phrase("Cena", textFont));
            cell3.HorizontalAlignment = 1;
            table2.AddCell(cell5);

            foreach (var orderDetail in orderExistDetailList)
            {
                table2.AddCell(new Phrase(orderDetail.ProductName, textFont));
                table2.AddCell(new Phrase(orderDetail.Quantity));
                table2.AddCell(new Phrase(orderDetail.Price + "zł", textFont));
            }
            orderPdf.Add(table);
            orderPdf.Add(table2);            

            orderPdf.Close();
            writer.Close();
            fs.Close();
        }

        public void SendMailWithAttachment()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Hurtownia TerraHurt", "biuro@reptigood.pl"));
            message.To.Add(new MailboxAddress("customer", "biuro@reptigood.pl"));
            BodyBuilder message_body = new BodyBuilder();
            //message_body.Attachments.Add("order1.pdf");
            string textBody = "Zamówienie";
            message_body.TextBody = textBody;
            message.Body = message_body.ToMessageBody();

            using (var smtpClient = new SmtpClient())
            {
                var mailConfig = _myContex.Mails.Find(1);
                smtpClient.Connect(mailConfig.Serwer, 465, true);
                smtpClient.Authenticate(mailConfig.Mail, mailConfig.Password);
                smtpClient.Send(message);
                smtpClient.Disconnect(true);
            }
        }
    }
}
