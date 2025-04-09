﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Rul.Entities;
using Rul.Pages;

namespace Rul.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderTicketPage.xaml
    /// </summary>
    public partial class OrderTicketPage : Page
    {
        List<Product> productList = new List<Product>();
        public OrderTicketPage(Order currentOrder, List<Product> products)
        {
            InitializeComponent();
            productList = products;
            DataContext = currentOrder;

            txtPickupPoint.Text = currentOrder.PickupPoint.Address;

            var result = "";
            foreach (var pl in productList)
                result += (result == "" ? "" : ", ") + pl.ProductName.ToString();
            txtProductList.Text = result;

            var total = productList.Sum(p => Convert.ToDouble(p.ProductCost) - Convert.ToDouble(p.ProductCost) * Convert.ToDouble(p.ProductDiscountAmount / 100.00));
            txtCost.Text = $"{total.ToString()} рублей";

            var discountSum = productList.Sum(p => p.ProductDiscountAmount);
            txtDiscountSum.Text = $"{discountSum.ToString()} %";
        }
        private void btnSaveDocument_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                IDocumentPaginatorSource idp = flowDoc;
                pd.PrintDocument(idp.DocumentPaginator, Title);
            }
        }
    }
}
