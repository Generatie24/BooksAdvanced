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
            InsertOrUpdate();

        }

        private void InsertOrUpdate()
        {
            if ((int)cmbCountry.SelectedValue < 0)
            {
                MessageBox.Show("You should select a country");
                lblError.Visible = true;
                lblError.Text = "You should select a country";
            }
            else
            {
                Book book = new Book();
                book.CountryId = (int)cmbCountry.SelectedValue;
                book.Title = txtTitle.Text;
                book.Author = txtAuthor.Text;
                book.Price = decimal.Parse(txtPrice.Text);
                book.Desription = txtDescription.Text;

                BookRepo bookRepo = new BookRepo();
                bookRepo.AddBook(book);
                lblError.Visible = false;
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
                txtDescription.Text = book.Desription;
                MessageBox.Show(book.Id.ToString());

            }
        }
    }
}
