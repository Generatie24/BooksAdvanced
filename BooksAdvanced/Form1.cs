using LibraryStoredProcedure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooksAdvanced
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            Insert();

        }

        private void Insert()
        {

            if ((int)cmbCountry.SelectedValue < 0)
            {
                MessageBox.Show("You should select country");
                lblError.Visible = true;
                lblError.Text = "You should select country";
            }
            else
            {
                Book book = new Book();
                book.CountryId = (int)cmbCountry.SelectedValue;
                book.Title = txtTitle.Text;
                book.Author = txtAuthor.Text;
                book.Price = decimal.Parse(txtPrice.Text);
                book.Description = txtDescription.Text;

                BookRepo bookRepo = new BookRepo();
                bookRepo.AddBook(book);
                lblError.Visible = false;
                FillListBoxWithBooks();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillCountries();
            FillListBoxWithBooks();

        }

        private void FillCountries()
        {
            List<Country> countriesWithText = new List<Country>();
            countriesWithText.Add(new Country { Id = -1, Name = "Select Country" });

            CountryRepo countryrepo = new CountryRepo();
            List<Country> countrylist = countryrepo.GetCountries();

            countriesWithText.AddRange(countrylist);

            this.cmbCountry.Items.Clear();
            cmbCountry.DisplayMember = "Name";
            cmbCountry.ValueMember = "Id";
            this.cmbCountry.DataSource = countriesWithText;
        }

        private void FillListBoxWithBooks()
        {
            List<Book> booklist = new List<Book>();
            BookRepo bookRepo = new BookRepo();
            booklist = bookRepo.GetBooks();
            lstBooks.Items.Clear();
            foreach (var item in booklist)
            {
                lstBooks.Items.Add(item);
            }
            lblCount.Text = "Number of records : " + lstBooks.Items.Count.ToString();

        }

        private void lstBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Book book = lstBooks.SelectedItem as Book;
            Book book = new Book();
            book = (Book)lstBooks.SelectedItem;
            if (book != null)
            {
                cmbCountry.SelectedValue = book.CountryId;
                txtTitle.Text = book.Title;
                txtAuthor.Text = book.Author;
                txtPrice.Text = book.Price.ToString();
                txtDescription.Text = book.Description;
                lblIdInvisible.Text = book.Id.ToString();

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            book.CountryId = (int)cmbCountry.SelectedValue;
            book.Title = txtTitle.Text;
            book.Author = txtAuthor.Text;
            book.Price = decimal.Parse(txtPrice.Text);
            book.Description = txtDescription.Text;

            BookRepo bookRepo = new BookRepo();
            bookRepo.UpdateBook(book);
            lblError.Visible = false;
            FillListBoxWithBooks();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();

        }

        private void ClearFields()
        {
            cmbCountry.SelectedIndex = 0;
            lblIdInvisible.Text = "";
            txtTitle.Text = "";
            txtAuthor.Text = "";
            txtPrice.Text = "";
            txtDescription.Text = "";
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            book = (Book)lstBooks.SelectedItem;
            if (book != null)
            {
                BookRepo bookRepo = new BookRepo();
                bookRepo.DeleteBook(book);
            }
            ClearFields();
            FillListBoxWithBooks();
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            Book book = new Book();
            book = (Book)lstBooks.SelectedItem;
            book.CountryId = (int)cmbCountry.SelectedValue;
            book.Title = txtTitle.Text;
            book.Author = txtAuthor.Text;
            book.Price = decimal.Parse(txtPrice.Text);
            book.Description = txtDescription.Text;

            if (book != null)
            {
                book.Id = int.Parse(lblIdInvisible.Text);
                BookRepo bookRepo = new BookRepo();
                bookRepo.UpdateBook(book);
            }
            FillListBoxWithBooks();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Form2 form2 = new Form2();
            form2.ShowDialog();  
        }
    }
}
