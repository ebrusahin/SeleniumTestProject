using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTestProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeleniumTestProject
{
    public partial class Form1 : Form
    {
        List<ProductModel> productList;
        public Form1()
        {
            InitializeComponent();
            productList = new List<ProductModel>();
            
            textBox2.Text = "Ürün Filtrele...";
        }


     

       
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            filterProduct(textBox2.Text);
        }

        public void createProduct(String company, ReadOnlyCollection<IWebElement> productList, ReadOnlyCollection<IWebElement> priceList)
        {
            List<ProductModel> pmList = new List<ProductModel>();
            for (var i = 0; i < productList.Count; i++)
            {
                ProductModel pm = new ProductModel();
                if (productList.ElementAt(i).Text != "" && priceList.ElementAt(i).Text != "")
                {
                    pm.name = productList.ElementAt(i).Text;
                    pm.price = priceList.ElementAt(i).Text;
                    pm.companyName = company;
                    pm.date = DateTime.Now;
                    pmList.Add(pm);
                }
            }
            Helper.HelperProduct.addProducts(pmList);
        }
        public void listProduct()
        {
            productList = Helper.HelperProduct.getProducts();
            foreach (ProductModel pm in productList)
            {
                dataGridView1.Rows.Add(pm.name, pm.discount, pm.price, pm.companyName, pm.date, pm.keyword);
            }
        }
        public void filterProduct(String filterText)
        {
            foreach (ProductModel pm in productList)
            {
                if (pm.name.ToLower().Contains(filterText.ToLower()))
                {
                    dataGridView1.Rows.Add(pm.name, pm.discount, pm.price, pm.companyName, pm.date, pm.keyword);
                }
            }
        }

        public void deleteAllProducts()
        {
            Helper.HelperProduct.deleteAllProducts();
        }

      
     

        private void button1_Click_1(object sender, EventArgs e)
        {
            deleteAllProducts();
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Lütfen sorgulamak istediğiniz ürünü giriniz!");
            }
            else
            {
                if (checkBox1.Checked)
                {
                    var driver = new ChromeDriver();
                    driver.Navigate().GoToUrl("https://n11.com");
                    driver.FindElement(By.Id("searchData")).SendKeys(textBox1.Text);
                    driver.FindElement(By.ClassName("searchBtn")).Click();
                    Thread.Sleep(1000);

                    ReadOnlyCollection<IWebElement> productList = driver.FindElements(By.ClassName("productName"));
                    ReadOnlyCollection<IWebElement> priceList = driver.FindElements(By.ClassName("newPrice"));
                    createProduct(Enum.CompanyEnum.Company.N11.ToString(), productList, priceList);
                    driver.Quit();
                    listProduct();
                }
                else if (checkBox2.Checked)
                {
                    var driver = new ChromeDriver();
                    driver.Navigate().GoToUrl("https://carrefoursa.com/tr/");
                    driver.FindElement(By.Id("js-site-search-input")).SendKeys(textBox1.Text);
                    driver.FindElement(By.ClassName("input-group-btn")).Click();
                    Thread.Sleep(1000);

                    ReadOnlyCollection<IWebElement> productList = driver.FindElements(By.ClassName("item-name"));
                    ReadOnlyCollection<IWebElement> priceList = driver.FindElements(By.ClassName("item-price"));

                    createProduct(Enum.CompanyEnum.Company.CARREFOURSA.ToString(), productList, priceList);
                    driver.Quit();
                    listProduct();
                }
                else
                {
                    MessageBox.Show("Lütfen arama yapmak istediğiniz siteyi seçiniz!");
                }

            }



        }

      

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Aranacak Ürün...";
        }
    }
}
