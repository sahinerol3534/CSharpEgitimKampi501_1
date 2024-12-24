using CSharpEgitimKampi501_1.Dtos;
using CSharpEgitimKampi501_1.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi501_1
{
    public partial class Form1 : Form
    {
       private readonly ProductRepository _repository;

       

        public Form1()
        {
            InitializeComponent();
            _repository = new ProductRepository();
           
        }

        private async void btnList_Click(object sender, EventArgs e)
        {
            var products = await _repository.GetAllProductAsync();
            dataGridView1.DataSource = products;
            var expensiveBook = await _repository.GetMostExpensiveBookAsync();
            lblmaxProductPriceName.Text = expensiveBook.ProductName.ToString();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var createDto = new CreateProductDto
            {
                ProductName = txtProductName.Text,
                ProductStock = int.Parse(txtProductStock.Text),
                ProductPrice = decimal.Parse(txtProductPrice.Text),
                ProductCatagory = cmbCatagoriesName.SelectedItem?.ToString(),
            };

            await _repository.CreateProductAsync(createDto);
            var products = await _repository.GetAllProductAsync();
            dataGridView1.DataSource = products;
            var expensiveBook = await _repository.GetMostExpensiveBookAsync();
            lblmaxProductPriceName.Text = expensiveBook.ProductName.ToString();
            MessageBox.Show("Ürün Eklendi");
            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox) 
                { 
                    textBox.Clear();
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            int productId = int.Parse(txtProductId.Text);
            await _repository.DeleteProductAsync(productId);
            MessageBox.Show("Ürün Silindi");
            var products = await _repository.GetAllProductAsync();
            dataGridView1.DataSource = products;
            var expensiveBook = await _repository.GetMostExpensiveBookAsync();
            lblmaxProductPriceName.Text = expensiveBook.ProductName.ToString();
            ClearTextBoxes() ;
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            var updateDto = new UpdateProductDto
            {
                ProductId = int.Parse(txtProductId.Text),
                ProductName = txtProductName.Text,
                ProductStock = int.Parse(txtProductStock.Text),
                ProductPrice = decimal.Parse( txtProductPrice.Text),
                ProductCatagory = cmbCatagoriesName.SelectedItem?.ToString(),
            };
            await _repository.UpdateProductAsync(updateDto);
            var products = await _repository.GetAllProductAsync();
            dataGridView1.DataSource = products;
            var expensiveBook = await _repository.GetMostExpensiveBookAsync();
            lblmaxProductPriceName.Text = expensiveBook.ProductName.ToString();
            MessageBox.Show("Ürün Güncellendi");
            ClearTextBoxes();
        }

        private async Task LoadCatagoriesAsycn()
        {
            var catagories = await _repository.GetAllCatagoriesAsycn();
            cmbCatagoriesName.Items.AddRange(catagories.ToArray());
        }

        

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadCatagoriesAsycn();
            int totalBooks = await _repository.GetTotalBookCountAsync();
            lblprodoctCount.Text = totalBooks.ToString();

            var expensiveBook = await _repository.GetMostExpensiveBookAsync();
            lblmaxProductPriceName.Text = expensiveBook.ProductName.ToString();

            int catagoryCount = await _repository.GetTotalCatagoryCount();
            lblproductCatagoryCount.Text =  catagoryCount.ToString();
        }
        
    }
}
 