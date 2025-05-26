using System;
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
using MySql.Data.MySqlClient;
using System.IO;

using System.Data.SqlClient;

namespace WpfApp1
{ 
    public partial class MainWindow : Window
    {
        MySqlConnection kapcs = new MySqlConnection("server=localhost;user id=root;password=;database=kardinali;port=3306;SslMode=None");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
                kapcs.Open();
                MySqlCommand parancs = new MySqlCommand("SELECT * FROM filmek", kapcs);
                MySqlDataReader olvaso = parancs.ExecuteReader();
                lbAdatok.Items.Clear();
                while (olvaso.Read())
                {
                    lbAdatok.Items.Add(olvaso["filmazon"].ToString()+";"+ olvaso["cim"].ToString()+";"+olvaso["ev"].ToString()+";"+olvaso["szines"].ToString()+";"+olvaso["mufaj"].ToString()+";"+olvaso["hossz"].ToString());
                }
                olvaso.Close();
                kapcs.Close();
        }

        private void lbAdatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbAdatok.SelectedItem != null)
            {
                var sor = lbAdatok.SelectedItem.ToString().Split(';');
                 lbFilmAzon.Content = sor[0]; // Filmazon
                tb1.Text = sor[1];
                tb2.Text = sor[2];
                tb3.Text = sor[3];
                tb4.Text = sor[4];
                tb5.Text = sor[4];
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var sql = $"update filmek set cim = '{tb1.Text}', ev = {tb2.Text}, szines = '{tb3.Text}', mufaj = '{tb4.Text}', hossz = {tb5.Text} where filmazon = '{lbFilmAzon.Content}'";
            MySqlCommand parancs = new MySqlCommand(sql, kapcs);
            try
            {
                kapcs.Open();
                int sor = parancs.ExecuteNonQuery();
                if (sor > 0)
                {
                    MessageBox.Show("rekord sikeresen frissitve");
                }
                else
                {
                    MessageBox.Show("egyik rekord sem lett frissitve");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba tortent: {ex.Message}");
            }
            finally
            {
                kapcs.Close();
            }//123
        }
    }
}
