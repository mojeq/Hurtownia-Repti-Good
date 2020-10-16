using AutoMapper;
using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models.Interfaces;
using HurtowniaReptiGood.Models.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;



namespace HurtowniaReptiGood.Models.Services
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly MyContex _myContex;
        public CartService(IMapper mapper, MyContex myContex)
        {
            _mapper = mapper;
            _myContex = myContex;
        }

        // creating new cart and saving that to database with state "cart"
        public async Task<int> CreateNewCartOrder(CustomerEntity loggedUser, ItemCartViewModel itemCart)
        {
            OrderEntity orderNew = new OrderEntity()
            {
                CustomerId = loggedUser.CustomerId,
                StateOrder = "cart",
                DateOrder = DateTime.Now,
                StatusOrder = "W realizacji",
            };
            await _myContex.Orders.AddAsync(orderNew);

            await _myContex.SaveChangesAsync();

            var order = await _myContex.Orders.OrderByDescending(s => s.OrderId)
                                 .Where(o => o.CustomerId == loggedUser.CustomerId)
                                 .Where(o => o.StateOrder == "cart")
                                 .FirstOrDefaultAsync();

            var orderDetail = _mapper.Map<OrderDetailEntity>(itemCart);

            orderDetail.OrderId = order.OrderId;

            orderDetail.CurrentStockInWholesale = await GetCurrentStockInWholesale(itemCart.ProductId);

            await _myContex.OrderDetails.AddAsync(orderDetail);

            await _myContex.SaveChangesAsync();

            // decrease current item stock in database 
            await DecreaseStockInWholesale(itemCart);

            return order.OrderId;
        }

        // adding next product to current cart or increase quantity
        public async Task<int> AddItemToExistCart(CustomerEntity loggedUser, ItemCartViewModel itemCart)
        {
            // finding last order ID
            int orderId = _myContex.Orders.OrderByDescending(s => s.OrderId)
                                             .Where(o => o.CustomerId == loggedUser.CustomerId)
                                             .Where(o => o.StateOrder == "cart")
                                             .FirstOrDefault().OrderId;

            // checking it is this item in cart if yes increase quantity if no, add new position OrderDetail
            OrderDetailEntity orderDetailExist = await _myContex.OrderDetails
                                                            .Where(a => a.OrderId == orderId)
                                                            .Where(a => a.ProductSymbol == itemCart.ProductSymbol)
                                                            .FirstOrDefaultAsync();
            if (orderDetailExist == null)
            {
                var orderDetail = _mapper.Map<OrderDetailEntity>(itemCart);

                orderDetail.OrderId = orderId;

                orderDetail.CurrentStockInWholesale = await GetCurrentStockInWholesale(itemCart.ProductId);

                await _myContex.OrderDetails.AddAsync(orderDetail);

                await _myContex.SaveChangesAsync();

                // decrease current item stock in database 
                await DecreaseStockInWholesale(itemCart);
            }
            else
            {
                orderDetailExist.Quantity += itemCart.Quantity;

                await _myContex.SaveChangesAsync();

                // decrease current item stock in database 
                await DecreaseStockInWholesale(itemCart);
            }

            // receive items in cart
            var itemsInCart = await _myContex.OrderDetails.Where(c => c.OrderId == orderId).ToListAsync();

            return orderId;
        }

        // get content of current cart/order
        public async Task<OrderDetailListViewModel> GetCartDetailList(int orderId)
        {
            var orderDetailList = await _myContex.OrderDetails
                .Where(c => c.OrderId == orderId)
                .ToListAsync();

            var mapped = _mapper.Map<List<OrderDetailViewModel>>(orderDetailList);

            OrderDetailListViewModel cartDetails = new OrderDetailListViewModel()
            {
                OrderDetailList = mapped,
            };

            return cartDetails;
        }

        // get shipping address of customer who make purchase 
        public async Task<ShippingAddressViewModel> GetShippingAddress(int orderId)
        {
            var orderWithCustomerAndShippingAddress = await _myContex.Orders
                .Include(p => p.Customer)
                    .ThenInclude(c => c.ShippingAddress)
                .Where(x => x.OrderId == orderId)
                .FirstOrDefaultAsync();

            var shippingAddress = _mapper.Map<ShippingAddressViewModel>(orderWithCustomerAndShippingAddress);

            return shippingAddress;
        }

        // get shipping address of customer who make purchase  
        public async Task<InvoiceAddressViewModel> GetInvoiceAddress(int orderId)
        {
            var orderWithCustomerAndInvoiceAddress = await _myContex.Orders
                .Include(x => x.Customer)
                    .ThenInclude(x => x.InvoiceAddress)
                .Where(x => x.OrderId == orderId)
                .FirstOrDefaultAsync();

            var invoiceAddress = _mapper.Map<InvoiceAddressViewModel>(orderWithCustomerAndInvoiceAddress);

            return invoiceAddress;
        }

        // remowe one item from current cart
        public async Task RemoveItemFromCart(int orderDetailId)
        {
            // increase stock in database
            await IncreaseStockInWholesale(orderDetailId);

            var orderDetailToRemove = await _myContex.OrderDetails.FindAsync(orderDetailId);

            _myContex.OrderDetails.Remove(orderDetailToRemove);

            await _myContex.SaveChangesAsync();
        }

        // update quantity of one item from current cart (button quantity in cart) 
        public async Task UpdateQuantityItemInCart(int orderDetailId, int quantity)
        {
            var orderDetailExist = await _myContex.OrderDetails.FindAsync(orderDetailId);

            orderDetailExist.Quantity = quantity;

            await _myContex.SaveChangesAsync();

            // increase stock in database
            await IncreaseStockInWholesale(orderDetailId, quantity);
        }

        // save new order to database exactly change state of current order and create attachment and sending mail with confirmation order
        public async Task SaveNewOrder(int orderId, double valueOrder, string orderMessage)
        {
            var orderUpdate = await _myContex.Orders.FindAsync(orderId);

            orderUpdate.DateOrder = DateTime.Now;
            orderUpdate.StateOrder = "bought";
            orderUpdate.ValueOrder = valueOrder;
            orderUpdate.StatusOrder = "W realizacji";
            orderUpdate.OrderMessage = orderMessage;

            await _myContex.SaveChangesAsync();
        }

        // create pdf with order 
        public async Task CreatePdfAttachmentWithOrder(int orderId)
        {
            var orderExist = _myContex.Orders.Find(orderId);
            var orderExistDetailList = _myContex.OrderDetails.Where(x => x.OrderId == orderId).ToList();

            FontFactory.RegisterDirectory("C:WINDOWSFonts"); //add polish signs
            var titleFont18 = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED, 18, Font.BOLD);
            var titleFont14 = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED, 14, Font.BOLD);
            var textFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED, 12);
            Font link = FontFactory.GetFont("Arial", 12, Font.UNDERLINE);

            FileStream fs = new FileStream("PDF/Zamowienie" + orderId + ".pdf", FileMode.Create);
            Document orderPdf = new Document(PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(orderPdf, fs);
            orderPdf.Open();

            Anchor orderLink = new Anchor("Link do panelu klienta http://www.reptihurt.pl/index \n", link);
            orderPdf.Add(new Paragraph(orderLink));
            String line = "Zamówienie nr " + orderId;
            orderPdf.Add(new Paragraph(line + "\n\n", titleFont14));
            //orderLink.Reference = "http://www.reptihurt.pl/zamowienia/index";           

            PdfPTable table = new PdfPTable(2);
            table.AddCell(new Phrase("Numer dokumentu", textFont));
            table.AddCell(orderId.ToString());
            table.AddCell(new Phrase("Data i godzina przyjęcia zamówienia", textFont));
            table.AddCell(orderExist.DateOrder.ToString());
            table.AddCell(new Phrase("Wartość zamówienia brutto", textFont));
            table.AddCell(new Phrase(orderExist.ValueOrder.ToString() + "zł", textFont));
            table.AddCell(new Phrase("Uwagi do zamówienia", textFont));
            table.AddCell(new Phrase(orderExist.OrderMessage, textFont));

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
                table2.AddCell(new Phrase(orderDetail.Quantity.ToString(), textFont));
                table2.AddCell(new Phrase(orderDetail.Price + "zł", textFont));
            }
            orderPdf.Add(table);
            orderPdf.Add(table2);

            orderPdf.Close();
            writer.Close();
            fs.Close();
        }

        // send mail with pdf attachment to customer
        public async Task SendMailWithAttachment(int orderId)
        {
            var order = _myContex.Orders.Find(orderId);
            var customerMail = _myContex.ShippingAddresses.Find(_myContex.Customers.Find(order.CustomerId).ShippingAddressId).Email;
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Hurtownia TerraHurt", "biuro@reptigood.pl"));
            message.To.Add(new MailboxAddress(customerMail));
            message.Subject = "Potwierdzenie zamówienia nr " + orderId;
            BodyBuilder message_body = new BodyBuilder();

            message_body.Attachments.Add("PDF/Zamowienie" + orderId + ".pdf");
            string textBody = "Potwierdzenie zamówienia w załączniku\n\nUwagi do zamówienia:\n"
                                + order.OrderMessage + "\n\nPozdrawiam\nPiotr Moj\nreptigood.pl";

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
        public async Task DecreaseStockInWholesale(ItemCartViewModel itemCart)
        {
            var productInDatabase = _myContex.Products.Find(itemCart.ProductId);

            productInDatabase.Stock = productInDatabase.Stock - itemCart.Quantity;

            await _myContex.SaveChangesAsync();
        }
        public async Task<int> GetCurrentStockInWholesale(int productId)
        {
            var product = await _myContex.Products.FindAsync(productId);

            return product.Stock;
        }
        public async Task IncreaseStockInWholesale(int orderDetailId)
        {
            var productWithOrderDeail = await _myContex.OrderDetails
                                                    .Include(x => x.Product)
                                                    .Where(x => x.OrderDetailId == orderDetailId)
                                                    .FirstOrDefaultAsync();

            productWithOrderDeail.Product.Stock = productWithOrderDeail.Product.Stock + productWithOrderDeail.Quantity; 

            await _myContex.SaveChangesAsync();
        }
        public async Task IncreaseStockInWholesale(int orderDetailId, int quantity)
        {
            var productWithOrderDetail = await _myContex.OrderDetails
                                                    .Include(x => x.Product)
                                                    .Where(x => x.OrderDetailId == orderDetailId)
                                                    .FirstOrDefaultAsync();

            productWithOrderDetail.Product.Stock = productWithOrderDetail.Product.Stock + quantity;

            await _myContex.SaveChangesAsync();
        }
    }
}
